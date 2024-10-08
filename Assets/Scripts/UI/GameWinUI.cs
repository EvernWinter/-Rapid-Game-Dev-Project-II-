using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup gameWinCanvasGroup;
    [SerializeField] private bool isGameWin = false;

    void Start()
    {
        gameWinCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && isGameWin)
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }

    public void DoFadeGameWinUI()
    {
        isGameWin = true;
        gameWinCanvasGroup.DOFade(1f, 0.5f);
    }
}
