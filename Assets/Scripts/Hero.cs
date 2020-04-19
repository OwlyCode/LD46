using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Cinemachine;

public class Hero : MonoBehaviour
{
    const float speed = 150f;

    const float jumpUpForce = 200f;
    const float jumpSideForce = 70f;

    const float groundedRaycastDistance = 1f;

    const float groundedCastVerticalOffset = 0.4f;

    public bool IsMoving = false;

    public Vector2 move;
    
    public Vector2 lastMovement; // Last non null movement

    public CinemachineVirtualCamera baseCamera;

    public CinemachineVirtualCamera watchtowerCamera;

    bool grounded = true;

    bool dead = false;

    public bool hasObservatory = false;
    public bool hasHelmet = false;
    public bool hasWall = false;
    public bool hasMill = false;
    public bool hasWatchTower = false;

    bool damagedHelmet = false;

    public Animator animator;

    Vector3 windModifier = Vector3.zero;

    GameObject mill;
    GameObject helmet;
    GameObject wall;
    GameObject watchtower;
    GameObject observatory;

    void Start()
    {
        mill = transform.Find("Mill").gameObject;
        helmet = transform.Find("Helmet").gameObject;
        wall = transform.Find("Wall").gameObject;
        watchtower = transform.Find("Watchtower").gameObject;
        observatory = transform.Find("Observatory").gameObject;
    }

    public void ApplyWind(Vector3 windModifier)
    {
        this.windModifier = windModifier;
    }

    public void LoseAllModules()
    {
        hasObservatory = false;

        if (hasHelmet && !damagedHelmet) {
            damagedHelmet = true;
        } else {
            hasHelmet = false;
        }

        hasWall = false;
        hasMill = false;
        hasWatchTower = false;
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

            return;
        }

        string[] modules = new []{hasObservatory ? "observatory" : "", hasWall ? "wall" : null, hasMill ? "mill" : "", hasWatchTower ? "watchtower": ""};
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

    void OnWaterTouch()
    {
        if (hasWall) {
            return;
        }

        dead = true;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Reboot(2f);

        foreach(Collider c in GetComponents<Collider>()) {
            c.enabled = false;
        }

        foreach(Collider c in GetComponentsInChildren<Collider>()) {
            c.enabled = false;
        }
    }

    void OnToggleHelmet()
    {
        hasHelmet = true;
        damagedHelmet = false;
    }

    void OnToggleObservatory()
    {
        hasObservatory = true;
    }

    void OnToggleWall()
    {
        hasWall = true;
    }

    void OnToggleMill()
    {
        hasMill = true;
    }

    void OnToggleWatchtower()
    {
        hasWatchTower = true;
    }

    void OnMove(InputValue value)
    {
        if (dead) {
            move = Vector2.zero;
            return;
        }

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
        mill.SetActive(hasMill);
        helmet.SetActive(hasHelmet);
        wall.SetActive(hasWall);
        watchtower.SetActive(hasWatchTower);
        observatory.SetActive(hasObservatory);

        baseCamera.Priority = hasWatchTower ? 5 : 20;

        bool grounded = Physics.Raycast(transform.position + Vector3.up * groundedCastVerticalOffset, Vector3.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position +  Vector3.up * groundedCastVerticalOffset, Vector3.down * groundedRaycastDistance, Color.yellow);

        if (!grounded && this.grounded && !dead)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(new Vector3(lastMovement.x * jumpSideForce, jumpUpForce, lastMovement.y * jumpSideForce));
        }

        GetComponent<Rigidbody>().AddForce(windModifier);


        if (grounded && !this.grounded && !hasMill) {
            // Fall
            LoseAllModules();
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

        if (grounded && !dead) {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed;
        }

        if (dead) {
            GetComponent<Rigidbody>().velocity = new Vector3(lastMovement.x * 0.8f, -0.3f, lastMovement.y * 0.8f);

            if (transform.position.y < -0.5f) {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
            }
        }
    }
}
