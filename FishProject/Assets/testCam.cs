using UnityEngine;
using System.Collections;

public class testCam : MonoBehaviour {
    static int Speed = 10;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W)){
            this.transform.Translate(0.0f, Speed * Time.deltaTime, 0.0f, Space.Self);
        }
        else if (Input.GetKey(KeyCode.S)){
            this.transform.Translate(0.0f, -Speed * Time.deltaTime, 0.0f, Space.Self);
        }
        else if (Input.GetKey(KeyCode.A)){
            this.transform.Translate(-Speed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D)){
            this.transform.Translate(Speed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
	}
}
