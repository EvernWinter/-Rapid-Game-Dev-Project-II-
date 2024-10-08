using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    [SerializeField] private Text chatText;
    public List<string> chatString;
    [SerializeField] private Font customFont;
    private string currentText = "";

    void Start()
    {
        if (customFont != null)
        {
            chatText.font = customFont; // Set the font to the custom one
        }
    }

    void Update()
    {
        
    }

    public bool SetText(int index)
    {
        chatText.text = $"{chatString[index]}";
        StartCoroutine(ShowText(index));
        return true;
    }

    private IEnumerator ShowText(int index)
    {
        //SoundManager.instance.TypingTextSound(true);

        for (int i = 0; i <= chatString[index].Length; i++)
        {
            currentText = chatString[index].Substring(0, i);
            chatText.text = currentText;
            yield return new WaitForSeconds(0.03f);
        }

        //SoundManager.instance.TypingTextSound(false);
        yield return new WaitForSeconds(1f);

        if (index + 1 < chatString.Count)
        {
            StartCoroutine(ShowText(index + 1));
        }
        else
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

    }
}
