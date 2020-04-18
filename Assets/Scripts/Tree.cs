using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    float offset;
    float excitation;

    float wind = 2f;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0f, 1f);
        excitation = 0f;        
    }

    void OnCollisionEnter(Collision collision)
    {
        excitation = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, -10f + Mathf.Sin(offset + Time.time * 10f) * (wind + excitation));
        // transform.position += Vector3.right * ;
        excitation = excitation / 1.01f;
        if (excitation < 1f) {
            excitation = 1f;
        }
    }
}
