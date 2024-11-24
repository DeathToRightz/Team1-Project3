using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class canon : MonoBehaviour
{
    [SerializeField] GameObject[] _projectiles;

    [SerializeField] Transform _shootPos;

    [SerializeField] float _shootPower;

   // [SerializeField] GameObject[] _players;
    public List<GameObject> _players;

    [SerializeField] public Animator cannonAnim;

    [SerializeField] public PlayableDirector cannonDirector;

    [SerializeField] float _fireRate = 5f;

    [SerializeField] UnityEvent<bool>  displaySightLinesEvent = new UnityEvent<bool>();

    [SerializeField] bool _showSightLines;
    DeathPit deathPit;

    public bool shootAtPlayers;

    private void Awake()
    {
        LookForPlayers(_players);
    }
    private void Start()
    {
        deathPit = FindObjectOfType<DeathPit>();
        LookForPlayers(_players);
        StartCoroutine(SpawnProjectile(_projectiles));     
    }
    private void Update()
    {
        displaySightLinesEvent.Invoke(_showSightLines);
    }

    IEnumerator SpawnProjectile(GameObject[] incomingArray)
    {
        yield return new WaitForSeconds(20f);
        cannonAnim.SetTrigger("StartCannons");
        yield return new WaitForSeconds(1f);
        GameObject chosenFruit = null;
        
        while(true)
        {
            yield return new WaitForSeconds(_fireRate);
            switch (Random.Range(0, incomingArray.Length))
            {
                case 0:
                    //Debug.Log("Apple");
                    cannonDirector.Play();
                    yield return new WaitForSeconds(1.2f);
                    chosenFruit = Instantiate(incomingArray[0], _shootPos.position, Quaternion.identity);
                    LookAtPlayers(_shootPos, _players, shootAtPlayers);
                    ShootProjectile(chosenFruit);
                    break;
                case 1:
                    //Debug.Log("Orange");
                    cannonDirector.Play();
                    yield return new WaitForSeconds(1.2f);
                    chosenFruit = Instantiate(incomingArray[1], _shootPos.position, Quaternion.identity);
                    LookAtPlayers(_shootPos, _players, shootAtPlayers);
                    ShootProjectile(chosenFruit);
                    break;
                case 2:
                    //Debug.Log("Blueberry");
                    cannonDirector.Play();
                    yield return new WaitForSeconds(1.2f);
                    chosenFruit = Instantiate(incomingArray[2], _shootPos.position, Quaternion.identity);
                    LookAtPlayers(_shootPos, _players, shootAtPlayers);
                    ShootProjectile(chosenFruit);
                    break;
                case 3:
                    //Debug.Log("Watermelon");
                    cannonDirector.Play();
                    yield return new WaitForSeconds(1.2f);
                    chosenFruit = Instantiate(incomingArray[3], _shootPos.position, Quaternion.identity);
                    LookAtPlayers(_shootPos, _players, shootAtPlayers);
                    ShootProjectile(chosenFruit);
                    break;
                default:
                    Debug.Log("Person!");
                    break;
            }
        }

    }

    private void ShootProjectile(GameObject incomingObject)
    {
       if (!deathPit.isGameOver)
        {
            
            Rigidbody _rb = incomingObject.GetComponent<Rigidbody>();
            _rb.AddForce(_shootPos.forward * _shootPower);
        }
    }

    private void LookAtPlayers(Transform incomingTransform, List<GameObject> incomingTargets, bool incomingBool)
    {
        if (incomingBool)
        {
            switch (Random.Range(0, incomingTargets.Count))
            {
                case 0:
                    incomingTransform.LookAt(incomingTargets[0].transform);
                    break;
                case 1:
                    incomingTransform.LookAt(incomingTargets[1].transform);
                    break;
                default:
                    Debug.LogError("Out of bounds");
                    break;
            }
        }
        


    }

    public void DisplaySightLines(bool incomingBool)
    {
        if (incomingBool)
        {
            Debug.DrawLine(_shootPos.position, _players[0].transform.position, Color.blue);
            Debug.DrawLine(_shootPos.position, _players[1].transform.position, Color.red);
        }
        
    }

    public void LookForPlayers(List<GameObject> incomingArray)
    {
        RABInput[] list = FindObjectsOfType<RABInput>();

        for (int i = 0; i <= list.Length-1; i++)
        {
            incomingArray.Add(list[i].gameObject);
        }

    }
   

}
