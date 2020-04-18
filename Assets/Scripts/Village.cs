using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        Debug.Log("hit !");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Upgrade zone");
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
