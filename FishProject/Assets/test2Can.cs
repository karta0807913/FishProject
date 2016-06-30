using UnityEngine;
using System.Collections;

public class test2Can : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        this.transform.Rotate(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
	}
   
}
