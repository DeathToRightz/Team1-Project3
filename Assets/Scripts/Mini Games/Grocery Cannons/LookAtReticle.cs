using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtReticle : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //transform.TransformDirection(Vector3.forward);
        transform.LookAt(target);
    }
}
