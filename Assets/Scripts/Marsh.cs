using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marsh : MonoBehaviour
{
    const float slowFactor = 0.5f;

    const float sicknessIncrement = 6f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.SendMessage("ApplySpeedModifier", slowFactor);
            other.gameObject.SendMessage("IncreaseMarsh");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.SendMessage("ApplySpeedModifier", 1f);
            other.gameObject.SendMessage("DecreaseMarsh");
        }
    }

    void Update()
    {
        
    }
}
