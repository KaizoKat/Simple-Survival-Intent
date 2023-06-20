using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtaracterController : MonoBehaviour
{
    //FLAG -------------------------AL THE VARIABLES AND OTHER THINGS OF THE SAME TIPE----------------------------------------

    CharacterController pl_controller = null;                           //player obj

    [SerializeField] Transform pl_camera;                               //camera obj
    [SerializeField] float mouse_speed = 3.5f;                          //mouse sensitivity
    [SerializeField] bool lockCursor = true;                            //bool cursor lock
    [SerializeField] float walkSpeed = 4.0f;                            //walk speed
    [SerializeField] float gravity = -13.0f;                            //gravity
    [SerializeField] float jumpHeight = 5.0f;                           //lump height
    [SerializeField][Range(0.0f,0.5f)] float moveSmooth_time = 0.3f;    //movment smoother
    [SerializeField][Range(0.0f,0.3f)] float rotationSpeed = 0.3f;      //rotation smoother

    Transform child;                                                    // gets the main menu child component

    Vector2 currentDir = Vector2.zero;                                  //current direction
    Vector2 currentVel = Vector2.zero;                                  //current velocity
    Vector3 ref3 = Vector3.zero;                                        //reference 3
    Vector3 ref4 = Vector3.zero;                                        //reference 4
    Vector3 rotator = Vector3.zero;                                     //rotation ref

    Vector2 currentFov = Vector2.zero;                                  //current fov
    Vector2 FOVI;                                                       //FOV indexer controller

    float cameraPitch = 0.0f;                                           //camera pich ref
    float velocitY = 0.0f;                                              //vertical velocity
    float zoomModifier = 70.0f;                                         //zoom modifier default (unchangable)
    float fovIndexer;                                                   //FOV indexer
    
    bool inventoryUp = false;                                           //inventory grafical interfacing

    //FLAG --------------------------------------MAIN INITIALIZERS-----------------------------------------------------------
    void Start()
    {
        pl_controller = GetComponent<CharacterController>();
        float fovIndexer = pl_camera.GetComponent<Camera>().fieldOfView;
        Vector2 FOVI = new Vector2(fovIndexer,0.0f);
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if(inventoryUp == false)
        {
            MouseLook();
        }
        Controllers();
        MovmentHandler();
    }

    // FLAG -------------------------------------MOVMENT AND CAMERA HANDLERS-----------------------------------------------
    void MouseLook()//COMMENT_ Camera Update
    {
        Vector2 mosueDelta = new Vector2(Input.GetAxis("mouse_x") ,Input.GetAxis("mouse_y"));

        cameraPitch -= mosueDelta.y * mouse_speed;
        cameraPitch = Mathf.Clamp(cameraPitch,-89,90);
        pl_camera.localEulerAngles = Vector3.right * cameraPitch;

        rotator = Vector3.SmoothDamp(rotator,Vector3.up * mosueDelta.x * mouse_speed,ref ref4,rotationSpeed);
        transform.Rotate(rotator);
    }

    void MovmentHandler()//COMMENT_ Movment Update
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("pos_x"), Input.GetAxisRaw("pos_y"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir,targetDir,ref currentVel,moveSmooth_time);

        pl_camera.GetComponent<Camera>().fieldOfView = FOVI.x;
        velocitY += gravity * Time.deltaTime;
        
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocitY;
        pl_controller.Move(velocity * Time.deltaTime);           
    }

    // FLAG----------------------------------------SETS UP HEIGHT MODIFIERS----------------------------------------------------
    void HeightMod()
    {
        if(pl_controller.isGrounded)
        {
            int layerMask = LayerMask.GetMask("Buildable");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                pl_controller.height = 1.2f;
                GetComponent<BoxCollider>().size = Vector3.up * 1.2f;
                pl_controller.Move(Vector3.down * 1.2f);
                pl_camera.transform.localPosition = Vector3.SmoothDamp(pl_camera.transform.localPosition,Vector3.up * 0.2f,ref ref3,0.1f);
            }
        }   
    }

    void HeightModUp()
    {
        if(pl_controller.isGrounded)
        {
            int layerMask = LayerMask.GetMask("Buildable");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                pl_controller.height = 2.0f;
                GetComponent<BoxCollider>().size = Vector3.up * 2.0f;
                pl_controller.Move(Vector3.down * 2.0f);
                pl_camera.transform.localPosition = Vector3.SmoothDamp(pl_camera.transform.localPosition,Vector3.up * 0.62f,ref ref3,0.1f);
            }
        }        
    }

    // FLAG ---------------------------------------------SETS UP CONTROLLS------------------------------------------------------
    void Controllers()
    {
        if(pl_controller.isGrounded)//COMMENT_ Grounding and jumping
        {
            velocitY = 0.0f;
            if(Input.GetKey(KeyCode.Space))
            {
                velocitY = jumpHeight;
            }
        }
        
        if(Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))//COMMENT_ Sprinting
        {
            walkSpeed = 8.0f;
            FOVI = Vector2.SmoothDamp(FOVI,new Vector2(63.0f,0.0f),ref currentFov,0.1f);
            moveSmooth_time = 0.2f;
        }
        else
        {
            moveSmooth_time = 0.05f;
        }
            
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))//COMMENT_ Sprint-jumping
        {
            walkSpeed = 8.0f;
            FOVI = Vector2.SmoothDamp(FOVI,new Vector2(63.0f,0.0f),ref currentFov,0.1f);
            moveSmooth_time = 0.3f;
        }

        if(Input.GetKey(KeyCode.LeftControl))//COMMENT_ Crouching
        {
            walkSpeed = 2.0f;
            FOVI = Vector2.SmoothDamp(FOVI,new Vector2(50.0f,0.0f),ref currentFov,0.1f);
            HeightMod();
        }
        else
        {
            HeightModUp();
        }
            

        if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))//COMMENT_ Sprint and crouch reset
        {   
            walkSpeed = 4.0f;
            FOVI = Vector2.SmoothDamp(FOVI,new Vector2(60.0f,0.0f),ref currentFov,0.1f);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inventoryUp == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                child.transform.GetChild(1).gameObject.SetActive(true);
                inventoryUp = true;
                return;
            }

            if(inventoryUp == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                child.transform.GetChild(1).gameObject.SetActive(false);
                inventoryUp = false;
                return;         
            }
        }

        if(Input.GetKey(KeyCode.F2))//COMMENT_ Zoom modifiers
        {
            if(Input.GetKey(KeyCode.Equals))
            {
                zoomModifier -= 0.25f;
                mouse_speed -= 0.025f;
            }
                
            if(Input.GetKey(KeyCode.Minus))
            {
                zoomModifier += 0.25f;
                mouse_speed += 0.025f;
            }
            zoomModifier = Mathf.Clamp(zoomModifier,10.0f,60.0f);
            mouse_speed = Mathf.Clamp(mouse_speed,1.0f,5.0f);
            FOVI = Vector2.zero;
            FOVI.x = zoomModifier;
        }
        else
        {
            mouse_speed = 5.0f;
            zoomModifier = 70.0f;
        }
    }
}
