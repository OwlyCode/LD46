using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.collider.gameObject;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            source.SendMessage("OnWaterTouch");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
