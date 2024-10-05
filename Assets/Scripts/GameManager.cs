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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        TimeManager.Update();
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
                Debug.LogWarning("Wrong Lantern SUIIII");

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
                Debug.LogWarning("Correct Lantern SUIIII");
                break;
            }
        }

        if(lanternCount >= lanterns.Length)
        {
            Debug.Log("LanternPassed");
            return true;
        }

        return false;
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
