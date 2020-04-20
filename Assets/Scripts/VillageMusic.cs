using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageMusic : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.gameObject.SendMessage("OnFadeOutMusic");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.gameObject.SendMessage("OnFadeInMusic");
        }
    } 
}
