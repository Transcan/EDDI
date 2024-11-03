﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using Utilities;

namespace EddiConfigService
{
    public sealed partial class ConfigService
    {
        /// <summary> Obtain configuration from a json (for unit testing). </summary>
        public static T FromJson<T>(string json) where T : new()
        {
            T configuration = default;
            if (json != null)
            {
                try
                {
                    configuration = JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    Logging.Warn($"Failed to read {typeof(T).Name}", ex);
                }
            }
            return configuration;
        }

        /// <summary> Obtain configuration from a file.  If the file name is not supplied then a default path is used </summary>
        private static T FromFile<T>(string directory = null) where T : Config, new()
        {
            if (directory == null) { directory = Constants.DATA_DIR; }
            string filename = directory + (typeof(T).GetCustomAttribute(typeof(RelativePathAttribute)) as RelativePathAttribute)?.relativePath;
            T configuration = null;
            if (File.Exists(filename) && !unitTesting)
            {
                string json = Files.Read(filename);
                if (json != null)
                {
                    configuration = FromJson<T>(json);
                }
            }
            return configuration ?? new T();
        }
    }
}
