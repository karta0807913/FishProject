using UnityEngine;
using System.Collections;

public class ChangeTex : MonoBehaviour {
    public Texture tex;
    // Use this for initialization
    void Start () {
        tex = Resources.Load("tmpPic/f66708a75587fa90001b54319af1cb30") as Texture;
        this.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
