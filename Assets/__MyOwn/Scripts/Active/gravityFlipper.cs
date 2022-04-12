using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class gravityFlipper : MonoBehaviour
{
    //reference getter
public gravityController gravControl;
public Transform tr;

public Vector3 newUp;


private void OnCollisionEnter(Collision other) 
{

if(other.gameObject.CompareTag("static") && gravControl.magBootsOn)
{
//Calculate new up direction
newUp = other.contacts[0].normal;

SwitchDirection(newUp, tr);  
}

/*else if(!gravControl.nullG)

{
newUp = other.contacts[0].normal;
GetComponent<AdvancedWalkerController>().AddMomentum(-newUp.normalized * 0.5f);
}
*/

}



void SwitchDirection(Vector3 _newUpDirection, Transform tr)
		{

            /*
			float _angleThreshold = 0.001f;

			//Calculate angle;
			float _angleBetweenUpDirections = Vector3.Angle(_newUpDirection, transform.up);

			//If angle between new direction and current rigidbody rotation is too small, return;
			if(_angleBetweenUpDirections < _angleThreshold)
				return;

           */

			Transform _transform = transform;

			//Rotate gameobject;
			Quaternion _rotationDifference = Quaternion.FromToRotation(_transform.up, _newUpDirection);
			_transform.rotation = _rotationDifference * _transform.rotation;
		}




    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
