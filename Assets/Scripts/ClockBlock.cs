using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClockBlock : Block
{
    [Header("Clock Block: Puzzle Data")] 
    [SerializeField] private int _number = 0;
    [SerializeField] private bool _isInLightRadius;
    public bool IsInLightRadius => _isInLightRadius;

    [SerializeField] private List<Lantern> lights = new List<Lantern>();

    [Header("Clock Block: Number Display")] 
    [SerializeField] private TMP_Text _numberText;
    
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (_number == 0)
        {
            _numberText.gameObject.SetActive(false);
        }
        
        CheckForLightsOff();
        
        UpdateTextBaseOnLights();
    }

    void UpdateTextBaseOnLights()
    {
        _isInLightRadius = lights.Count > 0 ? true : false;
        
        if (_isInLightRadius && _number != 0)
        {
            _numberText.gameObject.SetActive(true);
        }
        else
        {
            _numberText.gameObject.SetActive(false);
        }
    }

    void CheckForLightsOff()
    {
        foreach (var lantern in lights)
        {
            if(!lantern.IsLanternOn && lights.Contains(lantern))
            {
                lights.Remove(lantern);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        LanternRadius lanternRadius = collider.gameObject.GetComponent<LanternRadius>();

        if (lanternRadius != null && lanternRadius.lantern.IsLanternOn && !lights.Contains(lanternRadius.lantern))
        {
            lights.Add(lanternRadius.lantern);
        }
    }
    
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        LanternRadius lanternRadius = collider.gameObject.GetComponent<LanternRadius>();
    
        if (lanternRadius != null)
        {
            lights.Remove(lanternRadius.lantern);
        }
    }

    public void OnEnterLightRadius()
    {
        _isInLightRadius = true;
    }

    public void OnExitLightRadius()
    {
        _isInLightRadius = false;
    }

    public void AssignText(string text)
    {
        _number = int.Parse(_numberText.text);
        _numberText.text = text;
    }
}
