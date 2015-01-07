using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    //Attributes
    public float walkSpeed;
    public float rotateSpeed;
    public float runSpeed;
    public string playerName;

	private Animator anim;
    private float intSpeed;
    private Vector3 direction;

    //Methods

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
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical") ;
        direction = ResourceManager.cam.TransformDirection(moveHorizontal, 0.0f, moveVertical);
        direction.y = 0;
        direction = direction.normalized;
        if (moveHorizontal != 0 || moveVertical != 0) {
						anim.SetFloat ("Speed", 1);
				} else {
						anim.SetFloat ("Speed", 0);
				}
        Vector3 speed = (direction *  walkSpeed) ;
        rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
        RotateInWalkDirection(rotateSpeed, direction.x, direction.z);

    }

    /**
     * Makes the player sprint when a certain button is pressed.
     * */
    void Sprint(float runSpeed)
    {

        if (Input.GetKey(KeyCode.Space))
        {
            this.walkSpeed = this.intSpeed*runSpeed;
        }
        else
            this.walkSpeed = this.intSpeed;
    }

    void RotateToNPC()
    {
        if (ResourceManager.stopWalking)
        {
            Transform target = ResourceManager.conversationWith;
            Vector3 targetDir = target.position - transform.position;
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    //Actions

    void Start()
    {
        intSpeed = walkSpeed;
		anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!ResourceManager.stopWalking)
        {
            Walk(walkSpeed, rotateSpeed);
            Sprint(runSpeed);
            ResourceManager.playerPosition = transform.position;
        }
        RotateToNPC();
    }
}
