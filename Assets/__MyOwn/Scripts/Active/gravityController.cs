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
        gravityVector = Vector3.down;
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

//FOR DEMO

if(Input.GetKeyDown(KeyCode.LeftArrow))
{
  gravityVector = Vector3.left;
}
else if(Input.GetKeyDown(KeyCode.RightArrow))
{
  gravityVector = -Vector3.left;
}
else if(Input.GetKeyDown(KeyCode.UpArrow))
{
  gravityVector = Vector3.up;
}

else if(Input.GetKeyDown(KeyCode.DownArrow))
{
  gravityVector = -Vector3.up;
}
else if(Input.GetKeyDown(KeyCode.RightShift))
{
 gravityVector = Vector3.zero;
      foreach(Rigidbody body in allRigidBodies)
    {
          if(!body.gameObject.CompareTag("static") && !body.gameObject.CompareTag("Player") && !body.gameObject.transform.IsChildOf(pickupScript.attachPoint.transform));
     {
       body.AddForce(Vector3.up * 2 * body.mass/10, ForceMode.Impulse);

     } 
    }

    //Demo end



}


      foreach(Rigidbody body in allRigidBodies)
    {
          if(!body.gameObject.CompareTag("static") && !body.gameObject.CompareTag("Player") && !body.gameObject.transform.IsChildOf(pickupScript.attachPoint.transform));
     {
       body.AddForce(gravityVector * body.mass * gravityStrenght);

     } 
    }





    }





}
