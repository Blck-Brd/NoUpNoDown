using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupV2 : MonoBehaviour
{

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
        weightLimit = 100;
        throwForce = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        handPos = leftHand.transform.position;
    }

 void FixedUpdate()
    {

        pickupRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
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
    obj.transform.position = attachPoint.transform.position;
    obj.transform.SetParent(attachPoint.transform);
    objectAttached = true;
    readyToAttach = false;

    }

    public void Throw(GameObject obj)
    {
      obj.GetComponent<Rigidbody>().useGravity = true;
      obj.transform.SetParent(null);
      obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

    objectToAttach = null;
    grabedObject = null;
    objectAttached = false;
 
    }

    





}

