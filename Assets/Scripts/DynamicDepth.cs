using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDepth : MonoBehaviour
{
    public bool isInFront = true;

    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + (isInFront ? 1 : -1);
    }
}
