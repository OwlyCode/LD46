using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform camera = Camera.main.gameObject.transform;


        transform.rotation = Quaternion.Euler(camera.rotation.eulerAngles.x, 0, 0);
    }
}
