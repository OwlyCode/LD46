using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    float offset;
    float excitation;

    float wind = 2f;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0f, 10f);
        excitation = 0f;        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {

            // A remplacer par le check de module
            if (!collision.collider.GetComponent<Hero>().hasAnyModule()) {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
                StartCoroutine("ForgetCollisionIgnore", collision);
            }

            excitation = 80f;
        }
    }

    IEnumerator ForgetCollisionIgnore(Collision collision) 
    {
        while (Vector3.Distance(collision.collider.transform.position, transform.position) < 0.8f) {
            yield return new WaitForSeconds(0.1f);
        }

        Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, false);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, -wind*2f + Mathf.Sin(offset + Time.time * 10f) * (wind + excitation));
        // transform.position += Vector3.right * ;
        excitation = excitation / 1.01f;
        if (excitation < 1f) {
            excitation = 1f;
        }
    }
}
