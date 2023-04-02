using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform playerBody;
    public float mouseSensitivity = 100;

    private float pitch = -90f;

    private void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw
        if (!LevelManager.isGameOver)
        {
            playerBody.Rotate(Vector3.back * moveX);
        }

        //pitch
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -180f, 0f);

        if (!LevelManager.isGameOver)
        {
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}