using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupAndThrow : MonoBehaviour
{

public GameObject grabedObject;
public GameObject leftHand;
       
 public Vector3 handPos;
 public Vector3 origPos;
 public float distanceOfObject;

    public Ray pickupRay;
    public RaycastHit pickupRaycast;
    public float weightLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() 
    {
       handPos = leftHand.transform.position; 
        //distanceOfObject = Vector3.Distance(grabedObject.transform.position, handPos);
        grabedObject = pickupRaycast.collider.gameObject;
        origPos = grabedObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(Input.GetMouseButtonDown(0))
       {

        pickupRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(pickupRay,out pickupRaycast,10f,3)
        &&pickupRaycast.collider.CompareTag("pickupable")
        &&pickupRaycast.rigidbody.mass< weightLimit)
        {
         grabedObject = pickupRaycast.collider.gameObject;
         origPos = grabedObject.transform.position;
         
         StartCoroutine(Pickup());
        

        }
        else
        {
            grabedObject = null;
        }
       } 
    }

     /*void Pickup(GameObject obj, Vector3 origPos , Vector3 targetPos)
    {
     
        
        obj.GetComponent<Rigidbody>().isKinematic = true; // disable physics while jumping


    }*/

IEnumerator Pickup() 
{ 

    for ( float dist = Vector3.Distance(grabedObject.transform.position, handPos); dist > 1f; dist = Vector3.Distance(grabedObject.transform.position, handPos) ) 
    {
        grabedObject.GetComponent<Rigidbody>().useGravity = false;
        grabedObject.transform.position = Vector3.Lerp(origPos,handPos, 0.5f);
        if (dist < 1.5f)
        {
            grabedObject.GetComponent<Rigidbody>().useGravity = true;
            grabedObject = null;

        }
        yield return null;
    }
}


/*IEnumerator Pickup(GameObject obj, Vector3 origPos, Vector3 handPosition)
{
        for (float t = 0.0f; t < 1.0f;)
        {
            obj.transform.position = Vector3.Lerp(origPos,handPosition, Time.deltaTime);
            yield return null; // return here next frame
        }
}*/
/*void Pickup(GameObject obj, Vector3 origPos, Vector3 handPosition)
{
  obj.transform.position = Vector3.Lerp(origPos,handPosition, Time.deltaTime);
}*/

}
