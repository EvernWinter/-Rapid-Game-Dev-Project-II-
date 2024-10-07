using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private int checkPointIndex = 999;
    [SerializeField] private GameObject checkpointLight;

    private void Start()
    {
        checkpointLight.SetActive(false);
    }

    private void Update()
    {
        if(CheckPointManager.Instance.currentCheckPointIndex == checkPointIndex)
        {
            checkpointLight.SetActive(true);
        }
        else
        {
            checkpointLight.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckPointManager.Instance.currentCheckPointIndex = checkPointIndex;
        }
    }
}
