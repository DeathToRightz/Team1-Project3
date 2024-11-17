using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
   public float speed = 1.0f;
   public float amount  = 1.0f;
    Vector3 _newPos;
    Vector3 _startingPos;
    private void Awake()
    {
       
     _startingPos = transform.position;
    }

    void Update()
    {

        _newPos.x = _startingPos.x + Mathf.Cos(Time.time * speed) * amount;
        
        _newPos.y =  _startingPos.y;
        //_newPos.y =  _startingPos.y + Mathf.Sin(Time.time);
     
        _newPos.z = _startingPos.z;
        //_newPos.z = _startingPos.z + Mathf.Cos(Time.time);

       transform.position = _newPos;
    }
}
