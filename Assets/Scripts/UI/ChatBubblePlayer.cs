using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatBubbleForPlayer
{
    public List<string> chatString;
}

public class ChatBubblePlayer : MonoBehaviour
{
    [Header("ChatBubbleHitBox")]
    [SerializeField] private GameObject chatBubbleHitBox;

    [Header("Chatlist")]
    [SerializeField] private GameObject chatBubblePrefab;
    [SerializeField] private GameObject chatBubbleParent;

    private bool isShowTextFinished = false;
    private bool isHitBoxCollideOnce = false;

    [SerializeField] private List<string> chatString = new List<string>();

    void Start()
    {

    }

    void Update()
    {
        if (!isHitBoxCollideOnce && chatBubbleHitBox.GetComponent<HitBoxChatBubble>().isCollideWithObject)
        {
            isHitBoxCollideOnce = true;
            InstantiateChatBubble();
        }
    }

    private void InstantiateChatBubble()
    {
        GameObject chatBubble = Instantiate(chatBubblePrefab, chatBubbleParent.transform);
        ChatBubble controller = chatBubble.GetComponent<ChatBubble>();
        controller.chatString = chatString;
        isShowTextFinished = controller.SetText(0);
    }
}
