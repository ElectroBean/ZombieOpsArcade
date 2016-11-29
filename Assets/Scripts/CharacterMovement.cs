using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public GameObject mainCamera;
    public float MoveSpeed = 0.5f;
    public float RotateSpeed = 1f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20f;
    
    //FPS mode moveSpeed
    public float fpsSpeed = 15f;
    CharacterController cc;
    private Vector3 moveDirection = Vector3.zero;

    
    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        if(SceneManager.GetActiveScene().name == "Prototype_1")
        {
           mainCamera.transform.position = transform.position + new Vector3(0, 100, 5);
            if (Time.timeScale != 0)
                {
                    cc.Move(Vector3.forward * Input.GetAxis("Vertical") * MoveSpeed + Physics.gravity);
                    cc.Move(Vector3.right * Input.GetAxis("Horizontal") * MoveSpeed + Physics.gravity); 
                }
        }

        if (SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            //fps movement
            CharacterController controller = GetComponent<CharacterController>();
            //makes sure the characte was on the ground
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= fpsSpeed;
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}

