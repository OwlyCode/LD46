using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

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

    public bool hasObservatory = false;
    public bool hasHelmet = false;
    public bool hasWall = false;
    public bool hasMill = false;
    public bool hasWatchTower = false;

    bool damagedHelmet = false;

    public Animator animator;
    void Start()
    {
        
    }

    public void LoseModule()
    {
        if (!hasAnyModule()) {
            // TODO: gameover ! 
        }

        if (hasHelmet) {
            if (damagedHelmet) {
                hasHelmet = false;
            } else {
                hasHelmet = false;
            }
        }

        string[] modules = new []{"observatory", "wall", "mill", "watchtower"};
        List<string> existingModules = (from item in modules where item != "" select item).ToList();

        string destroyed = existingModules[Random.Range(0, existingModules.Count)];

        switch (destroyed) {
            case "observatory":
                hasObservatory = false;
            break;
            case "wall":
                hasWall = false;
            break;
            case "mill":
                hasMill = false;
            break;
            case "watchtower":
                hasWatchTower = false;
            break;
        }
    }

    public bool hasAnyModule()
    {
        return hasObservatory || hasHelmet || hasWall || hasMill || hasWatchTower;
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();

        if (move.x != 0 || move.y != 0) {
            lastMovement = move.normalized;
        }

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
        bool grounded = Physics.Raycast(transform.position + Vector3.up * groundedCastVerticalOffset, Vector3.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position +  Vector3.up * groundedCastVerticalOffset, Vector3.down * groundedRaycastDistance, Color.yellow);

        if (grounded == false && this.grounded)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(new Vector3(lastMovement.x * jumpSideForce, jumpUpForce, lastMovement.y * jumpSideForce));
        }

        this.grounded = grounded;


        if(move.x == 0)
        {
            animator.SetInteger("MoveLeftRight", 0);
        }
        if (move.y == 0)
        {
            animator.SetInteger("MoveUpDown", 0);
        }

        GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);

        if (grounded) {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
        }
    }
    private void LateUpdate()
    {
    }
}
