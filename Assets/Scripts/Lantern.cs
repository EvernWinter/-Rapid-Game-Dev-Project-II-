using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private GameObject lanternOn;
    [SerializeField] private GameObject lanternOff;

    [SerializeField] private bool isLanternOn = false;
    public bool IsLanternOn => isLanternOn;
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
            if(GameManager.instance.CheckIfAllLanternIgnitedCorrectly())
            {
                GameManager.instance.isLanternTurnOnCorrectly = true;
            }
            else
            {
                GameManager.instance.isLanternTurnOnCorrectly = false;
            }
        }
    }
}
