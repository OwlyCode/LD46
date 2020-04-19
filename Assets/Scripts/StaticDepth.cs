using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDepth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int) -(transform.position.z * 1000);   
    }
}
