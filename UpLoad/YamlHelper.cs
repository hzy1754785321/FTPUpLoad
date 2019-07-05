using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace UpLoad
{
    public class YamlHelper
    {
        static string _filePath = Directory.GetCurrentDirectory() + @"\Config\config.yaml";

        public static void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// 将对象序列化为yaml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void Serializer<T>(T obj)
        {
            StreamWriter yamlWriter = File.CreateText(_filePath);
            Serializer yamlSerializer = new Serializer();
            yamlSerializer.Serialize(yamlWriter, obj);
            yamlWriter.Close();
        }

        /// <summary>
        /// 将yaml反序列为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public T Deserializer<T>()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException();
            }
            StreamReader yamlReader = File.OpenText(_filePath);
            Deserializer yamlDeserializer = new Deserializer();

            T info = yamlDeserializer.Deserialize<T>(yamlReader);
            yamlReader.Close();
            return info;
        }

    }
}
