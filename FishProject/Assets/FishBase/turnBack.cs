﻿using UnityEngine;
using System.Collections;

public class turnBack : MonoBehaviour
{
    protected float animateTime = 0;
    public float getAnimateTime()
    {
        return animateTime;
    }
    public virtual void move(int speed)
    {
    }
}
