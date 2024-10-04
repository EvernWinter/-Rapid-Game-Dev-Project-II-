using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private GameObject lanternOn;
    [SerializeField] private GameObject lanternOff;

    public bool isLanternOn = false;
    public int lanternIndex = 999;

    void Start()
    {
        LanternOff();
    }

    void Update()
    {
        
    }

    public void LanternOn()
    {
        lanternOn.SetActive(true);
        lanternOff.SetActive(false);
        isLanternOn = true;
    }

    public void LanternOff()
    {
        lanternOff.SetActive(true);
        lanternOn.SetActive(false);
        isLanternOn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            LanternOn();
        }
    }
}
