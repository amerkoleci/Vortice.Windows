﻿// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;

public partial class IDMLBindingTable
{
    public void BindInputs(params BindingDescription[] descriptions)
    {
        BindInputs(descriptions.Length, descriptions);
    }

    public void BindOutputs(params BindingDescription[] descriptions)
    {
        BindOutputs(descriptions.Length, descriptions);
    }
}