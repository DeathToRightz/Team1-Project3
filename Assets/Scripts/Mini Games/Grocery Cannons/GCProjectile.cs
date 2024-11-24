using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCProjectile : MonoBehaviour
{
    public Transform target;
    private Vector3 _directon;

    // Start is called before the first frame update
    void Start()
    {
        _directon = target.position - transform.position;
    }
}
