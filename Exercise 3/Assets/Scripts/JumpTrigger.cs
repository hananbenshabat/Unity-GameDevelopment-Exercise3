using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private Animator anim;
    
    private void OnTriggerEnter(Collider other)
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isJump", false);
    }
}
