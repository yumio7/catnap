using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyDuration = 3;

    private void Start()
    {
        Destroy(gameObject, destroyDuration);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
