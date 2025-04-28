using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
        animator.SetBool("IsRunning", true);
        
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsRunning",false);
            
        }

        if (Input.GetKeyDown("space"))
        {
            animator.SetTrigger("Jumping");
            
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
            
        }
    }
}

