using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _collider;
    [SerializeField] int _fallCounter;
    private float _speed = 20f;
    private float _amount = .1f;
    private Vector3 _startingPos;
    private Vector3 _newPos;
    private  bool shouldShake = false;
    DeathPit deathPit;

    private void Awake()
    {
        deathPit = FindObjectOfType<DeathPit>();
        _rb = GetComponent<Rigidbody>();
        _fallCounter = Random.Range(5, 10);
        _startingPos = transform.position;
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        StartCoroutine(Drop(_fallCounter));
    }
   void Update()
    {
      Shake(shouldShake);
    }
    IEnumerator Drop(int incomingDelay)
    {
        if (!deathPit.isGameOver)
        {
            yield return new WaitForSeconds(incomingDelay - 2);
            Debug.Log("Falling soon");
            shouldShake = true;


            yield return new WaitForSeconds(2);
            shouldShake = false;
            _collider.enabled = false;
            _rb.isKinematic = false;
            Destroy(gameObject, 5);
        }
    }

    private void Shake(bool incomingBool)
    {

        if (incomingBool)
        {
            _newPos.y = _startingPos.y;
            _newPos.z = _startingPos.z;
            _newPos.x = _startingPos.x + Mathf.Cos(Time.time * _speed) * _amount;
            transform.position = _newPos;
        }
        else
        {
            return;
        }
        
    }
}
