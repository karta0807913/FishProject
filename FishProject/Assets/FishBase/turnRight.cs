using UnityEngine;
using System.Collections;

public class turnRight : MonoBehaviour {
    float animateTime = 2.3f;
    float speed;
    float seedX = 1.5f;
    float seedZ = 2f;
    float counter;
    public turnRight()
    {
        counter = 0;
        speed = 0;
    }
    public virtual float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move(int speed)
    {
        if (this.speed <= 0)
            this.speed = speed;
        if(counter < animateTime / 2) {
            this.speed -= Time.deltaTime * seedX;
            this.transform.Translate(speed*Time.deltaTime, 0, 0);
        }else{
            this.speed += Time.deltaTime * seedZ;
            this.transform.Translate(0, 0, -speed*Time.deltaTime);
        }
        if ((counter += Time.deltaTime) > animateTime){
            counter = 0;
            speed = 0;
        }
    }
}
