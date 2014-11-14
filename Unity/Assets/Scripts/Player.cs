using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float rotateSpeed;
    public float accelerator;
    private float intSpeed;



    /**
     * Rotates player in walking direction
     * */
    void RotateInWalkDirection(float rotateSpeed, float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection != (new Vector3(0, 0, 0)))
        {
            
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rigidbody.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }

    /**
     *  Makes the object walk in pre-defined direction
     **/
    void Walk(float walkSpeed,float rotateSpeed)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 speed = new Vector3(moveHorizontal, 0.0f, moveVertical) * walkSpeed ;
        rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
        RotateInWalkDirection(rotateSpeed, moveHorizontal, moveVertical);
    }

    /**
     * Makes the player sprint when a certain button is pressed.
     * */
    void Sprint(float accelerator)
    {

        if (Input.GetKey(KeyCode.Space))
        {
            this.walkSpeed = this.intSpeed*accelerator;
        }
        else
            this.walkSpeed = this.intSpeed;
    }

    // Use this for initialization
    void Start()
    {
        intSpeed = walkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk(walkSpeed, rotateSpeed);
        Sprint(accelerator);
        ResourceManager.playerPosition = transform.position;
    }
}
