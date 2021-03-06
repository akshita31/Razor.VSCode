﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;

namespace Microsoft.AspNetCore.Razor.OmniSharpPlugin
{
    public sealed class OmniSharpHostProject
    {
        public OmniSharpHostProject(string projectFilePath, RazorConfiguration razorConfiguration)
        {
            if (projectFilePath == null)
            {
                throw new ArgumentNullException(nameof(projectFilePath));
            }

            if (razorConfiguration == null)
            {
                throw new ArgumentNullException(nameof(razorConfiguration));
            }

            InternalHostProject = new HostProject(projectFilePath, razorConfiguration);
        }

        public string FilePath => InternalHostProject.FilePath;

        public RazorConfiguration Configuration => InternalHostProject.Configuration;

        internal HostProject InternalHostProject { get; }
    }
}
