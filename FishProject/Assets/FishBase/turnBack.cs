using UnityEngine;
using System.Collections;

public class turnBack : MonoBehaviour {
    float animateTime = 1.8f;
    public virtual float getAnimateTime(){
        return animateTime;
    }
    public virtual void move()
    {
        this.transform.Rotate(0, 180, 0);
    }
}
