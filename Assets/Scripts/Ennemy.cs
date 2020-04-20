using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
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
        GetComponentInChildren<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);   

        GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Keep").transform.position);
    }
}
