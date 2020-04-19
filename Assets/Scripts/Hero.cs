﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 150f;

    public bool IsMoving = false;
    public Vector2 move;
    
    void Start()
    {
        
    }

    void OnMove(InputValue value)
    {
        //IsMoving = true;
        move = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);

        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
    }
    private void LateUpdate()
    {
        IsMoving = false;
    }
}
