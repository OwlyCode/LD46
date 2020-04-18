using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 50f;

    Vector2 move;

    void Start()
    {
        
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = move * Time.fixedDeltaTime * speed;
        //transform.position = transform.position + (Vector3) move * Time.fixedDeltaTime * speed;
    }
}
