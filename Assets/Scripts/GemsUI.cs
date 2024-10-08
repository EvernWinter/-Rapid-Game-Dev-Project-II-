using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsUI : MonoBehaviour
{
    [SerializeField] private Image image_GemRed;
    [SerializeField] private Image image_GemBlue;
    [SerializeField] private Image image_GemGreen;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance.isCollectGemStone_Red)
        {
            image_GemRed.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image_GemRed.color = new Color(1f, 1f, 1f, 0.1f);
        }

        if (GameManager.instance.isCollectGemStone_Blue)
        {
            image_GemBlue.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image_GemBlue.color = new Color(1f, 1f, 1f, 0.1f);
        }

        if (GameManager.instance.isCollectGemStone_Green)
        {
            image_GemGreen.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image_GemGreen.color = new Color(1f, 1f, 1f, 0.1f);
        }
    }
}
