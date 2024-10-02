using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private GameObject lanternOn;
    [SerializeField] private GameObject lanternOff;

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
    }

    public void LanternOff()
    {
        lanternOff.SetActive(true);
        lanternOn.SetActive(false);
    }
}
