using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    [SerializeField] private Vector3 spikeIdlePosition;
    [SerializeField] private Vector3 spikeActivatedPosition;

    [SerializeField] private bool isActivated = false;

    private void Update()
    {
        if(isActivated)
        {
            spike.transform.position = Vector3.MoveTowards(spike.transform.position, spikeActivatedPosition, 0.07f);
        }
        else
        {
            spike.transform.position = Vector3.MoveTowards(spike.transform.position, spikeIdlePosition, 0.07f);
        }

        if(spike.transform.position == spikeActivatedPosition)
        {
            isActivated = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }
}
