using UnityEngine;
using System.Collections;

public class turnRight : MonoBehaviour {
    public virtual void move()
    {
        this.transform.Translate(-90, 0, 0);
    }
}
