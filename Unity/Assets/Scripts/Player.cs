using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float walkSpeed;
    public float rotateSpeed;
    

   bool rotateDirection(float rotateTo)
    {
        return rigidbody.rotation.eulerAngles.y <= rotateTo;    
    }

    /**
     * Rotates player in walking direction
     * */
   void rotate(float rotateTo, float rotateSpeed)
    {
       
       Vector3 angleSpeed = new Vector3(0, rigidbody.rotation.eulerAngles.y, 0);
       
       if (rotateDirection(rotateTo))
       {
           if(rigidbody.rotation.eulerAngles.y < rotateTo)
                angleSpeed = new Vector3(0, rigidbody.rotation.eulerAngles.y + rotateSpeed, 0);
       }
       else
       {
           if(rigidbody.rotation.eulerAngles.y > rotateTo)
           angleSpeed = new Vector3(0, rigidbody.rotation.eulerAngles.y - rotateSpeed + rotateTo, 0);
       }
        Quaternion deltaRotation = Quaternion.Euler(angleSpeed * Time.deltaTime);
    }
    
    /**
     *  Makes the object walk in pre-defined direction
     **/
    void walk(Vector3 speed)
    {
        rigidbody.MovePosition(rigidbody.position + speed*Time.deltaTime);
    }

    /**
     * Converts input from the keyboard to movement
     * */
    void InputToWalk(float walkSpeed, float rotateSpeed)
    {
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 speed = new Vector3(-walkSpeed, 0.0f, -walkSpeed);
            float rotateTo = 135;
            rotate(rotateTo, rotateSpeed);
            walk(speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 speed = new Vector3(walkSpeed, 0.0f, -walkSpeed);
            float rotateTo = 45;
            rotate(rotateTo, rotateSpeed);
            walk(speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 speed = new Vector3(-walkSpeed, 0.0f, walkSpeed);
            float rotateTo = 225;
            rotate(rotateTo, rotateSpeed);
            walk(speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 speed = new Vector3(walkSpeed, 0.0f, walkSpeed);
            float rotateTo = 315;
            rotate(rotateTo, rotateSpeed);
            walk(speed);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        InputToWalk(walkSpeed,rotateSpeed);
	
	}
}
