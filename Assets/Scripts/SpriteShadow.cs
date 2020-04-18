using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteShadow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
