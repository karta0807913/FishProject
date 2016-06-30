using UnityEngine;
using System.Collections;

public class turnLeft : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
    public virtual void move()
    {
        this.transform.Translate(90, 0, 0);
    }
}
