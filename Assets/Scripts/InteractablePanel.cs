using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InteractablePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private bool isBlinkingText;
    [SerializeField] private TextMeshProUGUI textEscape;
    [SerializeField] private float alpha;
    [SerializeField] private bool isBlinkingUp = false;

    [SerializeField] private bool isShowingText;

    private void Start()
    {
        _panel.SetActive(false);
    }
    public void Update()
    {
        textEscape.color = new Color(0f, 0f, 0f, alpha);

        if (isShowingText)
        {
            if (!isBlinkingUp)
            {
                alpha -= 1f * Time.deltaTime;
            }
            else if (isBlinkingUp)
            {
                alpha += 1f * Time.deltaTime;
            }

            if (alpha >= 1)
            {
                isBlinkingUp = false;
            }
            else if (alpha <= 0)
            {
                isBlinkingUp = true;
            }


            //StartCoroutine(BlinkingTextFlashlight());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    // Make sure this method is public
    public void Show()
    {
        _panel.SetActive(true);
        isShowingText = true;
    }

    // Make sure this method is public
    public void Hide()
    {
        _panel.SetActive(false);
        isShowingText = false;
    }

    private IEnumerator BlinkingTextFlashlight()
    {
        isBlinkingText = false;
        if (alpha == 0f)
        {
            textEscape.DOFade(1f, 0.5f);
        }

        if (alpha == 1f)
        {
            textEscape.DOFade(0f, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        if (isShowingText)
        {
            alpha = 0f;
        }

        isBlinkingText = true;
    }
}
