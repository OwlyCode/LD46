using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WatchtowerEffect : MonoBehaviour
{
    bool scaled = false;

    void OnScale()
    {
        scaled = true;
    }

    void OnUnscale()
    {
        scaled = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
    }
}
