using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.collider.gameObject.GetComponent<Hero>().HordeDamage();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Keep").transform.position);
    }
}
