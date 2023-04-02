using UnityEngine;

public class Spin : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);

    }
}
