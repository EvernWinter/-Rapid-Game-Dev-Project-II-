using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject interactPanel;

    [SerializeField] private bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            interactText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                //interactPanel.SetActive(true);
                interactPanel.GetComponent<InteractablePanel>().Show();
            }
        }
        else
        {
            interactText.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
              
            canInteract = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
        
    }
}
