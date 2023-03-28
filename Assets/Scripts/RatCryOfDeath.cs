using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCryOfDeath : MonoBehaviour
{
    [SerializeField] AudioClip cryOfDeath;

    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(cryOfDeath, this.transform.position);
    }
}
