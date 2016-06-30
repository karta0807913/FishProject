using UnityEngine;
using System.Collections;

public class turnRight : MonoBehaviour {
    float animateTime = 1.8f;
    public virtual float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move(int speed)
    {
        this.transform.Rotate(0, -90, 0);
    }
}
