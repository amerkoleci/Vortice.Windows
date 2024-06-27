// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial struct AdapterIdentifier
{
    /// <summary>
    /// Gets a value indicating whether the adapter is WHQL certified.
    /// </summary>
    public bool Certified
    {
        get { return WhqlLevel != 0; }
    }

    /// <summary>
    /// Gets the driver version.
    /// </summary>
    public Version DriverVersion
    {
        get
        {
            return new Version((int)(RawDriverVersion >> 48) & 0xFFFF, (int)(RawDriverVersion >> 32) & 0xFFFF, (int)(RawDriverVersion >> 16) & 0xFFFF, (int)(RawDriverVersion >> 0) & 0xFFFF);
        }
    }

    /// <summary>
    /// Gets the certification date.
    /// </summary>
    public DateTime CertificationDate
    {
        get
        {
            // Decoding http://msdn.microsoft.com/en-us/library/bb172505%28v=vs.85%29.aspx
            return WhqlLevel == 0 ? DateTime.MaxValue : (WhqlLevel == 1 ? DateTime.MinValue : new DateTime(1999 + (WhqlLevel >> 16), (WhqlLevel & 0xFF00) >> 8, WhqlLevel & 0xFF));
        }
    }
}
