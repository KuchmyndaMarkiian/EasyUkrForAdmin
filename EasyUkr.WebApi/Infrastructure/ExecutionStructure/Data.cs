using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EasyUkr.WebApi.Infrastructure.ExecutionStructure
{
    /// <summary>
    /// Class for manipulating with picture on web client
    /// </summary>
    public static class Data
    {
        public static bool FromClient { get; set; } = true;
        public static List<string> Files { get; set; }=new List<string>();
        public static void SerializeFile(byte[] bytes)
        {
           Files.Add(JsonConvert.SerializeObject(bytes));
        }

        public static void Refresh()
        {
            FromClient = true;
            Files =new List<string>();
        }
        public static byte[] DesrializeFile(int index = 0)
        {
            if (Files.Any())
            {
                var file = Files.First();
                return  JsonConvert.DeserializeObject<byte[]>(file);
            }
            return null;
        }
        public static string File(int index = 0)
        {
            if (Files.Any())
            {
                var file = Files.First();
                return file;
            }
            return null;
        }
    }
}