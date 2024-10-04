using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [field: SerializeField] public TimeManager TimeManager; 
    
    [SerializeField] private List<GemStone> _gemStones = new List<GemStone>();

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

    public bool CheckIfAllLanternIgnited()
    {
        GameObject[] lanterns = GameObject.FindGameObjectsWithTag("Lantern");
        int lanternCount = 0;

        foreach (var lantern in lanterns)
        {
            if(lantern.GetComponent<Lantern>().isLanternOn)
            {
                lanternCount++;
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
