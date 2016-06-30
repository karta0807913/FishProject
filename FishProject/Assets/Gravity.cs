using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    public float speed = 1;
    float x = 1.6f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(0, -(speed += Time.deltaTime * x)*Time.deltaTime, 0);
	}
    void OnTriggerEnter(Collider other)
    {
        Destroy(this, 1);
        x = -x;
    }
}
