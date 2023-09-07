using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace MusicRandomizer
{
    public class Utils
    {
        public static string GetPluginPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetFilePath(string fileName)
        { 
            return Path.Combine(Utils.GetPluginPath(), fileName);
        }

        public static class JsonParser
        {
            public static T Read<T>(string fileName)
            {
                var jsonString = File.ReadAllText(GetFilePath(fileName));
                return JsonSerializer.Deserialize<T>(jsonString)!;
            }

            public static void Write<T>(T classInst, string fileName)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(GetFilePath(fileName), JsonSerializer.Serialize(classInst, options));
            }
        }

        public static int ReadConfigFile()
        {
            return 1;
        }
    }
}
