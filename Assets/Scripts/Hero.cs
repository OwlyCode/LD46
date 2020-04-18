using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{   
    const float speed = 50f;
    public bool IsMoving = false;
    public Vector2 move;
    
    void Start()
    {
        
    }

    void OnMove(InputValue value)
    {
        IsMoving = true;
        move = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = move * Time.fixedDeltaTime * speed;        
    }
    private void LateUpdate()
    {
        //IsMoving = false;
    }
}
