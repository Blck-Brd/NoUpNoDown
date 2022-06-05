using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public bool gunRaised;
    // Start is called before the first frame update
    void Start()
    {
        gunRaised = false;
    }

    // Update is called once per frame
    void Update()
    {
   HandleControls();
    }


void HandleControls()
{


if(Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
{
    gunRaised = !gunRaised;
Debug.Log(gunRaised);
}

}


}
