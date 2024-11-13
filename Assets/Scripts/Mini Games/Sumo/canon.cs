using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour
{
    [SerializeField] GameObject[] _projectiles;

    [SerializeField] Transform _shootPos;

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
        switch (Random.Range(0, incomingRandomNumber))
        {           
            case 0:
            Debug.Log("Apple");
            chosenFruit = Instantiate(_projectiles[0], _shootPos.position, Quaternion.identity);
            break;
        case 1:
            Debug.Log("Orange");
            chosenFruit = Instantiate(_projectiles[1], _shootPos.position, Quaternion.identity);
            break;
        case 2:
            Debug.Log("Blueberry");
            chosenFruit = Instantiate(_projectiles[2], _shootPos.position, Quaternion.identity);
            break;
        case 3:
            Debug.Log("Watermelon");
            chosenFruit = Instantiate(_projectiles[3], _shootPos.position, Quaternion.identity);
            break;
        default:
            Debug.Log("Person!");
            break;
        }
    }
}
