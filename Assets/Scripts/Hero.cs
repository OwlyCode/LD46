using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Cinemachine;

public class Hero : MonoBehaviour
{
    const float speed = 150f;

    const float jumpUpForce = 250f;
    const float jumpSideForce = 150f;

    const float groundedRaycastDistance = 1f;

    const float groundedCastVerticalOffset = 0.4f;

    const float maxSickness = 100f;

    const float sicknessDecay = 12f;

    const float knockbackStrength = 150f;

    public bool IsMoving = false;

    public Vector2 move;
    
    public Vector2 lastMovement; // Last non null movement

    public CinemachineVirtualCamera baseCamera;

    public CinemachineVirtualCamera watchtowerCamera;

    public Color sicknessColor;

    bool grounded = true;

    public bool knockback = false;

    bool dead = false;

    public bool hasObservatory = false;
    public bool hasHelmet = false;
    public bool hasWall = false;
    public bool hasMill = false;
    public bool hasWatchTower = false;

    public bool damagedHelmet = false;

    public Animator animator;

    public PhysicMaterial slippery;
    public PhysicMaterial sticky;

    Vector3 windModifier = Vector3.zero;

    GameObject mill;
    GameObject helmet;
    GameObject wall;
    GameObject watchtower;
    GameObject observatory;

    float speedMultiplier = 1f;

    float sickness = 0f;
    bool sicknessIncreased = false;

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

    public void ApplySpeedModifier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }

    public void IncreaseSickness(float amount)
    {
        this.sickness += amount;
        sickness = Mathf.Clamp(sickness, 0, maxSickness);
        sicknessIncreased = true;
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

    public void HordeDamage(Vector3 ennemyVelocity)
    {
        if (knockback) {
            return;
        }

        if (!hasAnyModule()) {
            Die();

            return;
        }

        knockback = true;
        StartCoroutine(KnockbackCooldown(0.5f));
        SetPhysicMaterial(slippery);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(0f, knockbackStrength, 0f));
        GetComponent<Rigidbody>().AddForce(ennemyVelocity.normalized * knockbackStrength * 2f);

        LoseModule();
    }

    void SetPhysicMaterial(PhysicMaterial material)
    {
        foreach(Collider c in GetComponents<Collider>()) {
            c.material = material;
        }

        foreach(Collider c in GetComponentsInChildren<Collider>()) {
            c.material = material;
        }
    }

    IEnumerator KnockbackCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetPhysicMaterial(sticky);

        knockback = false;
    }

    public void LoseModule()
    {
        if (hasHelmet) {
            if (damagedHelmet) {
                hasHelmet = false;
            } else {
                damagedHelmet = true;
            }

            return;
        }

        string[] modules = new []{hasObservatory ? "observatory" : null, hasWall ? "wall" : null, hasMill ? "mill" : null, hasWatchTower ? "watchtower": null};
        List<string> existingModules = (from item in modules where item != null select item).ToList();

        string destroyed = existingModules[Random.Range(0, existingModules.Count - 1)];

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

    void Die()
    {
        dead = true;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().Reboot(2f);
    }

    void OnWaterTouch()
    {
        if (hasWall) {
            return;
        }

        foreach(Collider c in GetComponents<Collider>()) {
            c.enabled = false;
        }

        foreach(Collider c in GetComponentsInChildren<Collider>()) {
            c.enabled = false;
        }

        Die();
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

    void UpdateSickness()
    {
        if (hasAnyModule() && sickness == maxSickness) {
            LoseModule();
            sickness = 0f;
        }

        if (!sicknessIncreased) {
            sickness -= sicknessDecay * Time.fixedDeltaTime;
        }

        sicknessIncreased = false;

        sickness = Mathf.Clamp(sickness, 0, maxSickness);

        Color color = Color.Lerp(Color.white, sicknessColor, sickness / maxSickness);

        foreach(SpriteRenderer r in GetComponents<SpriteRenderer>()) {
            r.color = color;
        }

        foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>()) {
            r.color = color;
        }
    }

    void FixedUpdate()
    {
        UpdateSickness();

        mill.SetActive(hasMill);
        helmet.SetActive(hasHelmet);
        wall.SetActive(hasWall);
        watchtower.SetActive(hasWatchTower);
        observatory.SetActive(hasObservatory);

        baseCamera.Priority = hasWatchTower ? 5 : 20;

        bool grounded = Physics.Raycast(transform.position + Vector3.up * groundedCastVerticalOffset, Vector3.down, groundedRaycastDistance, LayerMask.GetMask("Ground"));

        Debug.DrawRay(transform.position +  Vector3.up * groundedCastVerticalOffset, Vector3.down * groundedRaycastDistance, Color.yellow);

        if (!grounded && this.grounded && !dead && !knockback)
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

        if (grounded && !dead && !knockback) {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, currentVelocity.y, 0f) + new Vector3(move.x, 0f, move.y) * Time.fixedDeltaTime * speed * speedMultiplier;
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
