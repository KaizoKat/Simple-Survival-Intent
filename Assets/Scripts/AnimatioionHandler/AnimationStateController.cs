using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool isCheckWASD = false;
    bool isCheckCROUCH = false;
    bool isCheckSPRINT = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WASD();
        CROUCH();
        SPRINT();
        BOOLER();
    }


    void WASD()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) == true)
        {
            isCheckWASD = true;
        }
        else
        {
            isCheckWASD = false;
        }
    }

    void CROUCH()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCheckCROUCH = true;
        }
        else
        {
            isCheckCROUCH = false;
        }
    }

    void SPRINT()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isCheckSPRINT = true;
        }
        else
        {
            isCheckSPRINT = false;
        }
    }

    void BOOLER()
    {
        // WASD movment
        if(isCheckWASD == true)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking",false);
        }

        // sprint
        if(isCheckSPRINT == true)
        {
            animator.SetBool("isSprinting", true);
        }
        else
        {
            animator.SetBool("isSprinting",false);
        }

        //crouch
        if(isCheckCROUCH == true)
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching",false);
        }

        //exceptions
        if(isCheckCROUCH == true && isCheckWASD == false)
        {
            animator.SetBool("isCrouching",false);
        }
        if(isCheckSPRINT == true && isCheckWASD == false)
        {
            animator.SetBool("isSprinting",false);
        }
    }
}
