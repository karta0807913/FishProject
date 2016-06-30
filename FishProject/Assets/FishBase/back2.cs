using UnityEngine;
using System.Collections;

public class back2 : turnBack {

	// Use this for initialization
	void Start () {
	
	}
    public override void move()
    {
        this.transform.Rotate(0, 0, 90);
    }
}
