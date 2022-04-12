using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;
public class gravityController : MonoBehaviour
{
  public GameObject player;

public Vector3 gravityVector;
public float gravityStrenght; 

public pickupV2 pickupScript;
public AdvancedWalkerController advancedControllerScript;
public Mover moverScript;

public bool nullG;

public bool magBootsOn;



public Rigidbody[] allRigidBodies; 

     void Start()
    {
       allRigidBodies = (Rigidbody[]) GameObject.FindObjectsOfType(typeof(Rigidbody));
      // advancedControllerScript = player.GetComponent<AdvancedWalkerController>();
      
//Scene params here
    
       magBootsOn = true;
        
        gravityVector = Vector3.down;
        gravityStrenght = 10;

       
    } 


    

    // Update is called once per frame
    void Update()
    {

     Debug.Log(nullG);

        if (nullG)
      {
     GravityChange(0, Vector3.zero);
      }




      

//FOR DEMO

if(Input.GetKeyDown(KeyCode.LeftArrow))
{
  GravityChange(10, -Vector3.right);
}
else if(Input.GetKeyDown(KeyCode.RightArrow))
{

GravityChange(10, Vector3.right);

}
else if(Input.GetKeyDown(KeyCode.UpArrow))
{
  GravityChange(10, -Vector3.down);
}

else if(Input.GetKeyDown(KeyCode.DownArrow))
{
  GravityChange(10, Vector3.down);
}


if(Input.GetKeyDown(KeyCode.RightShift))
{
  nullG = !nullG;
  
}

    //Demo end



}







    

     void FixedUpdate()
      {

       StableGravity(gravityStrenght, gravityVector); 

      }
 

//GRAVITY CHANGE EVENTS

void GravityChange(float newStrengt, Vector3 newGravDir)
{
//nullG = false;

gravityStrenght = newStrengt;
gravityVector = newGravDir;



//advancedControllerScript.AddMomentum(newGravDir * 10);


}

//stableGravMethod

void StableGravity(float strenght, Vector3 gravDirection)
{
  
      foreach(Rigidbody body in allRigidBodies)
    {
          if(!body.gameObject.CompareTag("static") && /*!body.gameObject.CompareTag("Player") &&*/ !body.gameObject.transform.IsChildOf(pickupScript.attachPoint.transform));
     {
       body.AddForce(gravDirection * body.mass * strenght);

      if(moverScript.isGrounded)
       {
       advancedControllerScript.SetMomentum(gravDirection * (player.GetComponent<Rigidbody>().mass * 0.3f));
       }
     } 
    }
 
}


}
