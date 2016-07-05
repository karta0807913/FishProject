using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Fish: MonoBehaviour
{
#if DEBUG
    public
#endif
    float aliveTime;
    int moveSpeed;
    Animator anima;
    GameObject childAni;

    FishMove moveStyle;
    turnBack backStyle;
    turnLeft leftStyle;
    turnRight rightStyle;

    const int MOVE_ACTION       = 0;
    const int BACK_ACTION       = 1;
    const int LEFT_ACTION       = 2;
    const int RIGHT_ACTION      = 3;
    const int RANDOM_BASIC_TIME = 7;
    const int RANDOM_SEED       = 4;
    const int CUNTER_OFFEST     = 2;
    int action;

    float timeCounter;
    public float changeTime;
    
    void Start()
    {
        childAni = this.gameObject.transform.GetChild(0).gameObject;
        anima = childAni.GetComponent<Animator>();
        backStyle = this.gameObject.GetComponent<turnBack>();
        moveStyle = this.gameObject.GetComponent<FishMove>();
        leftStyle = this.gameObject.GetComponent<turnLeft>();
        rightStyle = this.gameObject.GetComponent<turnRight>();
        changeTime = RANDOM_BASIC_TIME + (Random.value * RANDOM_SEED);
        action = MOVE_ACTION;
        moveSpeed = 1;
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
                backStyle.move(moveSpeed);
                if ((timeCounter -= Time.deltaTime) < 0){
                    action = MOVE_ACTION;
                    anima.SetBool("back", false);
                    this.transform.Rotate(0, 180, 0);
                }
                break;
            case LEFT_ACTION:
                leftStyle.move(moveSpeed);
                if ((timeCounter -= Time.deltaTime) < 0){
                    action = MOVE_ACTION;
                    anima.SetBool("left", false);
                    this.transform.Rotate(0, -90, 0);
                }
                break;
            case RIGHT_ACTION:
                rightStyle.move(moveSpeed);
                if ((timeCounter -= Time.deltaTime) < 0) { 
                    action = MOVE_ACTION;
                    anima.SetBool("right", false);
                    this.transform.Rotate(0, 90, 0);
                }
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
                anima.SetBool("left", true);
                action = LEFT_ACTION;
                changeTime += leftStyle.getAnimateTime() + CUNTER_OFFEST;
                timeCounter = leftStyle.getAnimateTime();
            }
            else
            {
                anima.SetBool("right", true);
                action = RIGHT_ACTION;
                changeTime += rightStyle.getAnimateTime() + CUNTER_OFFEST;
                timeCounter = rightStyle.getAnimateTime();
            }
            changeTime += (Random.value * 4) - 2;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            if (anima == null)
                return;
            anima.SetBool("back", true);
            action = BACK_ACTION;
            changeTime += rightStyle.getAnimateTime() + CUNTER_OFFEST;
            timeCounter = backStyle.getAnimateTime();
#if DEBUG
            Debug.Log("touch the wall");
#endif
        }
#if DEBUG
        else { Debug.Log("touch the other thing unknow"); }
#endif
    }
}
