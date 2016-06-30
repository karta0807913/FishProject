using UnityEngine;
using System.Collections;

public class Fish: MonoBehaviour
{
    public FishMove moveStyle;//= new FishMove();
    public turnBack backStyle;// = new back2();
    public turnLeft leftStyle;//= new turnLeft();
    public turnRight rightStyle;//= new turnRight();

    void Start()
    {
        //backStyle = gameObject.GetComponent<back2>();
    }

	void Update ()
    {
        moveStyle.move();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
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
