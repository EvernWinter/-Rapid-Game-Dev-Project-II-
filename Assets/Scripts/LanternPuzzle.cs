using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternPuzzle : MonoBehaviour
{
    [SerializeField] private enum CutSceneState { Null, CutScene1};
    [SerializeField] private bool isPlayCutSceneOnStart;
    [SerializeField] private CutSceneState cutSceneState;
    [SerializeField] private Animator camAnim;

    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private List<GameObject> lanternsList;
    [SerializeField] private GameObject lanternPrefab;
    [SerializeField] private List<GameObject> lanternSpawnerPosition;
    [SerializeField] private List<int> lanternIndexList;
    [SerializeField] private bool isLanternPuzzlePassOnce = false;
    [SerializeField] private GameObject gem;

    [SerializeField] private GameObject lanternPuzzlePassed;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CameraController cameraController;


    private void Awake()
    {
        CinemachineBrain cinemachineBrain = GetComponent<CinemachineBrain>();
        CameraController cameraController = GetComponent<CameraController>();
    }

    void Start()
    {
        DisabledCineMachineBrain();
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        lanternPuzzlePassed.SetActive(false);

        for (int i = 0; i < lanternSpawnerPosition.Count; i++)
        {
            int random = Random.Range(0, lanternIndexList.Count);
            GameObject lantern = Instantiate(lanternPrefab, lanternSpawnerPosition[i].transform);
            lanternsList.Add(lantern);
            lantern.GetComponent<Lantern>().lanternIndex = lanternIndexList[random];
            lanternIndexList.RemoveAt(random);
        }
    }

    void Update()
    {
        if(GameManager.instance.isLanternTurnOnCorrectly && !isLanternPuzzlePassOnce)
        {
            isLanternPuzzlePassOnce = true;
            EnabledCineMachineBrain();
            cutSceneState = CutSceneState.CutScene1;
            camAnim.SetBool("cutscene1", true);
            Invoke(nameof(ChangeCutscene), 2f);
            boxCollider2D.enabled = false;
            PlayerController.Instance.IsCutSceneOn = true;
            lanternPuzzlePassed.SetActive(true);
            //Instantiate(gem, lanternPuzzlePassed.transform);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cutscene Detect player");
            if (cutSceneState == CutSceneState.CutScene1)
            {
                //boxCollider2D.enabled = false;
                camAnim.SetBool("cutscene1", true);
                EnabledCineMachineBrain();
                PlayerController.Instance.IsCutSceneOn = true;
            }

            StartCoroutine(LanternPreviewCoroutine());
            Invoke(nameof(ChangeCutscene), lanternsList.Count * 0.7f);
        }
    }

    private IEnumerator LanternPreviewCoroutine()
    {
        for(int i = 0; i < lanternsList.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            foreach(GameObject lantern in lanternsList)
            {
                if(lantern.GetComponent<Lantern>().lanternIndex == i + 1)
                {
                    lantern.GetComponent<Lantern>().LanternOn();
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < lanternsList.Count; i++)
        {
            lanternsList[i].GetComponent<Lantern>().LanternOff();
        }
    }

    private void ChangeCutscene()
    {
        if (cutSceneState == CutSceneState.CutScene1)
        {
            camAnim.SetBool("cutscene1", false);
            PlayerController.Instance.IsCutSceneOn = false;
            Invoke("DisabledCineMachineBrain", 1.5f);
        }
    }

    private void EnabledCineMachineBrain()
    {
        cinemachineBrain.enabled = true;
        cameraController.enabled = false;
    }

    private void DisabledCineMachineBrain()
    {
        cameraController.enabled = true;
        cinemachineBrain.enabled = false;
    }
}
