using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    float offset;
    float excitation;

    float wind = 2f;

    void Start()
    {
        offset = Random.Range(0f, 10f);
        excitation = 0f;        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            excitation = 40f;
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, -wind*2f + Mathf.Sin(offset + Time.time * 10f) * (wind + excitation));
        excitation = excitation / 1.01f;
        if (excitation < 1f) {
            excitation = 1f;
        }
    }
}
