﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    public Vector3 velocity;
    Vector3 previousPosition;

    GameObject keep;

    float baseSpeed;

    float speedUpDistance = 10f;

    float speedUpRatio = 2f;

    // Start is called before the first frame update
    void Start()
    {
        keep = GameObject.Find("Keep");
        velocity = Vector3.zero;
        previousPosition = transform.position;

        baseSpeed = GetComponent<NavMeshAgent>().speed;
        GetComponentInChildren<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.collider.gameObject.GetComponent<Hero>().HordeDamage(GetComponent<Ennemy>().velocity);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.collider.gameObject.GetComponent<Hero>().HordeDamage(GetComponent<Ennemy>().velocity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = (transform.position - previousPosition) / Time.fixedDeltaTime;

        previousPosition = transform.position;

        GetComponentInChildren<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 100);   

        GetComponent<NavMeshAgent>().SetDestination(keep.transform.position);

        Hero h = keep.GetComponent<Hero>();

        if (h.knockback || h.dead) {
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }

        float distance = Vector3.Distance(keep.transform.position, transform.position);

        if (distance > speedUpDistance) {
            GetComponent<NavMeshAgent>().speed = baseSpeed * speedUpRatio;
        } else {
            GetComponent<NavMeshAgent>().speed = baseSpeed;
        }
    }
}
