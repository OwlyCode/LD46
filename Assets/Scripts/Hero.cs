using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 1f;

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
        transform.position = transform.position + (Vector3) move * Time.fixedDeltaTime * speed;
    }
}
