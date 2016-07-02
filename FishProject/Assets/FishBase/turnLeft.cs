using UnityEngine;
using System.Collections;

public class turnLeft : MonoBehaviour {
    float animateTime = 0;
    public virtual float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move(int speed)
    {
    }
}
