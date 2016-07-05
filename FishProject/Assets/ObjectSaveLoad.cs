
using System.IO;
using System.Xml.Serialization;

public class ObjectSaveLoad{
    public static T Load<T>(string path)
    {
        T Object = default(T);
        XmlSerializer xmls = new XmlSerializer(typeof(T));
        using (var stream = File.OpenRead(path))
        {
            Object = (T)xmls.Deserialize(stream);
        }
        return Object;
    }
    public static void Save<T>(string path, T Object)
    {
        XmlSerializer xmls = new XmlSerializer(typeof(T));

        using (var stream = File.OpenWrite(path))
        {
            xmls.Serialize(stream, Object);
        }
    }
}
