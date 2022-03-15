using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : MonoBehaviour
{

public Vector3 gravityVector;
public float gravityStrenght;

public bool nullG;
     public Rigidbody[] allRigidBodies; 

    void Start()
    {
       allRigidBodies = (Rigidbody[]) GameObject.FindObjectsOfType(typeof(Rigidbody));

        gravityVector = Vector3.down;
        gravityStrenght = 10;

        
        nullG = false;
       
    } 


    

    // Update is called once per frame
    void Update()
    {
      if (nullG) return;
      
foreach(Rigidbody body in allRigidBodies)
 {
     if(!body.gameObject.CompareTag("static"))
     {
       body.AddForce(gravityVector * body.mass * gravityStrenght);

     } 
    }
    }
}
