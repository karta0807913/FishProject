using UnityEngine;
using System.Collections;

public class Fish: MonoBehaviour
{
#if DEBUG
    public
#endif
    float aliveTime;
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
    
    void Start()
    {
        action = MOVE_ACTION;
    }

	void Update ()
    {
        if ((aliveTime -= Time.deltaTime) < 0)
        {
            Destroy(this.gameObject);
        }
        switch (action){
            case MOVE_ACTION:
                moveStyle.move();
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
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            action = BACK_ACTION;
            timeCounter = backStyle.getAnimateTime();
            backStyle.move();
#if DEBUG
            Debug.Log("touch the wall");
#endif
        }
#if DEBUG
        else { Debug.Log("touch the other thing unknow"); }
#endif
    }
}
