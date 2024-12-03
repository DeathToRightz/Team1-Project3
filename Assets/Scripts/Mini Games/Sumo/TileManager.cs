using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> _tiles = new List<GameObject>();
    [SerializeField] Transform _tilesParent;
    DeathPit deathPit;
    private void Awake()
    {
        deathPit = FindObjectOfType<DeathPit>();
        foreach (Transform child in _tilesParent)
        {
            _tiles.Add(child.transform.gameObject);
        }
    }

    private void Start()
    {
        
           StartCoroutine(AddFallScript(_tiles)); 
        
    }

    private void Update()
    {
        if (deathPit.isGameOver)
        {
            Destroy(this);
        }
    }


    IEnumerator AddFallScript(List<GameObject> incomingList)
    {
        while (true)
        {
            int tileIndex = Random.Range(0, incomingList.Count);
            Debug.Log("Tile index is " + tileIndex);
            if (incomingList[tileIndex] == null) { Debug.Log("Tile not here"); }
            if (incomingList[tileIndex].GetComponent<Falling_Platform>() == null) { Debug.Log("Add script"); incomingList[tileIndex].AddComponent<Falling_Platform>(); incomingList.Remove(incomingList[tileIndex]); }
            else { Debug.Log("Dont add script"); }
            yield return new WaitForSeconds(2.5f);
        }
       
    }

}
