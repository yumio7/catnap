using UnityEngine;

public class RatCryOfDeath : MonoBehaviour
{
    [SerializeField] private AudioClip cryOfDeath;

    private AudioSource _source;
    // Start is called before the first frame update
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(cryOfDeath, this.transform.position);
    }
}
