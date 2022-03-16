using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupV2 : MonoBehaviour
{
    public Camera cam;

      public float weightLimit;
      public float grabDistance;

      public float throwForce;
      public GameObject grabedObject;
      public GameObject objectToAttach;

      public GameObject leftHand;
      public GameObject attachPoint;
      public Vector3 handPos;


      public Ray pickupRay;
      public RaycastHit pickupRaycast;

      public bool objectGrabed;
      public bool readyToAttach;

      public bool objectAttached;
      
      



    // Start is called before the first frame update
    void Start()
    {
        grabDistance = 2f;
        weightLimit = 20;
        throwForce = 10f;
  
    }

    // Update is called once per frame
    void Update()
    {
        handPos = leftHand.transform.position;



        pickupRay = new Ray(cam.transform.position, cam.transform.forward);
        
        if(Physics.Raycast(pickupRay,out pickupRaycast, grabDistance, 3)
        &&
        pickupRaycast.collider.CompareTag("pickupable"))
        {
            grabedObject = pickupRaycast.collider.gameObject;
        }

        else
        {
         grabedObject = null;
        }



       

       if(Input.GetMouseButtonDown(0))
       { 

        if(Physics.Raycast(pickupRay,out pickupRaycast,grabDistance,3)
        &&pickupRaycast.rigidbody.mass< weightLimit
        &&!objectGrabed
        && grabedObject !=null)

        {
         //start pickup here
        StartCoroutine(Pickup());
       
        }

        else if(objectAttached)
            {
                Throw(objectToAttach);
            }
        }

        

    
    if(readyToAttach)
    {
     Attach(objectToAttach);
    }


    

    }

 void FixedUpdate()
    {

        

    }


//Works well with 10F
     IEnumerator Pickup()
    {
    
        for (float dist = Vector3.Distance(grabedObject.transform.position, handPos); dist > 0.5f; dist = Vector3.Distance(grabedObject.transform.position, handPos) )
        {
        grabedObject.GetComponent<Rigidbody>().useGravity = false;
        grabedObject.transform.position = Vector3.Lerp(grabedObject.transform.position,handPos, 0.1f);
        objectGrabed = true;
        }


        if(Vector3.Distance(grabedObject.transform.position,handPos) < 1.5f)
        {
           grabedObject.GetComponent<Rigidbody>().useGravity = true;
           objectGrabed = false;
           readyToAttach = true;
           objectToAttach = grabedObject;


        }
        yield return null;
    }


    public void Attach(GameObject obj)
    {
    
    obj.GetComponent<Rigidbody>().useGravity = false;
    obj.GetComponent<Rigidbody>().isKinematic = true;
    obj.GetComponent<Collider>().enabled = false;
    obj.transform.position = attachPoint.transform.position;
    obj.transform.SetParent(attachPoint.transform);
    objectAttached = true;
    readyToAttach = false;

    }

    public void Throw(GameObject obj)
    {
      obj.GetComponent<Rigidbody>().useGravity = true;
      obj.transform.SetParent(null);
      obj.GetComponent<Rigidbody>().isKinematic = false;
      obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
      obj.GetComponent<Collider>().enabled = true;

    objectToAttach = null;
    grabedObject = null;
    objectAttached = false;
 
    }



    





}

