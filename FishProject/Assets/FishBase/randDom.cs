using UnityEngine;
using System.Collections;

public class randDom : MonoBehaviour {

    public Transform[] vec;

    public GameObject fishObject;

    // Use this for initialization
    void Start () {
        //vec = new Transform[9];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 50, 20), "add fish")){
            int ran = (int)(Random.value * 10 % 9);
            Instantiate(fishObject, vec[ran].position, vec[ran].rotation);
        }
    }
}
