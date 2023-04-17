using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform ob;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(ob.position, Vector3.up, speed * Time.deltaTime);
    }
}
