using UnityEngine;
using System.Collections;

public class turnBack : MonoBehaviour {

    public turnBack()
    {
	
	}

    public virtual void move()
    {
        this.transform.Rotate(0, 180, 0);
    }
}
