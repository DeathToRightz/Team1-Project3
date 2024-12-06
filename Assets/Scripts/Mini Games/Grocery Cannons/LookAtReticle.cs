using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class LookAtReticle : MonoBehaviour
{
    
    [SerializeField] public bool canShoot;
    [SerializeField] public float powa = 100f;
    [SerializeField] public Transform target;
    [SerializeField] public ParticleSystem _smokeParticle;
    [SerializeField] public GameObject _projectile;
    private Vector3 _direction;
    [SerializeField] Transform _shootPoint;
    [SerializeField] public PlayableDirector groceryCannondirector;
    private Vector3 _newDirection;
    private Quaternion _desiredRotation;
    void Update()
    {
        _newDirection = (target.position - transform.position);
        _desiredRotation = Quaternion.LookRotation(_newDirection, Vector3.up);


        transform.rotation = _desiredRotation * Quaternion.Euler(0, 182, 0);
        
       
    }

    private void Start()
    {
        canShoot = true;
        //Quaternion.LookRotation(-Vector3.forward, Vector3.up);
    }

    public void ShootCannon()
    {
        _direction = target.position - transform.position;
        if (canShoot == true)
        {
            //Debug.Log("Player Shot");
            
            StartCoroutine(Shoot(this._projectile));
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
        _smokeParticle.Play();
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
        yield return null;

    }
    
}
