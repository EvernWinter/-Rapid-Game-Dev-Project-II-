using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformSnapper : MonoBehaviour
{
    public static FloatingPlatformSnapper Instance;

    [field: SerializeField] public bool _isSnapped { get; private set; }
    [SerializeField] private GameObject _snapPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void CheckSnapStone(GameObject gameObject)
    {
        _isSnapped = true;
        
        gameObject.GetComponent<Block>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
        
        // Snap the stone to the snap point
        gameObject.transform.position = _snapPosition.transform.position; // Snap directly to the position
        gameObject.transform.rotation = Quaternion.identity; // Reset rotation if needed
        gameObject.transform.SetParent(_snapPosition.transform); // Set the stone as a child of the snap point
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isSnapped)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Object"))
        {
            CheckSnapStone(other.gameObject);
        }
    }
}
