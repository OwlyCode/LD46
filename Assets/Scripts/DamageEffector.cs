using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffector : MonoBehaviour
{
    public GameObject breakParticles;

    void OnDisable()
    {
        Instantiate(breakParticles, transform.position, Quaternion.identity);
    }
}
