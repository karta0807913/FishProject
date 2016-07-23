using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateFish : MonoBehaviour {

    #region CONST
    const string fishPath = "NormalFish/";
    const string picPath = "";
    #endregion

    const int _BUFFER_SIZE = 20;
    public Transform[] vec;
    FishManager fishManager;
    GameObject fishObject;
    List<pair<string, GameObject>> fishBuffer;
    bool boolean = false;

    void Start()
    {
        fishManager = GetComponent<FishManager>();
        fishObject = Resources.Load(fishPath + "Normal_Fish") as GameObject;
        fishBuffer = new List<pair<string, GameObject>>();
    }

    void Update()
    {
        if (boolean) {
            foreach (pair<string, GameObject> fd in fishBuffer) {
                boolean = false;
                var picName = fd.first;
                Material material = new Material(Shader.Find("Sprites/Default"));
                Texture tex = readTexture(picPath + picName);
                material.mainTexture = tex;

                int ran = (int)(Random.value * 10 % 9);

                fd.second = (GameObject)Instantiate(fishObject,
                    vec[ran].position, vec[ran].rotation);

                fd.second.transform.GetChild(0).GetChild(0).
                    GetComponent<Renderer>().material = material;
                fishManager.addData(fd);

                Debug.Log("Fish Created");
            }
            fishBuffer.Clear();
        }
    }

    private static Texture readTexture(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D tex2D = new Texture2D(1, 1);
        tex2D.LoadImage(bytes);
        return tex2D;
    }

    #region CREATE_FUNCTION
    public void createFish(string picName)
    {
        var data = new pair<string, GameObject>(picName, null);
        fishBuffer.Add(data);
        boolean = true;
    }

    public void createFish(string[] array)
    {
        foreach(string picName in array){
            var data = new pair<string, GameObject>(picName, null);
            fishBuffer.Add(data);
            boolean = true;
        }
    }

    public void createFish(List<string> array)
    {
        foreach (string picName in array){
            var data = new pair<string, GameObject>(picName, null);
            fishBuffer.Add(data);
            boolean = true;
        }
    }
    #endregion
}
