﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNetCore.Razor.LanguageServer.Common.Serialization
{
    internal class FullProjectSnapshotHandleJsonConverter : JsonConverter
    {
        public static readonly FullProjectSnapshotHandleJsonConverter Instance = new FullProjectSnapshotHandleJsonConverter();

        public override bool CanConvert(Type objectType)
        {
            return typeof(FullProjectSnapshotHandle).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                return null;
            }

            var obj = JObject.Load(reader);
            var filePath = obj[nameof(FullProjectSnapshotHandle.FilePath)].Value<string>();
            var configuration = obj[nameof(FullProjectSnapshotHandle.Configuration)].ToObject<RazorConfiguration>(serializer);
            var projectWorkspaceState = obj[nameof(FullProjectSnapshotHandle.ProjectWorkspaceState)].ToObject<ProjectWorkspaceState>(serializer);

            return new FullProjectSnapshotHandle(filePath, configuration, projectWorkspaceState);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var handle = (FullProjectSnapshotHandle)value;

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(FullProjectSnapshotHandle.FilePath));
            writer.WriteValue(handle.FilePath);

            if (handle.Configuration == null)
            {
                writer.WritePropertyName(nameof(FullProjectSnapshotHandle.Configuration));
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName(nameof(FullProjectSnapshotHandle.Configuration));
                serializer.Serialize(writer, handle.Configuration);
            }

            if (handle.ProjectWorkspaceState == null)
            {
                writer.WritePropertyName(nameof(FullProjectSnapshotHandle.ProjectWorkspaceState));
                writer.WriteNull();
            }
            else
            {
                writer.WritePropertyName(nameof(FullProjectSnapshotHandle.ProjectWorkspaceState));
                serializer.Serialize(writer, handle.ProjectWorkspaceState);
            }

            writer.WriteEndObject();
        }
    }
}