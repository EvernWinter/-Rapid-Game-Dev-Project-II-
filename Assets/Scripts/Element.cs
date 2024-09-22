using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ElementType
{
    Fire,
    Water,
    Earth
}
public abstract class Element : MonoBehaviour
{
    [SerializeField] private ElementType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Element Object"))
        {
            //Coding Here
            if (this.GetComponent<ElementType>() == other.GetComponent<ElementType>())
            {
                
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        else if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
