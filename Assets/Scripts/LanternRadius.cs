using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternRadius : MonoBehaviour
{

    [field: SerializeField] public Lantern lantern { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    // protected virtual void OnTriggerStay2D(Collider2D collider)
    // {
    //     ClockBlock clockBlock = collider.gameObject.GetComponent<ClockBlock>();
    //
    //     if (clockBlock != null && lantern.IsLanternOn)
    //     {
    //         clockBlock.OnEnterLightRadius();
    //     }
    //     else if (clockBlock != null && !lantern.IsLanternOn)
    //     {
    //         clockBlock.OnExitLightRadius();
    //     }
    //     
    // }
    //
    // protected virtual void OnTriggerExit2D(Collider2D collider)
    // {
    //     ClockBlock clockBlock = collider.gameObject.GetComponent<ClockBlock>();
    //
    //     if (clockBlock != null)
    //     {
    //         clockBlock.OnExitLightRadius();
    //     }
    // }
}
