using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : MonoBehaviour
{

public Vector3 gravityVector;
public float gravityStrenght; 

public pickupV2 pickupScript;




public bool nullG;

public Rigidbody[] allRigidBodies; 

     void Start()
    {
       allRigidBodies = (Rigidbody[]) GameObject.FindObjectsOfType(typeof(Rigidbody));

        nullG = false;
        gravityVector = Vector3.right;
        gravityStrenght = 10;


       
    } 


    

    // Update is called once per frame
    void Update()
    {
if (nullG)
{
  gravityVector = Vector3.zero;
  gravityStrenght = 0;
}

      if (nullG) return;
      
      foreach(Rigidbody body in allRigidBodies)
    {
          if(!body.gameObject.CompareTag("static") && !body.gameObject.CompareTag("Player") && !body.gameObject.transform.IsChildOf(pickupScript.attachPoint.transform));
     {
       body.AddForce(gravityVector * body.mass * gravityStrenght);

     } 
    }
    }
}
