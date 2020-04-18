using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 100f;

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
        GetComponent<Rigidbody>().velocity = new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
    }
}
