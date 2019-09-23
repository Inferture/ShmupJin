using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlSerialization
{

    /*//Before
     public static void WriteToXml<T>(string filePath, T[] array)
    {
        if(array.Length>0)
        {
            WriteToXml(filePath, array[0], false);
        }
        for(int i=1;i<array.Length;i++)
        {
            WriteToXml(filePath, array[i], true);
        }
    }
    public static void WriteToXml<T>(string filePath, List<T> list)
    {
        if (list.Count > 0)
        {
            WriteToXml(filePath, list[0], false);
        }
        for (int i = 1; i < list.Count; i++)
        {
            WriteToXml(filePath, list[i], true);
        }
    }
    public static void WriteToXml<T>(string filePath, T obj, bool append = false)
    {
        TextWriter writer = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(filePath, append);
            serializer.Serialize(writer, obj);
        }
        finally
        {
            if (writer != null)
                writer.Close();
            Debug.Log("xml serialized into " + filePath);
        }
        
    }
    public static T ReadFromXml<T>(string filePath)
    {
        TextReader reader = null;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            reader = new StreamReader(filePath);
            return (T)serializer.Deserialize(reader);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }
    */

    //After (ReadFromXmlResources named ReadFromXml and WriteToXmlResources named WriteToXml)
    public static T ReadFromXmlResource<T>(TextAsset ta)
    {
        using (TextReader textReader = new StringReader(ta.text))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            T XmlData = (T)serializer.Deserialize(textReader);

            return XmlData;
        }
    }

    public static void WriteToXmlResource<T>(string filePath, T obj, bool append = false)
    {
        TextWriter writer = null;

        if (!Directory.Exists(Path.Combine(Application.dataPath, "Resources")))
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "Resources"));
        }
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(Path.Combine(Path.Combine(Application.dataPath, "Resources"), filePath), append);
            serializer.Serialize(writer, obj);
        }
        finally
        {
            if (writer != null)
                writer.Close();
            Debug.Log("xml serialized into " + filePath);
        }

    }

}
