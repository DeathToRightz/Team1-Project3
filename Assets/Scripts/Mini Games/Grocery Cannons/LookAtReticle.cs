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
        _newDirection = (target.position - transform.position).normalized;
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
        if (canShoot == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(_shootPoint.position, (target.position - _shootPoint.position).normalized, out hit))
            {
                _direction = (hit.point - target.position).normalized;
                Debug.DrawRay(_shootPoint.position, _direction * 100f, Color.red, 2f);
                //Debug.Log("Raycast Hit target.");
                Debug.Log("Found target");

            }
            else
            {
                _direction = target.position - transform.position;

                //Debug.DrawRay(_shootPoint.position, _direction * 100f, Color.yellow, 2f);
               //Debug.LogError("Raycast did not hit anything.");
            }
            //Debug.Log("Player Shot");
            
            StartCoroutine(Shoot(this._projectile));
        }
    }

    private IEnumerator Shoot(GameObject incomingGameObject)
    {
        canShoot = false;
        groceryCannondirector.Play();
       
        yield return new WaitForSeconds(1.2f);
        //GameObject projectile = null;

        GameObject projectile = Instantiate(incomingGameObject,_shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(_direction * powa * 100);

        _smokeParticle.Play();
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
        yield return null;

    }

}
