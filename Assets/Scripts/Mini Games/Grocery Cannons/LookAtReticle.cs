using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtReticle : MonoBehaviour
{
    public float powa = 100f;
    public Transform target;
    public ParticleSystem _smokeParticle;
    public GameObject _projectile;
    private Vector3 _direction;
    public Transform _shootPoint;
    void Update()
    {
        _direction = target.position - transform.position;
        //transform.TransformDirection(Vector3.forward);
        transform.LookAt(target);

        if (Input.GetMouseButtonDown(0))
        {
            _smokeParticle.Play();
            Shoot(_projectile);
        }
    }

    private void Shoot(GameObject incomingGameObject)
    {
        GameObject projectile = null;
        projectile = Instantiate(incomingGameObject,_shootPoint);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(_direction * powa);

    }
}
