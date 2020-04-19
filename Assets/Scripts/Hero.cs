using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 150f;

    public Vector2 move;

    public Animator animator;
    void Start()
    {
        
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();

        animator.SetInteger("MoveLeftRight", 0);
        animator.SetInteger("MoveUpDown", 0);

        if (move.x > 0)
        {
            animator.SetInteger("MoveLeftRight", 1);
        }
        else if(move.x < 0)
        {
            animator.SetInteger("MoveLeftRight", -1);
        }
        if(move.y > 0)
        {
            animator.SetInteger("MoveUpDown", 1);
        }
        else if (move.y < 0)
        {
            animator.SetInteger("MoveUpDown", -1);
        }
    }

    void FixedUpdate()
    {
        if(move.x == 0)
        {
            animator.SetInteger("MoveLeftRight", 0);
        }
        if (move.y == 0)
        {
            animator.SetInteger("MoveUpDown", 0);
        }
        GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);

        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
    }
    private void LateUpdate()
    {
    }
}
