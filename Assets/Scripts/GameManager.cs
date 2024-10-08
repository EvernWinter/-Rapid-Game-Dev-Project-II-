using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [field: SerializeField] public TimeManager TimeManager; 
    
    [SerializeField] private List<GemStone> _gemStones = new List<GemStone>();
    [SerializeField] public bool isCollectGemStone_Red = false;
    [SerializeField] public bool isCollectGemStone_Blue = false;
    [SerializeField] public bool isCollectGemStone_Green = false;

    [SerializeField] private int currentLanternIndex = 1;
    [SerializeField] public bool isLanternTurnOnCorrectly = false;
    [SerializeField] private int lanternCount = 0;

    [SerializeField] public Animator camAnim;
    [SerializeField] private enum GameManagerCutSceneState { Null, CutScene1, CutScene2, CutScene3, CutScene4, CutScene5 };
    [SerializeField] private bool isPlayCutSceneOnStart;
    [SerializeField] private GameManagerCutSceneState cutSceneState;
    [SerializeField] private bool isCollectedAllGemsOnce = false;

    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CameraController cameraController;

    [SerializeField] private GameObject portal;

    private TimeManager timeManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CinemachineBrain cinemachineBrain = GetComponent<CinemachineBrain>();
        CameraController cameraController = GetComponent<CameraController>();

        timeManager = new TimeManager();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DisabledCineMachineBrain();
        timeManager.ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        timeManager.Update();

        if ((!isCollectedAllGemsOnce && CheckIfAllCrystalCollected()) || (Input.GetKeyDown(KeyCode.P)))
        {
            isCollectedAllGemsOnce = true;
            SetCutSceneState(4);
            portal.GetComponent<Portal>().isPass = true;
            portal.GetComponent<Portal>().OpenDoorMethod();

        }
    }
    
    public void SetCutSceneState(int cutSceneIndex)
    {
        EnabledCineMachineBrain();

        switch (cutSceneIndex)
        {
            case 1:
                cutSceneState = GameManagerCutSceneState.CutScene1;
                camAnim.SetBool("cutscene1", true);
                break;

            case 2:
                cutSceneState = GameManagerCutSceneState.CutScene2;
                camAnim.SetBool("cutscene2", true);
                break;

            case 3:
                cutSceneState = GameManagerCutSceneState.CutScene3;
                camAnim.SetBool("cutscene3", true);
                break;

            case 4:
                cutSceneState = GameManagerCutSceneState.CutScene4;
                camAnim.SetBool("cutscene4", true);
                break;

            case 5:
                cutSceneState = GameManagerCutSceneState.CutScene5;
                camAnim.SetBool("cutscene5", true);
                break;
        }

        
        Invoke(nameof(ChangeCutscene), 2f);
        PlayerController.Instance.IsCutSceneOn = true;
    }

    public bool CheckIfHasGemStoneOfType(GemStoneTypeEnum targetType)
    {
        foreach (var gem in _gemStones)
        {
            if (gem.GetGemStoneType() == targetType)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool CheckIfAllCrystalCollected()
    {
        if(isCollectGemStone_Blue && isCollectGemStone_Green && isCollectGemStone_Red)
        {
            return true;
        }
        return false;
        /*
        foreach (GemStoneTypeEnum type in System.Enum.GetValues(typeof(GemStoneTypeEnum)))
        {
            if (!_gemStones.Exists(g => g.GetGemStoneType() == type))
            {
                return false; // A type is missing
            }
        }
        return true; // All gemstone types collected*/
    }

    public bool CheckIfAllLanternIgnitedCorrectly()
    {
        GameObject[] lanterns = GameObject.FindGameObjectsWithTag("Lantern");

        foreach (var lantern in lanterns)
        {
            if(lantern.GetComponent<Lantern>().IsLanternOn && lantern.GetComponent<Lantern>().lanternIndex > currentLanternIndex)
            {
                lanternCount = 0;
                currentLanternIndex = 1;

                foreach(GameObject l in lanterns)
                {
                    l.GetComponent<Lantern>().LanternOff();
                }
                break;
            }
            else if (lantern.GetComponent<Lantern>().IsLanternOn && lantern.GetComponent<Lantern>().lanternIndex == currentLanternIndex)
            {
                lanternCount++;
                currentLanternIndex++;
                break;
            }
        }

        if(lanternCount >= lanterns.Length)
        {
            return true;
        }

        return false;
    }

    public void EnabledCineMachineBrain()
    {
        cinemachineBrain.enabled = true;
        cameraController.enabled = false;
    }

    public void DisabledCineMachineBrain()
    {
        cameraController.enabled = true;
        cinemachineBrain.enabled = false;
    }

    private void ChangeCutscene()
    {

        PlayerController.Instance.IsCutSceneOn = false;
        Invoke("DisabledCineMachineBrain", 1.5f);
        camAnim.SetBool("cutscene1", false);
        camAnim.SetBool("cutscene2", false);
        camAnim.SetBool("cutscene3", false);
        camAnim.SetBool("cutscene4", false);
        camAnim.SetBool("cutscene5", false);

        /*if (cutSceneState == GameManagerCutSceneState.CutScene1)
        {
            camAnim.SetBool("cutscene1", false);
        }
        else if (cutSceneState == GameManagerCutSceneState.CutScene2)
        {
            camAnim.SetBool("cutscene2", false);
        }
        else if (cutSceneState == GameManagerCutSceneState.CutScene3)
        {
            camAnim.SetBool("cutscene3", false);
        }
        else if (cutSceneState == GameManagerCutSceneState.CutScene4)
        {
            camAnim.SetBool("cutscene4", false);
        }
        else if (cutSceneState == GameManagerCutSceneState.CutScene5)
        {
            camAnim.SetBool("cutscene5", false);
        }*/
    }
}

public enum GemStoneTypeEnum
{
    Red,
    Blue,
    Green
    /*Fire,
    Water,
    Wind*/
}

//Temporary Crystal Class
public class GemStone
{
    [SerializeField] public GemStoneTypeEnum _type;
    public GemStone(GemStoneTypeEnum type)
    {
        _type = type;
    }
    
    public GemStoneTypeEnum GetGemStoneType()
    {
        return _type;
    }


}


public class TimeManager
{
    public float TimeLeft => _timeCount;
    [SerializeField] private float _timeCount;
    [SerializeField] private float _maxTime;
    
    public void Update()
    {
        if (_timeCount > 0)
        {
            _timeCount -= Time.deltaTime * 1f; 
        }
    }

    public void ResetTime()
    {
        _timeCount = _maxTime;
    }

}
