using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private enum CutSceneState { Null, CutSceneOnStart1, CutSceneOnStart2, CutSceneOnStart3, CutScene1, CutScene2, CutScene3, CutSceneEnd };
    [SerializeField] private bool isPlayCutSceneOnStart;
    [SerializeField] private CutSceneState cutSceneState;
    [SerializeField] private Animator camAnim;

    [SerializeField] private BoxCollider2D boxCollider2D;


    void Start()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        if (isPlayCutSceneOnStart)
        {
            Invoke(nameof(CutSceneOnStart), 1f);
        }
    }

    void Update()
    {

    }

    private void CutSceneOnStart()
    {
        cutSceneState = CutSceneState.CutSceneOnStart1;
        camAnim.SetBool("cutsceneonstart1", true);
        PlayerController.Instance.IsCutSceneOn = true;
        Invoke(nameof(ChangeCutscene), 1.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cutscene Detect player");
            if (cutSceneState == CutSceneState.CutScene1)
            {
                boxCollider2D.enabled = false;
                camAnim.SetBool("cutscene1", true);
                PlayerController.Instance.IsCutSceneOn = true;
            }
            else if (cutSceneState == CutSceneState.CutScene2)
            {
                boxCollider2D.enabled = false;
                camAnim.SetBool("cutscene2", true);
                PlayerController.Instance.IsCutSceneOn = true;
            }

            Invoke(nameof(ChangeCutscene), 2f);
        }
    }

    private void ChangeCutscene()
    {
        if (cutSceneState == CutSceneState.CutSceneEnd)
        {
            camAnim.SetBool("cutsceneEnd", false);
            PlayerController.Instance.IsCutSceneOn = false;
        }

        if (cutSceneState == CutSceneState.CutScene1)
        {
            camAnim.SetBool("cutscene1", false);
            PlayerController.Instance.IsCutSceneOn = false;
        }
        else if (cutSceneState == CutSceneState.CutScene2)
        {
            cutSceneState = CutSceneState.CutScene3;
            camAnim.SetBool("cutscene2", false);
            camAnim.SetBool("cutscene3", true);
            Invoke(nameof(ChangeCutscene), 1.2f);
            PlayerController.Instance.IsCutSceneOn = true;
        }
        else if (cutSceneState == CutSceneState.CutScene3)
        {
            cutSceneState = CutSceneState.CutSceneEnd;
            camAnim.SetBool("cutscene3", false);
            camAnim.SetBool("cutsceneEnd", true);
            Invoke(nameof(ChangeCutscene), 1.2f);
            PlayerController.Instance.IsCutSceneOn = true;
        }



        if (cutSceneState == CutSceneState.CutSceneOnStart1)
        {
            cutSceneState = CutSceneState.CutSceneOnStart2;
            camAnim.SetBool("cutsceneonstart1", false);
            camAnim.SetBool("cutsceneonstart2", true);
            Invoke(nameof(ChangeCutscene), 2f);
            PlayerController.Instance.IsCutSceneOn = true;
        }
        else if (cutSceneState == CutSceneState.CutSceneOnStart2)
        {
            cutSceneState = CutSceneState.CutSceneEnd;
            camAnim.SetBool("cutsceneonstart2", false);
            camAnim.SetBool("cutsceneEnd", true);
            Invoke(nameof(ChangeCutscene), 1.2f);
            PlayerController.Instance.IsCutSceneOn = true;
        }
    }
}
