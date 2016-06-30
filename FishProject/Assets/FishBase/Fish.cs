using UnityEngine;
using System.Collections;

public class Fish: MonoBehaviour
{
#if DEBUG
    public
#endif
    float aliveTime;
    int moveSpeed;
    public FishMove moveStyle;
    public turnBack backStyle;
    public turnLeft leftStyle;
    public turnRight rightStyle;
    float timeCounter;
    const int MOVE_ACTION   = 0;
    const int BACK_ACTION   = 1;
    const int LEFT_ACTION   = 2;
    const int RIGHT_ACTION  = 3;
    int action;
    public float changeTime;
    
    void Start()
    {
        moveSpeed = 1;
        changeTime = 5 + (Random.value * 4) - 2;
        action = MOVE_ACTION;
        aliveTime = 1200;
    }

	void Update ()
    {
        rnadom();
        switch (action){
            case MOVE_ACTION:
                moveStyle.move(moveSpeed);
                break;
            case BACK_ACTION:
                if ((timeCounter -= Time.deltaTime) < 0)
                    action = MOVE_ACTION;
                break;
            case LEFT_ACTION:
                if ((timeCounter -= Time.deltaTime) < 0)
                    action = MOVE_ACTION;
                break;
            case RIGHT_ACTION:
                if ((timeCounter -= Time.deltaTime) < 0)
                    action = MOVE_ACTION;
                break;
        }

        if ((aliveTime -= Time.deltaTime) < 0)
        {
            Destroy(this.gameObject);
        }
    }
    void rnadom()
    {
        if ((changeTime -= Time.deltaTime) < 0)
        {
            changeTime = 5;
            if (Random.value * 10 < 5)
            {
                action = LEFT_ACTION;
                leftStyle.move(1);
                changeTime += leftStyle.getAnimateTime();
            }
            else
            {
                action = RIGHT_ACTION;
                rightStyle.move(1);
                changeTime += rightStyle.getAnimateTime();
            }
            changeTime += (Random.value * 4) - 2;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            action = BACK_ACTION;
            timeCounter = backStyle.getAnimateTime();
            backStyle.move(moveSpeed);
#if DEBUG
            Debug.Log("touch the wall");
#endif
        }
#if DEBUG
        else { Debug.Log("touch the other thing unknow"); }
#endif
    }
}
