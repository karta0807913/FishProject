using UnityEngine;
using System.Collections;

public class N_Fish_Left : turnLeft {
    float speed;
    float seedX = 1.5f;
    float seedZ = 2f;
    float counter;
    public N_Fish_Left()
    {
        animateTime = 2.2f;
        counter = 0;
        speed = 0;
    }
    public override void move(int speed)
    {
        if (this.speed <= 0)
            this.speed = speed;
        if (counter < animateTime / 2)
        {
            this.speed -= Time.deltaTime * seedX;
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            this.speed += Time.deltaTime * seedZ;
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if ((counter += Time.deltaTime) > animateTime)
        {
            counter = 0;
            speed = 0;
        }
    }
}
