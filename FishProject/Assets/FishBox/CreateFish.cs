using System.IO;
using UnityEngine;


public class CreateFish : MonoBehaviour {

    public const string defPath = "";//"tmpPic/";
    public Transform[] vec;
    public GameObject fishObject;
    public Texture tex;
    public string picName;
    public bool boolean = false;

    void Start()
    {
        
        //fishObject = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Normal_Fish.prefab");
        fishObject = Resources.Load("Normal_Fish") as GameObject;
    }

    void Update()
    {
        if (boolean)
        {
            Material material = new Material(Shader.Find("Sprites/Default"));
            tex = readTexture(defPath + picName);
            material.mainTexture = tex;

            int ran = (int)(Random.value * 10 % 9);
            GameObject tmp = (GameObject)Instantiate(fishObject, vec[ran].position, vec[ran].rotation);
            tmp.transform.GetChild(0).GetChild(0).
                GetComponent<Renderer>().material = material;
            Debug.Log("Fish Created");
            boolean = false;
        }
    }

    private static Texture readTexture(string path)
    {
        //return UnityEditor.AssetDatabase.LoadAssetAtPath<Texture>(path + ".bmp");
        //return Resources.Load(path) as Texture;
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D tex2D = new Texture2D(1, 1);
        tex2D.LoadImage(bytes);
        return tex2D;
    }

    public void createFish(string picName)
    {
        this.picName = picName;
        boolean = true;
    }
}
