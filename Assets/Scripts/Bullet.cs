using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Element
{
    [SerializeField] private ElementType element;
    // Start is called before the first frame update
    void Start()
    {
        element = ElementType.Fire;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Element Object"))
        {
            //Coding Here
            if (element == other.GetComponent<ElementType>())
            {
                
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
    }
}
