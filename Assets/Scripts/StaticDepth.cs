using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDepth : MonoBehaviour
{
    public bool useParent = true;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int) -((useParent ? transform.parent.position.z : transform.position.z) * 100);   
    }
}
