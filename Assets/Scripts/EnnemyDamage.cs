using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDamage : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            collision.collider.gameObject.GetComponent<Hero>().HordeDamage(transform.parent.GetComponent<Ennemy>().velocity);
        }
    }
}
