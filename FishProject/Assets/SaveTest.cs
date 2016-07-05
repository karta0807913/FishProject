using UnityEngine;
using System.Xml.Serialization;
using System.IO;
public class SaveTest : MonoBehaviour {
    public Player player;
    void Start()
    {
        Player pl = new Player { Level = 5, Health = 500 };
        pl.arr = new pub[2];
        pl.arr[0] = new pub(2);
        //pl.arr[0].x = 2;
        pl.arr[1] = new pub(3);
        //pl.arr[1].x = 3;
        Save<Player>("my_player.xml", pl);
        //Debug.Log(Load<Player>("my_player.xml").arr[0]);
    }
	// Update is called once per frame
	void Update () {
	
	}

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

[XmlRoot]
public class Player
{
    [XmlElement]
    public int Level { get; set; }

    [XmlElement]
    public int Health { get; set; }

    [XmlArray("arr")]
    [XmlArrayItem("pub")]
    public pub[] arr;
}

public class pub
{
    [XmlAttribute]
    public int x;
    public pub(int x)
    {
        this.x = x;
    }
    public pub() {
    }
}
