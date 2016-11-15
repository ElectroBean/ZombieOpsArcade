using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public GameObject mainCamera;
    public float MoveSpeed = 0.5f;
    public float RotateSpeed = 1f;
    CharacterController cc;

    
    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        cc.Move(Vector3.forward * Input.GetAxis("Vertical") * MoveSpeed + Physics.gravity);
        cc.Move(Vector3.right * Input.GetAxis("Horizontal") * MoveSpeed + Physics.gravity); 
        mainCamera.transform.position = transform.position + new Vector3(0, 100, 5);
    }
}


        // rotate the character according to left/right key presses
        //transform.Rotate(Vector3.up* Input.GetAxis("Horizontal") * RotateSpeed);
        // move forward/backward according to up/down key presses
        //cc.Move(transform.forward* Input.GetAxis("Vertical") * MoveSpeed + Physics.gravity);