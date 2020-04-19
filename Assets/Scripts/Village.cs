using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public string toggledItem = "Helmet";

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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.gameObject.SendMessage("OnFadeOutMusic");
            other.gameObject.SendMessage("OnToggle" + toggledItem);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.gameObject.SendMessage("OnFadeInMusic");
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
