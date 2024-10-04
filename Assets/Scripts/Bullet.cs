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
            // Check if the element type matches
            if (element == other.GetComponent<ElementType>())
            {
                // Add logic if the element types match
            }
            else
            {
                Destroy(gameObject); // Destroy bullet if elements don't match
            }
        }

        if (other.CompareTag("Lantern"))
        {
            Destroy(gameObject); // Destroy bullet if it hits a lantern
        }
    }

    // Called when the object is no longer visible by any camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy bullet when it goes out of camera view
    }
}
