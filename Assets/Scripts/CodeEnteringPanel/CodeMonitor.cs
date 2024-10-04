using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMonitor : MonoBehaviour
{
    [SerializeField] private bool _playerIsInRange;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerIsInRange && Input.GetKeyDown(KeyCode.E))
        {
            CodeEnteringPanel.Instance.Show();
        }
    }
    
    
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerIsInRange = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerIsInRange = false;
        }
    }
    
}
