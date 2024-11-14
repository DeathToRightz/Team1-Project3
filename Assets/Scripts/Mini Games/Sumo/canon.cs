using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class canon : MonoBehaviour
{
    [SerializeField] GameObject[] _projectiles;

    [SerializeField] Transform _shootPos;

    [SerializeField] float _shootPower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot(_projectiles.Length); 

        }
    }


    private void Shoot(int incomingRandomNumber)
    {
        GameObject chosenFruit = null;
        
        Rigidbody _rb = null;
        
        switch (Random.Range(0, _projectiles.Length))
        {
            case 0:
                Debug.Log("Apple");
                chosenFruit = Instantiate(_projectiles[0], _shootPos.position,Quaternion.identity);
                _rb = chosenFruit.GetComponent<Rigidbody>();
                _rb.AddForce(_shootPos.forward * _shootPower);
               
                
                break;
            case 1:
                Debug.Log("Orange");
                chosenFruit = Instantiate(_projectiles[1], _shootPos.position, Quaternion.identity);
                _rb = chosenFruit.GetComponent<Rigidbody>();
                _rb.AddForce(_shootPos.forward * _shootPower);
                break;
            case 2:
                Debug.Log("Blueberry");
                chosenFruit = Instantiate(_projectiles[2], _shootPos.position, Quaternion.identity);
                _rb = chosenFruit.GetComponent<Rigidbody>();
                _rb.AddForce(_shootPos.forward * _shootPower);
                break;
            case 3:
                Debug.Log("Watermelon");
                chosenFruit = Instantiate(_projectiles[3], _shootPos.position, Quaternion.identity);
                _rb = chosenFruit.GetComponent<Rigidbody>();
                _rb.AddForce(_shootPos.forward * _shootPower);
                break;
            default:
                Debug.Log("Person!");
                break;
        }
    }
}
