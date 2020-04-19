using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    const float speed = 150f;

    const float jumpUpForce = 200f;
    const float jumpSideForce = 70f;

    const float groundedRaycastDistance = 0.8f;

    const float groundedCastVerticalOffset = 0.4f;

    public bool IsMoving = false;
    public Vector2 move;
    
    public Vector2 lastMovement; // Last non null movement

    bool grounded = true;

    void Start()
    {
        
    }

    void OnMove(InputValue value)
    {
        //IsMoving = true;
        move = value.Get<Vector2>();

        if (move.x != 0 || move.y != 0) {
            lastMovement = move.normalized;
        }
    }

    void FixedUpdate()
    {
        bool grounded = Physics.Raycast(transform.position + Vector3.up * groundedCastVerticalOffset, Vector3.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position +  Vector3.up * groundedCastVerticalOffset, Vector3.down * groundedRaycastDistance, Color.yellow);

        if (grounded == false && this.grounded)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(new Vector3(lastMovement.x * jumpSideForce, jumpUpForce, lastMovement.y * jumpSideForce));
        }

        this.grounded = grounded;


        GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);

        if (grounded) {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
        }
    }
    private void LateUpdate()
    {
        IsMoving = false;
    }
}
