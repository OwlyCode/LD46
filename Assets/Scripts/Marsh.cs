using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsh : MonoBehaviour
{   const float slowFactor = 0.5f;
    const float sicknessIncrement = 6f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.SendMessage("ApplySpeedModifier", slowFactor);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.SendMessage("IncreaseSickness", sicknessIncrement * Time.fixedDeltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.SendMessage("ApplySpeedModifier", 1f);
        }
    }

    void Update()
    {
        
    }
}
