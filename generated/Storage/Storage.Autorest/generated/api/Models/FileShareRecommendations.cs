// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.PowerShell.Cmdlets.Storage.Models
{
    using static Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Extensions;

    /// <summary>
    /// Constants used for calculating recommended provisioned IOPS and bandwidth for a file share in the storage account.
    /// </summary>
    public partial class FileShareRecommendations :
        Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendations,
        Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendationsInternal
    {

        /// <summary>Backing field for <see cref="BandwidthScalar" /> property.</summary>
        private double? _bandwidthScalar;

        /// <summary>
        /// The scalar for bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Origin(Microsoft.Azure.PowerShell.Cmdlets.Storage.PropertyOrigin.Owned)]
        public double? BandwidthScalar { get => this._bandwidthScalar; }

        /// <summary>Backing field for <see cref="BaseBandwidthMiBPerSec" /> property.</summary>
        private int? _baseBandwidthMiBPerSec;

        /// <summary>
        /// The base bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Origin(Microsoft.Azure.PowerShell.Cmdlets.Storage.PropertyOrigin.Owned)]
        public int? BaseBandwidthMiBPerSec { get => this._baseBandwidthMiBPerSec; }

        /// <summary>Backing field for <see cref="BaseIop" /> property.</summary>
        private int? _baseIop;

        /// <summary>The base IOPS in the file share provisioned IOPS recommendation formula.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Origin(Microsoft.Azure.PowerShell.Cmdlets.Storage.PropertyOrigin.Owned)]
        public int? BaseIop { get => this._baseIop; }

        /// <summary>Backing field for <see cref="IoScalar" /> property.</summary>
        private double? _ioScalar;

        /// <summary>The scalar for IO in the file share provisioned IOPS recommendation formula.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Origin(Microsoft.Azure.PowerShell.Cmdlets.Storage.PropertyOrigin.Owned)]
        public double? IoScalar { get => this._ioScalar; }

        /// <summary>Internal Acessors for BandwidthScalar</summary>
        double? Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendationsInternal.BandwidthScalar { get => this._bandwidthScalar; set { {_bandwidthScalar = value;} } }

        /// <summary>Internal Acessors for BaseBandwidthMiBPerSec</summary>
        int? Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendationsInternal.BaseBandwidthMiBPerSec { get => this._baseBandwidthMiBPerSec; set { {_baseBandwidthMiBPerSec = value;} } }

        /// <summary>Internal Acessors for BaseIop</summary>
        int? Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendationsInternal.BaseIop { get => this._baseIop; set { {_baseIop = value;} } }

        /// <summary>Internal Acessors for IoScalar</summary>
        double? Microsoft.Azure.PowerShell.Cmdlets.Storage.Models.IFileShareRecommendationsInternal.IoScalar { get => this._ioScalar; set { {_ioScalar = value;} } }

        /// <summary>Creates an new <see cref="FileShareRecommendations" /> instance.</summary>
        public FileShareRecommendations()
        {

        }
    }
    /// Constants used for calculating recommended provisioned IOPS and bandwidth for a file share in the storage account.
    public partial interface IFileShareRecommendations :
        Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.IJsonSerializable
    {
        /// <summary>
        /// The scalar for bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Read = true,
        Create = false,
        Update = false,
        Description = @"The scalar for bandwidth in the file share provisioned bandwidth recommendation formula.",
        SerializedName = @"bandwidthScalar",
        PossibleTypes = new [] { typeof(double) })]
        double? BandwidthScalar { get;  }
        /// <summary>
        /// The base bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Read = true,
        Create = false,
        Update = false,
        Description = @"The base bandwidth in the file share provisioned bandwidth recommendation formula.",
        SerializedName = @"baseBandwidthMiBPerSec",
        PossibleTypes = new [] { typeof(int) })]
        int? BaseBandwidthMiBPerSec { get;  }
        /// <summary>The base IOPS in the file share provisioned IOPS recommendation formula.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Read = true,
        Create = false,
        Update = false,
        Description = @"The base IOPS in the file share provisioned IOPS recommendation formula.",
        SerializedName = @"baseIOPS",
        PossibleTypes = new [] { typeof(int) })]
        int? BaseIop { get;  }
        /// <summary>The scalar for IO in the file share provisioned IOPS recommendation formula.</summary>
        [Microsoft.Azure.PowerShell.Cmdlets.Storage.Runtime.Info(
        Required = false,
        ReadOnly = true,
        Read = true,
        Create = false,
        Update = false,
        Description = @"The scalar for IO in the file share provisioned IOPS recommendation formula.",
        SerializedName = @"ioScalar",
        PossibleTypes = new [] { typeof(double) })]
        double? IoScalar { get;  }

    }
    /// Constants used for calculating recommended provisioned IOPS and bandwidth for a file share in the storage account.
    internal partial interface IFileShareRecommendationsInternal

    {
        /// <summary>
        /// The scalar for bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        double? BandwidthScalar { get; set; }
        /// <summary>
        /// The base bandwidth in the file share provisioned bandwidth recommendation formula.
        /// </summary>
        int? BaseBandwidthMiBPerSec { get; set; }
        /// <summary>The base IOPS in the file share provisioned IOPS recommendation formula.</summary>
        int? BaseIop { get; set; }
        /// <summary>The scalar for IO in the file share provisioned IOPS recommendation formula.</summary>
        double? IoScalar { get; set; }

    }
}