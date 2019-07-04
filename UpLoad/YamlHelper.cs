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

        public static void Serializer<T>(T obj)            // 序列化操作  
        {
            StreamWriter yamlWriter = File.CreateText(_filePath);
            Serializer yamlSerializer = new Serializer();
            yamlSerializer.Serialize(yamlWriter, obj);
            yamlWriter.Close();
        }

        static public T Deserializer<T>()           // 泛型反序列化操作  
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException();
            }
            StreamReader yamlReader = File.OpenText(_filePath);
            Deserializer yamlDeserializer = new Deserializer();

            //读取持久化对象  
            T info = yamlDeserializer.Deserialize<T>(yamlReader);
            yamlReader.Close();
            return info;
        }

    }
}
