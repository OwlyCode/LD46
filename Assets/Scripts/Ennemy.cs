using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    public Vector3 velocity;
    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        previousPosition = transform.position;

        GetComponentInChildren<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;

        previousPosition = transform.position;

        GetComponentInChildren<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);   

        GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("Keep").transform.position);
    }
}
