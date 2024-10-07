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

    [SerializeField] private int currentLanternIndex = 1;
    [SerializeField] public bool isLanternTurnOnCorrectly = false;
    [SerializeField] private int lanternCount = 0;

    [SerializeField] public Animator camAnim;
    [SerializeField] private enum GameManagerCutSceneState { Null, CutScene1, CutScene2 };
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
            EnabledCineMachineBrain();
            cutSceneState = GameManagerCutSceneState.CutScene2;
            camAnim.SetBool("cutscene2", true);
            Invoke(nameof(ChangeCutscene), 2f);
            PlayerController.Instance.IsCutSceneOn = true;
            portal.GetComponent<Portal>().isPass = true;
            
        }
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
        foreach (GemStoneTypeEnum type in System.Enum.GetValues(typeof(GemStoneTypeEnum)))
        {
            if (!_gemStones.Exists(g => g.GetGemStoneType() == type))
            {
                return false; // A type is missing
            }
        }
        return true; // All gemstone types collected
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
        if (cutSceneState == GameManagerCutSceneState.CutScene1)
        {
            camAnim.SetBool("cutscene1", false);
            PlayerController.Instance.IsCutSceneOn = false;
            Invoke("DisabledCineMachineBrain", 1.5f);
        }
        else if (cutSceneState == GameManagerCutSceneState.CutScene2)
        {
            camAnim.SetBool("cutscene2", false);
            PlayerController.Instance.IsCutSceneOn = false;
            Invoke("DisabledCineMachineBrain", 1.5f);
        }
    }
}

public enum GemStoneTypeEnum
{
    Fire,
    Water,
    Wind
}

//Temporary Crystal Class
public class GemStone
{
    [SerializeField] private GemStoneTypeEnum _type;
    
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
