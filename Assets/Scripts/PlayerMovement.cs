using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float crouchHeight=1f;
    //public float crouchSpeed=6f;
    public float crouchMoveMult=0.5f;       //slower movement when crouch
    public float crouchCamOffset=0.6f;
    public float cameraForwardOffset=0.1f;

    public Transform playerCamera;
    //public Transform playerParent;
    
    private CharacterController controller;
    private FirstPersonController fpsController;

    private float standHeight;
    private float standMoveSpeed;
    private float standCamHeight;
    private bool isCrouching/*=false*/;
    //private Vector3 standPos;

    // void OnEnable()
    // {
    //     crouchAction.Enable();
    //     moveAction.Enable();
    // }

    // void onDisable()
    // {
    //     crouchAction.Disable();
    //     moveAction.Disable();
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller=GetComponent<CharacterController>();
        fpsController=GetComponent<FirstPersonController>();

        standHeight=controller.height;
        standMoveSpeed=fpsController.MoveSpeed;

        //standPos=transform.position;
        standCamHeight=playerCamera.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCrouch();
        HandleMovement();
    }

    void HandleCrouch()
    {
        //read crouch value from input system action
        bool crouchInput=Input.GetKey(KeyCode.C);
        //isCrouching=Input.GetKey(KeyCode.C);

        //adjust player height
        if (crouchInput&& !isCrouching)
        {
            isCrouching=true;

            //float heightDifference=standHeight-crouchHeight;
            controller.height=crouchHeight;
            controller.center=new Vector3(0,controller.height/2f,0);

            //move whole capsule down
            //transform.position-=Vector3.up*(heightDifference/2f);
            float offset=(standHeight-crouchHeight)/2f;
            //playerParent.position-=new Vector3(0,offset,0);
            fpsController.transform.position-=new Vector3(0,offset,0);

            //move slower when crouching
            fpsController.MoveSpeed=standMoveSpeed*crouchMoveMult;
        }
        if(!crouchInput && isCrouching)
        {
            isCrouching=false;

            //float heightDifference=standHeight-crouchHeight;

            controller.height=standHeight;
            controller.center=new Vector3(0,standHeight/2f,0);

            //restore position
            //transform.position+=Vector3.up*(heightDifference/2f);
            // float offset=(standHeight-crouchHeight)/2f;
            // //playerParent.position+=new Vector3(0,offset,0);
            // fpsController.transform.position+=new Vector3(0,offset,0);

            //restore speed
            fpsController.MoveSpeed=standMoveSpeed;
        }


        //adjust camera height
        Vector3 camPos=playerCamera.localPosition;
        if (isCrouching)
        {
            camPos.y=standCamHeight-crouchCamOffset;
        }
        else
        {
            camPos.y=standCamHeight;
        }
        camPos.z=cameraForwardOffset;
        playerCamera.localPosition=camPos;
        
        //moveSpeed=5f;
    }

    // void HandleMovement()
    // {
    //     Vector2 moveInput=movementScript.moveInput;
        
    //     Vector3 move=transform.right*moveInput.x+transform.forward*moveInput.y;

    //     //slow movement while crouching
    //     float speed;
    //     if (isCrouching)
    //     {
    //         speed=moveSpeed*crouchMoveMult;
    //     }
    //     else
    //     {
    //         speed=moveSpeed;
    //     }
    //     controller.Move(move*speed*Time.deltaTime);
    // }

    //teh camera has to go DOWN-NOT UP

    void HandleMovement()
    {
        if (isCrouching)
        {
            float offset=(standHeight-crouchHeight)/2f;
            //playerParent.position+=new Vector3(0,offset,0);
            fpsController.transform.position-=new Vector3(0,offset,0);
        }
        else
        {
             float offset=(standHeight-crouchHeight)/2f;
            // //playerParent.position+=new Vector3(0,offset,0);
             fpsController.transform.position+=new Vector3(0,offset,0);
        }
    }
}