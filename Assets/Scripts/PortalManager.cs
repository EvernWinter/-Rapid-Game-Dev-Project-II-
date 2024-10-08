using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private bool isCollectRedGemOnce = false;
    [SerializeField] private bool isCollectBlueGemOnce = false;
    [SerializeField] private bool isCollectGreenGemOnce = false;

    [SerializeField] private GameObject[] portals = new GameObject[4];

    void Start()
    {
        portals[0].GetComponent<Portal>().isPass = true;
    }

    void Update()
    {
        if ((!isCollectRedGemOnce && GameManager.instance.isCollectGemStone_Red) || (Input.GetKeyDown(KeyCode.B)))
        {
            isCollectRedGemOnce = true;
            GameManager.instance.SetCutSceneState(2);
            portals[1].GetComponent<Portal>().isPass = true;
            portals[1].GetComponent<Portal>().OpenDoorMethod();
            portals[2].GetComponent<Portal>().isPass = true;
            portals[2].GetComponent<Portal>().OpenDoorMethod();

        }
        else if ((!isCollectGreenGemOnce && GameManager.instance.isCollectGemStone_Green) || (Input.GetKeyDown(KeyCode.N)))
        {
            isCollectGreenGemOnce = true;
            GameManager.instance.SetCutSceneState(3);
        }
        else if ((!isCollectBlueGemOnce && GameManager.instance.isCollectGemStone_Blue) || (Input.GetKeyDown(KeyCode.M)))
        {
            isCollectBlueGemOnce = true;
            GameManager.instance.SetCutSceneState(4);
            portals[3].GetComponent<Portal>().isPass = true;
            portals[3].GetComponent<Portal>().OpenDoorMethod();

        }
    }
}
