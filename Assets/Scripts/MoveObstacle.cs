using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
	void Update ()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time, 10f) - 5f,transform.position.y, transform.position.z);
    }
}
