using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {
    struct False_Type { };
    struct True_Type { };
    public float speed = 1;
    float speedArg = 1.6f;

	// Use this for initialization
	void Start () {
    }
	
	void Update () {
        this.transform.Translate(0, -(speed += Time.deltaTime * speedArg) *Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish")
            return;
        speed -= speed /20;
        Destroy(this, 1);
        speedArg = -speedArg;
    }
}
