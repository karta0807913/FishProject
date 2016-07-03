using UnityEngine;
using System.Collections;

public class randDom : MonoBehaviour {

    public Transform[] vec;

    public GameObject fishObject;
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
