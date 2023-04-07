using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform playerBody;
    public float mouseSensitivity = 100;

    private float pitch = 0f;

    private void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        mouseSensitivity = PlayerPrefs.GetInt("MouseSenstivity", 50);
        print(mouseSensitivity);
    }

    // Update is called once per frame
    private void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw
        if (!LevelManager.isGameOver)
        {
            playerBody.Rotate(Vector3.up * moveX);
        }

        //pitch
        pitch -= moveY;

        if (!LevelManager.isGameOver)
        {
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}