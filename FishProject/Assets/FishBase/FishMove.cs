using UnityEngine;
using System.Collections;

public class FishMove : MonoBehaviour {
#if DEBUG
    public
#endif
    Vector3 nextMove;
#if DEBUG
    public
#endif
    float aliveTime;
	// Use this for initialization
	public FishMove()
    {
        nextMove = new Vector3(-1, 0, 0);
        aliveTime = 1200;
    }

    public virtual void move()
    {
        this.transform.Translate(nextMove * Time.deltaTime);
        if ((aliveTime -= Time.deltaTime) < 0)
        {
            Destroy(this);
        }
    }
}
