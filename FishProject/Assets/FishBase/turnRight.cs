using UnityEngine;
using System.Collections;

public class turnRight : MonoBehaviour {
    float animateTime = 1.8f;
    public virtual float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move()
    {
        this.transform.Translate(-90, 0, 0);
    }
}
