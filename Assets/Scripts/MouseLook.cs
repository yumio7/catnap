using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    public float mouseSensitivity = 100;

    float pitch = 0;

    void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw
        playerBody.Rotate(Vector3.back * moveX);

        //pitch
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -180f, 0f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
