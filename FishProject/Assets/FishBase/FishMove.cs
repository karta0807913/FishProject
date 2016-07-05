using UnityEngine;
using System.Collections;

public class FishMove : MonoBehaviour
{
    public virtual void move(int speed)
    {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
