using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityFlipper : MonoBehaviour
{
    //reference getter
public gravityController gravControl;
public Transform tr;

private void OnCollisionEnter(Collision other) 
{
//SwitchDirection(tr.forward, col.GetComponent<Controller>());    
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
