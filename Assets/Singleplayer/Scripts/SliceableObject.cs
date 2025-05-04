using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Slice()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(this.gameObject);

    }

}
