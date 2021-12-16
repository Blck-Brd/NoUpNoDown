using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControll : MonoBehaviour
{

        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -50F;
        public float maximumX = 60F;

        public float minimumY = -50F;
        public float maximumY = 50F;

        float rotationY = 0F;
    

    //edit:
    float rotationX = 0F;





    void Update()
        {
        //CAM CODE
            if (axes == RotationAxes.MouseXAndY)
            {
            //float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            //edit
            rotationX  += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
         

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        

    }

        void Start()
        {
            
            //if(!networkView.isMine)
            //enabled = false;

            // Make the rigid body not change rotation
            //if (rigidbody)
            //rigidbody.freezeRotation = true;
        }
    }
