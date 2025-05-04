using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Sliceable"))
        //{
        //    Debug.Log("We got a sword tip collision with a sliceable");
        //}
        if(other.TryGetComponent<SliceableObject>(out SliceableObject sliceableObject))
        {
            sliceableObject.Slice();
        }
    }
}
