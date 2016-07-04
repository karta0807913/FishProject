using UnityEngine;
using System.Collections;

public class N_Fish_Move : FishMove {
    public float time;
    const int BASE_TIME = 3;
    const int RANNDOM_SEED = 2;
    public float fishRotateZ;

	public N_Fish_Move() {
        time = 7;
        fishRotateZ = 0;
    }
	
    public override void move(int speed)
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        //transform.localEulerAngles = fishRotate;
        float tmp = this.transform.localEulerAngles.z;
        if (tmp > 180)
            tmp -= 360;
        transform.Rotate(0, 0, (fishRotateZ - tmp) * Time.deltaTime);
        if ((time -= Time.deltaTime) < 0)
        {
            //fishRotation = this.gameObject.transform.rotation;
            time = BASE_TIME + Random.value * RANNDOM_SEED;
            //fishRotation.y = Random.value * 60 - 30;
            fishRotateZ = transform.localEulerAngles.z;
            fishRotateZ = Random.value * 60 - 30;
        }
    }
}
