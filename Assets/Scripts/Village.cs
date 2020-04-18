using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        Debug.Log("hit !");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Upgrade zone");
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
