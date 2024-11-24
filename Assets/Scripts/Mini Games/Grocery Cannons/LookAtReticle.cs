using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Windows;

public class LookAtReticle : MonoBehaviour
{
    [SerializeField] public bool canShoot;
    [SerializeField] public float powa = 100f;
    [SerializeField] public Transform target;
    [SerializeField] public ParticleSystem _smokeParticle;
    [SerializeField] public GameObject _projectile;
    [SerializeField] private Vector3 _direction;
    [SerializeField] Transform _shootPoint;
    [SerializeField] public PlayableDirector groceryCannondirector;

    void Update()
    {
        transform.LookAt(target);
    }

    private void Start()
    {
        canShoot = true;
    }

    public void ShootCannon()
    {
        _direction = target.position - transform.position;
        if (canShoot == true)
        {
            //Debug.Log("Player Shot");
            _smokeParticle.Play();
            StartCoroutine(Shoot(_projectile));
        }
    }

    private IEnumerator Shoot(GameObject incomingGameObject)
    {
        canShoot = false;
        groceryCannondirector.Play();
        yield return new WaitForSeconds(1.2f);
        GameObject projectile = null;
        projectile = Instantiate(incomingGameObject,_shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(_direction * powa);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
        yield return null;

    }
}
