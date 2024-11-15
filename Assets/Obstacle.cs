using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int direction;
    public float speed;

    void FixedUpdate()
    {
        transform.position += new Vector3(direction * speed, 0, 0);
        transform.Rotate(0, 0, 1);
    }
}
