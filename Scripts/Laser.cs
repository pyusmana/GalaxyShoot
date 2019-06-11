using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{ 
    [SerializeField]
    private float _v = 10.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _v);
        if(transform.position.y>5.6f)
        {
            Destroy(this.gameObject);
        }
    }
   
}
