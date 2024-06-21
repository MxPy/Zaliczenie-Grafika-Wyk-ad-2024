using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }
}
