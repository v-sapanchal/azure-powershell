﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Dns
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Azure.Management.Dns.Models;

    /// <summary>
    /// Represents a set of records with the same name, with the same type and in the same zone.
    /// </summary>
    public class DnsRecordSet : ICloneable
    {
        /// <summary>
        /// Gets or sets the ID of the record set.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this record set, relative to the name of the zone to which it belongs and WITHOUT a terminating '.' (dot) character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the zone to which this recordset belongs.
        /// </summary>
        public string ZoneName { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource group to which this record set belongs.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the TTL of all the records in this record set.
        /// </summary>
        public uint Ttl { get; set; }

        /// <summary>
        /// Gets or sets the Etag of this record set.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the type of DNS records in this record set. Only records of this type may be added to this record set.
        /// </summary>
        public RecordType RecordType { get; set; }

        /// <summary>
        /// Gets or sets the alias target resource Id of the record set
        /// </summary>
        public string TargetResourceId { get; set; }

        /// <summary>
        /// Gets or sets the list of records in this record set.
        /// </summary>
        public List<DnsRecordBase> Records { get; set; }

        /// <summary>
        /// Gets or sets the tags of this record set.
        /// </summary>
        public Hashtable Metadata { get; set; }

        /// <summary>
        /// Gets or sets the provisioning state of the record set
        /// </summary>
        public string ProvisioningState { get; set; }

        /// <summary>
        /// Returns a deep copy of this record set
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var clone = new DnsRecordSet();

            clone.Name = this.Name;
            clone.TargetResourceId = this.TargetResourceId;
            clone.ProvisioningState = this.ProvisioningState;
            clone.Id = this.Id;
            clone.ZoneName = this.ZoneName;
            clone.ResourceGroupName = this.ResourceGroupName;
            clone.Ttl = this.Ttl;
            clone.Etag = this.Etag;
            clone.RecordType = this.RecordType;

            if (this.Records != null)
            {
                clone.Records = this.Records.Select(record => record.Clone()).Cast<DnsRecordBase>().ToList();
            }

            if (this.Metadata != null)
            {
                clone.Metadata = (Hashtable)this.Metadata.Clone();
            }

            return clone;
        }
    }

    /// <summary>
    /// Represents a DNS record that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public abstract class DnsRecordBase : ICloneable
    {
        public abstract object Clone();

        public const int TxtRecordChunkSize = 255;

        public const int CaaRecordMaxLength = 1024;

        public const int CaaRecordMinLength = 0;

        internal abstract object ToMamlRecord();

        internal static DnsRecordBase FromMamlRecord(object record)
        {
            if (record is Management.Dns.Models.ARecord)
            {
                var mamlRecord = (Management.Dns.Models.ARecord)record;
                return new ARecord
                {
                    Ipv4Address = mamlRecord.Ipv4Address
                };
            }
            else if (record is Management.Dns.Models.AaaaRecord)
            {
                var mamlRecord = (Management.Dns.Models.AaaaRecord)record;
                return new AaaaRecord
                {
                    Ipv6Address = mamlRecord.Ipv6Address
                };
            }
            else if (record is Management.Dns.Models.CnameRecord)
            {
                var mamlRecord = (Management.Dns.Models.CnameRecord)record;
                return new CnameRecord
                {
                    Cname = mamlRecord.Cname
                };
            }
            else if (record is Management.Dns.Models.NsRecord)
            {
                var mamlRecord = (Management.Dns.Models.NsRecord)record;
                return new NsRecord
                {
                    Nsdname = mamlRecord.Nsdname
                };
            }
            else if (record is Management.Dns.Models.MxRecord)
            {
                var mamlRecord = (Management.Dns.Models.MxRecord)record;
                return new MxRecord
                {
                    Exchange = mamlRecord.Exchange,
                    Preference = (ushort)mamlRecord.Preference,
                };
            }
            else if (record is Management.Dns.Models.SrvRecord)
            {
                var mamlRecord = (Management.Dns.Models.SrvRecord)record;
                return new SrvRecord
                {
                    Port = (ushort)mamlRecord.Port,
                    Priority = (ushort)mamlRecord.Priority,
                    Target = mamlRecord.Target,
                    Weight = (ushort)mamlRecord.Weight,
                };
            }
            else if (record is Management.Dns.Models.SoaRecord)
            {
                var mamlRecord = (Management.Dns.Models.SoaRecord)record;
                return new SoaRecord
                {
                    Email = mamlRecord.Email,
                    ExpireTime = (uint)mamlRecord.ExpireTime.GetValueOrDefault(),
                    Host = mamlRecord.Host,
                    MinimumTtl = (uint)mamlRecord.MinimumTtl.GetValueOrDefault(),
                    RefreshTime = (uint)mamlRecord.RefreshTime.GetValueOrDefault(),
                    RetryTime = (uint)mamlRecord.RetryTime.GetValueOrDefault(),
                    SerialNumber = (uint)mamlRecord.SerialNumber.GetValueOrDefault(),
                };
            }
            else if (record is Management.Dns.Models.TxtRecord)
            {
                var mamlRecord = (Management.Dns.Models.TxtRecord)record;
                return new TxtRecord
                {
                    Value = ToPowerShellTxtValue(mamlRecord.Value),
                };
            }
            else if (record is Management.Dns.Models.PtrRecord)
            {
                var mamlRecord = (Management.Dns.Models.PtrRecord)record;
                return new PtrRecord
                {
                    Ptrdname = mamlRecord.Ptrdname,
                };
            }
            else if (record is Management.Dns.Models.CaaRecord)
            {
                var mamlRecord = (Management.Dns.Models.CaaRecord)record;
                return new CaaRecord
                {
                    Flags = (byte)mamlRecord.Flags.GetValueOrDefault(),
                    Value = mamlRecord.Value,
                    Tag = mamlRecord.Tag,
                };
            }
            else if (record is Management.Dns.Models.DsRecord)
            {
                var mamlRecord = (Management.Dns.Models.DsRecord)record;
                return new DsRecord
                {
                    Algorithm = mamlRecord.Algorithm.GetValueOrDefault(),
                    Digest = mamlRecord.Digest.Value,
                    DigestType = mamlRecord.Digest.AlgorithmType.GetValueOrDefault(),
                    KeyTag = mamlRecord.KeyTag.GetValueOrDefault(),
                };
            }
            else if (record is Management.Dns.Models.TlsaRecord)
            {
                var mamlRecord = (Management.Dns.Models.TlsaRecord)record;
                return new TlsaRecord
                {
                    Usage = mamlRecord.Usage.GetValueOrDefault(),
                    CertificateAssociationData = mamlRecord.CertAssociationData,
                    MatchingType = mamlRecord.MatchingType.GetValueOrDefault(),
                    Selector = mamlRecord.Selector.GetValueOrDefault(),
                };
            }
            else if (record is Management.Dns.Models.NaptrRecord)
            {
                var mamlRecord = (Management.Dns.Models.NaptrRecord)record;
                return new NaptrRecord
                {
                    Flags = mamlRecord.Flags,
                    Order = (ushort) mamlRecord.Order,
                    Preference = (ushort) mamlRecord.Preference,
                    Regexp = mamlRecord.Regexp,
                    Replacement = mamlRecord.Replacement,
                    Services = mamlRecord.Services,
                };
            }

            return null;
        }

        private static string ToPowerShellTxtValue(ICollection<string> value)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }

            var sb = new StringBuilder();
            foreach (var s in value)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Represents a DNS record of type A that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class ARecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the IPv4 address of this A record in string notation
        /// </summary>
        public string Ipv4Address { get; set; }

        public override string ToString()
        {
            return this.Ipv4Address;
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.ARecord
            {
                Ipv4Address = this.Ipv4Address,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new ARecord { Ipv4Address = this.Ipv4Address };
        }
    }

    /// <summary>
    /// Represents a DNS record of type AAAA that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class AaaaRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the IPv6 address of this AAAA record in string notation.
        /// </summary>
        public string Ipv6Address { get; set; }

        public override string ToString()
        {
            return this.Ipv6Address;
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.AaaaRecord
            {
                Ipv6Address = this.Ipv6Address,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new AaaaRecord { Ipv6Address = this.Ipv6Address };
        }
    }

    /// <summary>
    /// Represents a DNS record of type CNAME that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class CnameRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the canonical name for this CNAME record without a terminating dot.
        /// </summary>
        public string Cname { get; set; }

        public override string ToString()
        {
            return this.Cname;
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.CnameRecord
            {
                Cname = this.Cname,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new CnameRecord { Cname = this.Cname };
        }
    }

    /// <summary>
    /// Represents a DNS record of type NS that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class NsRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the name server name for this NS record, without a terminating dot.
        /// </summary>
        public string Nsdname { get; set; }

        public override string ToString()
        {
            return this.Nsdname;
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.NsRecord
            {
                Nsdname = this.Nsdname,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new NsRecord { Nsdname = this.Nsdname };
        }
    }

    /// <summary>
    /// Represents a DNS record of type TXT that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class TxtRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the text value of this TXT record.
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }

        internal override object ToMamlRecord()
        {
            char[] letters = this.Value.ToCharArray();
            var splitValues = new List<string>();

            int remaining = letters.Length;
            int begin = 0;
            while (remaining > 0)
            {
                if (remaining < TxtRecordChunkSize)
                {
                    splitValues.Add(new string(letters, begin, remaining));
                    remaining = 0;
                }
                else
                {
                    splitValues.Add(new string(letters, begin, TxtRecordChunkSize));
                    begin += TxtRecordChunkSize;
                    remaining -= TxtRecordChunkSize;
                }
            }

            return new Management.Dns.Models.TxtRecord
            {
                Value = splitValues,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new TxtRecord { Value = this.Value };
        }
    }

    /// <summary>
    /// Represents a DNS record of type MX that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class MxRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the preference metric for this MX record.
        /// </summary>
        public ushort Preference { get; set; }

        /// <summary>
        /// Gets or sets the domain name of the mail host, without a terminating dot
        /// </summary>
        public string Exchange { get; set; }

        public override string ToString()
        {
            return string.Format("[{0},{1}]", Preference, Exchange);
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.MxRecord
            {
                Exchange = this.Exchange,
                Preference = this.Preference,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new MxRecord { Exchange = this.Exchange, Preference = this.Preference };
        }
    }

    /// <summary>
    /// Represents a DNS record of type NAPTR that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class NaptrRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the order for this NAPTR record.
        /// </summary>
        public ushort Order { get; set; }

        /// <summary>
        /// Gets or sets the preference metric for this NAPTR record.
        /// </summary>
        public ushort Preference { get; set; }

        /// <summary>
        /// Gets or sets the flags for this NAPTR record.
        /// </summary>
        public string Flags { get; set; }

        /// <summary>
        /// Gets or sets the services for this NAPTR record.
        /// </summary>
        public string Services { get; set; }

        /// <summary>
        /// Gets or sets the regular expression for this NAPTR record.
        /// </summary>
        public string Regexp { get; set; }

        /// <summary>
        /// Gets or sets the replacement for this NAPTR record.
        /// </summary>
        public string Replacement { get; set; }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2},{3},{4},{5}]", Order, Preference, Flags, Services, Regexp, Replacement);
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.NaptrRecord
            {
                Order = this.Order,
                Preference = this.Preference,
                Flags = this.Flags,
                Services = this.Services,
                Regexp = this.Regexp,
                Replacement = this.Replacement
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new NaptrRecord { Order = this.Order, Preference = this.Preference, Flags = this.Flags, Services = this.Services, Regexp = this.Regexp, Replacement = this.Replacement };
        }
    }

    /// <summary>
    /// Represents a DNS record of type SRV that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class SrvRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the domain name of the target for this SRV record, without a terminating dot.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the weight metric for this SRV record.
        /// </summary>
        public ushort Weight { get; set; }

        /// <summary>
        /// Gets or sets the port for this SRV record
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// Gets or sets the priority metric for this SRV record.
        /// </summary>
        public ushort Priority { get; set; }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2},{3}]", Priority, Weight, Port, Target);
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.SrvRecord
            {
                Priority = this.Priority,
                Target = this.Target,
                Weight = this.Weight,
                Port = this.Port,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new SrvRecord
            {
                Priority = this.Priority,
                Target = this.Target,
                Weight = this.Weight,
                Port = this.Port
            };
        }
    }

    /// <summary>
    /// Represents a DNS record of type SOA that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class SoaRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the domain name of the authoritative name server for this SOA record, without a temrinating dot.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the email for this SOA record.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the serial number of this SOA record.
        /// </summary>
        public uint SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the refresh value for this SOA record.
        /// </summary>
        public uint RefreshTime { get; set; }

        /// <summary>
        /// Gets or sets the retry time for SOA record.
        /// </summary>
        public uint RetryTime { get; set; }

        /// <summary>
        /// Gets or sets the expire time for this SOA record.
        /// </summary>
        public uint ExpireTime { get; set; }

        /// <summary>
        /// Gets or sets the minimum TTL for this SOA record.
        /// </summary>
        public uint MinimumTtl { get; set; }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2},{3},{4},{5}]", Host, Email, RefreshTime, RetryTime, ExpireTime, MinimumTtl);
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.SoaRecord
            {
                Host = this.Host,
                Email = this.Email,
                SerialNumber = this.SerialNumber,
                RefreshTime = this.RefreshTime,
                RetryTime = this.RetryTime,
                ExpireTime = this.ExpireTime,
                MinimumTtl = this.MinimumTtl,
            };
        }

        /// <summary>
        /// Cerates a deep copy of this object
        /// </summary>
        /// <returns>A clone of this object</returns>
        public override object Clone()
        {
            return new SoaRecord
            {
                Host = this.Host,
                Email = this.Email,
                SerialNumber = this.SerialNumber,
                RefreshTime = this.RefreshTime,
                RetryTime = this.RetryTime,
                ExpireTime = this.ExpireTime,
                MinimumTtl = this.MinimumTtl,
            };
        }
    }

    /// <summary>
    /// Represents a DNS record of type PTR that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class PtrRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the ptr for this record.
        /// </summary>
        public string Ptrdname { get; set; }

        public override string ToString()
        {
            return this.Ptrdname;
        }

        public override object Clone()
        {
            return new PtrRecord()
            {
                Ptrdname = this.Ptrdname,
            };
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.PtrRecord
            {
                Ptrdname = this.Ptrdname,
            };
        }
    }

    /// <summary>
    /// Represents a DNS record of type CAA that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class CaaRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the flags field for this record.
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Gets or sets the property tag for this record.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the property value for this record.
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return $"[{Flags},{Tag},{Value}]";
        }

        public override object Clone()
        {
            return new CaaRecord()
            {
                Flags = this.Flags,
                Tag = this.Tag,
                Value = this.Value
            };
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.CaaRecord(
                this.Flags,
                this.Tag,
                this.Value);
        }
    }

    /// <summary>
    /// Represents a DNS record of type DS that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class DsRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the key tag for this record.
        /// </summary>
        public int KeyTag { get; set; }

        /// <summary>
        /// Gets or sets the algorithm for this record.
        /// </summary>
        public int Algorithm { get; set; }

        /// <summary>
        /// Gets or sets the digest type for this record.
        /// </summary>
        public int DigestType { get; set; }

        /// <summary>
        /// Gets or sets the digest for this record.
        /// </summary>
        public string Digest { get; set; }

        public override string ToString()
        {
            return $"[{this.KeyTag},{this.Algorithm},{this.DigestType},{this.Digest}]";
        }

        public override object Clone()
        {
            return new DsRecord()
            {
                KeyTag = this.KeyTag,
                Algorithm = this.Algorithm,
                DigestType = this.DigestType,
                Digest = this.Digest
            };
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.DsRecord(
                this.KeyTag,
                this.Algorithm,
                new Management.Dns.Models.Digest(
                    this.DigestType,
                    this.Digest));
        }
    }

    /// <summary>
    /// Represents a DNS record of type TLSA that is part of a <see cref="DnsRecordSet"/>.
    /// </summary>
    public class TlsaRecord : DnsRecordBase
    {
        /// <summary>
        /// Gets or sets the certificate usage for this record.
        /// </summary>
        public int Usage { get; set; }

        /// <summary>
        /// Gets or sets the selector for this record.
        /// </summary>
        public int Selector { get; set; }

        /// <summary>
        /// Gets or sets the matching type for this record.
        /// </summary>
        public int MatchingType { get; set; }

        /// <summary>
        /// Gets or sets the certificate association data for this record.
        /// </summary>
        public string CertificateAssociationData { get; set; }

        public override string ToString()
        {
            return $"[{this.Usage},{this.Selector},{this.MatchingType},{this.CertificateAssociationData}]";
        }

        public override object Clone()
        {
            return new TlsaRecord()
            {
                Usage = this.Usage,
                Selector = this.Selector,
                MatchingType = this.MatchingType,
                CertificateAssociationData = this.CertificateAssociationData
            };
        }

        internal override object ToMamlRecord()
        {
            return new Management.Dns.Models.TlsaRecord(
                this.Usage,
                this.Selector,
                this.MatchingType,
                this.CertificateAssociationData);
        }
    }

}
