using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroupBlackFadePanel;
    [SerializeField] private CanvasGroup canvasGroupMainMenu;
    [SerializeField] private CanvasGroup canvasGroupHowToPlay;
    [SerializeField] private CanvasGroup canvasGroupStory;
    [SerializeField] private CanvasGroup canvasGroupQuit;

    [Header("Panel")]
    [SerializeField] private CanvasGroup panel_GameName;

    [Header("Button_MainMenu")]
    [SerializeField] private CanvasGroup button_Start;
    [SerializeField] private CanvasGroup button_HowToPlay;
    [SerializeField] private CanvasGroup button_Story;
    [SerializeField] private CanvasGroup button_Quit;

    void Start()
    {
        SetUpUIOnStart();
        StartCoroutine(DoFadeOnStartUpCoroutine());
    }

    public void StartButton()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupMainMenu, canvasGroupBlackFadePanel));
        Invoke("OnStart", 1f);
    }

    public void HowToPlayButton()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupMainMenu, canvasGroupHowToPlay));
    }

    public void StoryButton()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupMainMenu, canvasGroupBlackFadePanel));
        Invoke("OnVideoScene", 1f);
    }

    public void QuitButton()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupMainMenu, canvasGroupQuit));
    }

    public void CancelQuit()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupQuit, canvasGroupMainMenu));
    }
    public void ConfirmQuit()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupQuit, canvasGroupBlackFadePanel));
        Invoke("OnQuit", 1f);
    }

    public void BackToMainmenu_Story()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupStory, canvasGroupMainMenu));
    }

    public void BackToMainmenu_HowToPlay()
    {
        //SoundManager.Instance.ClickingUISound();
        StartCoroutine(DoFadeCanvasGroupCoroutine(canvasGroupHowToPlay, canvasGroupMainMenu));
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    private void OnVideoScene()
    {
        SceneManager.LoadScene("VideoScene");
    }

    private IEnumerator DoFadeCanvasGroupCoroutine(CanvasGroup curCanvasGroup, CanvasGroup nextCanvasGroup)
    {
        curCanvasGroup.DOFade(0f, 0.5f);
        curCanvasGroup.interactable = false;
        curCanvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(0.5f);
        nextCanvasGroup.DOFade(1f, 0.5f);
        nextCanvasGroup.interactable = true;
        nextCanvasGroup.blocksRaycasts = true;
    }

    private IEnumerator DoFadeCanvasGroupCoroutine(CanvasGroup curCanvasGroup)
    {
        curCanvasGroup.DOFade(0f, 0.5f);
        curCanvasGroup.interactable = false;
        curCanvasGroup.blocksRaycasts = false;
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator DoFadeOnStartUpCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canvasGroupBlackFadePanel.DOFade(0f, 1f);

        yield return new WaitForSeconds(1f);
        panel_GameName.DOFade(1f, 0.5f);
        canvasGroupMainMenu.interactable = true;
        canvasGroupMainMenu.blocksRaycasts = true;

        yield return new WaitForSeconds(0.2f);
        button_Start.DOFade(1f, 0.5f);
        button_Start.interactable = true;
        button_Start.blocksRaycasts = true;

        yield return new WaitForSeconds(0.2f);
        button_HowToPlay.DOFade(1f, 0.5f);
        button_HowToPlay.interactable = true;
        button_HowToPlay.blocksRaycasts = true;

        yield return new WaitForSeconds(0.2f);
        button_Story.DOFade(1f, 0.5f);
        button_Story.interactable = true;
        button_Story.blocksRaycasts = true;

        yield return new WaitForSeconds(0.2f);
        button_Quit.DOFade(1f, 0.5f);
        button_Quit.interactable = true;
        button_Quit.blocksRaycasts = true;
    }

    private void SetUpUIOnStart()
    {
        panel_GameName.alpha = 0f;

        button_Start.alpha = 0f;
        button_HowToPlay.alpha = 0f;
        button_Story.alpha = 0f;
        button_Quit.alpha = 0f;

        canvasGroupMainMenu.alpha = 1f;
        canvasGroupStory.alpha = 0f;
        canvasGroupHowToPlay.alpha = 0f;
        canvasGroupBlackFadePanel.alpha = 1f;
        canvasGroupQuit.alpha = 0f;

        button_Start.interactable = false;
        button_HowToPlay.interactable = false;
        button_Story.interactable = false;
        button_Quit.interactable = false;

        canvasGroupMainMenu.interactable = false;
        canvasGroupStory.interactable = false;
        canvasGroupHowToPlay.interactable = false;
        canvasGroupBlackFadePanel.interactable = false;
        canvasGroupQuit.interactable = false;

        button_Start.blocksRaycasts = false;
        button_HowToPlay.blocksRaycasts = false;
        button_Story.blocksRaycasts = false;
        button_Quit.blocksRaycasts = false;

        canvasGroupMainMenu.blocksRaycasts = false;
        canvasGroupStory.blocksRaycasts = false;
        canvasGroupHowToPlay.blocksRaycasts = false;
        canvasGroupBlackFadePanel.blocksRaycasts = false;
        canvasGroupQuit.blocksRaycasts = false;
    }
}
