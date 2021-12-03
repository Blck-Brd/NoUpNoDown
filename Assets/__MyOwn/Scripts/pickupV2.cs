using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupV2 : MonoBehaviour
{

      public float weightLimit;
      public float grabDistance;
      public GameObject grabedObject;

      public GameObject leftHand;
      public Vector3 handPos;


      public Ray pickupRay;
      public RaycastHit pickupRaycast;




    // Start is called before the first frame update
    void Start()
    {
        grabDistance = 10f;
        weightLimit = 100;
    }

    // Update is called once per frame
    void Update()
    {
        handPos = leftHand.transform.position;
    }

 void FixedUpdate()
    {

        pickupRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.Raycast(pickupRay,out pickupRaycast, grabDistance, 3))
        {
            grabedObject = pickupRaycast.collider.gameObject;
        }

       

       if(Input.GetMouseButtonDown(0))
       { 

        if(Physics.Raycast(pickupRay,out pickupRaycast,grabDistance,3)
        &&pickupRaycast.collider.CompareTag("pickupable")
        &&pickupRaycast.rigidbody.mass< weightLimit)

        {
         //start pickup here
        StartCoroutine(Pickup());
        }
        else
        {
            
        }

       } 
    
    }


//Works well with 10F
     IEnumerator Pickup()
    {
        for (float dist = Vector3.Distance(grabedObject.transform.position, handPos); dist > 1f; dist = Vector3.Distance(grabedObject.transform.position, handPos) )
        {
        grabedObject.GetComponent<Rigidbody>().useGravity = false;
        grabedObject.transform.position = Vector3.Lerp(grabedObject.transform.position,handPos, 1f);
        }


        if(Vector3.Distance(grabedObject.transform.position,handPos) < 1.5f)
        {
           grabedObject.GetComponent<Rigidbody>().useGravity = true;
           grabedObject = null;
        }
        yield return null;
    }

    





}

