using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

public class FishManager : MonoBehaviour {
    static int maximalFishObject = 30;
    private const string savePath = "data.xml";
    FishDataArray fishDataArray;
    CreateFish createFish;
    List<pair<string, GameObject>> fishObject;

    private FishManager() { }

    void Start ()
    {
        createFish = GetComponent<CreateFish>();
        fishObject = new List<pair<string, GameObject>>();
        fishDataArray = new FishDataArray();

        if (File.Exists(savePath)){
            createFish.createFish(Load<FishDataArray>(savePath).fishPicName);
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("data saving");
        Save(savePath, fishDataArray);
    }

    public void addData(pair<string, GameObject> data)
    {
        Debug.Log("adding data");
        fishDataArray.addData(data.first);
        Debug.Log(data.second);
        fishObject.Add(data);
        if(fishObject.Count > maximalFishObject) {
            foreach(var tmp in fishObject){
                Debug.Log(tmp.second);
                Destroy(tmp.second);
                fishObject.Remove(tmp);
                break;
            }
        }
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
public class FishDataArray
{
    static int maximalFish = 20;

    [XmlArray("List")]
    [XmlArrayItem("fishPicName")]
    public string[] fishPicName = new string[maximalFish];
    
    int index;
    
    public void addData(string data)
    {
        Debug.Log(data);

        fishPicName[index] = data;
        ++index;
        index %= maximalFish;

        Debug.Log("fishData End");
    }
}

public class pair<T1, T2>{
    public T1 first;
    public T2 second;
    public pair(T1 first, T2 second)
    {
        this.first = first;
        this.second = second;
    }
}