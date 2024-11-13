using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] int _fallCounter;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _fallCounter = Random.Range(0, 15);
    }
    private void Start()
    {
        StartCoroutine(Drop(_fallCounter));
    }
    IEnumerator Drop(int incomingDelay)
    {
        yield return new WaitForSeconds(incomingDelay);
        _rb.isKinematic = false;
        Destroy(gameObject, 5);
    }
}
