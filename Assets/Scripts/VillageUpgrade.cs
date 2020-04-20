using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageUpgrade : MonoBehaviour
{
    public string toggledItem;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.gameObject.SendMessage("OnToggle" + toggledItem);
        }
    }
}
