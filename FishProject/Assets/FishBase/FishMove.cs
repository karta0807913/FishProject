using UnityEngine;
using System.Collections;

public class FishMove : MonoBehaviour {
#if DEBUG
    public
#endif
    Vector3 nextMove;
	// Use this for initialization
	public FishMove()
    {
        nextMove = new Vector3(1, 0, 0);
    }

    public virtual void move()
    {
        this.transform.Translate(nextMove * Time.deltaTime);
    }
}
