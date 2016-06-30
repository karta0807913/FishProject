using UnityEngine;
using System.Collections;

public class turnLeft : MonoBehaviour {
    float animateTime = 1.8f;
    public virtual float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move(int speed)
    {
        this.transform.Rotate(0, 90, 0);
    }
}
