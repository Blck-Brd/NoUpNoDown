using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palmGrabTrigger : MonoBehaviour
{
    // Start is called before the first frame update


    public GrabAndThrow grabScript;
    public Collider leftPalmTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gravModable") && !grabScript.isGrabed)
        {
            grabScript.Grab(other.gameObject);
            //leftPalmTrigger.enabled = false;
        }
    }




    void Start()
    {
        leftPalmTrigger = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
