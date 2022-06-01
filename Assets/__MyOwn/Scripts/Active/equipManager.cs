using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipManager : MonoBehaviour
{
    public int ActiveWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
HandleActiveWeapon();
    }






    public void HandleActiveWeapon()
    {
    if(Input.GetKeyDown(KeyCode.Alpha1))
    {
        ActiveWeapon = 1;
    }

    if(Input.GetKeyDown(KeyCode.Alpha2))
    {
        ActiveWeapon = 2;
    }

            if(Input.GetKeyDown(KeyCode.Alpha3))
    {
        ActiveWeapon = 3;
    }

            if(Input.GetKeyDown(KeyCode.Alpha0))
    {
        ActiveWeapon = 0;
    }

    

    




    }

    private void SetActiveWpn(int wpn)
    {
        
            switch(wpn)
            {
             case 1:
                //stuff for one handed ;
                break;

             case 2:
                //stuff for twohanded;
                break;

            case 3:
            //stuff for special;
            break;

            case 0:
            //stuff for unarmed;
            break;

            } 
    }
}
