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
        Debug.Log("Running");
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("IsRunning",false);
            Debug.Log("Stopped Running");
        }

        if (Input.GetKeyDown("space"))
        {
            animator.SetTrigger("Jumping");
            Debug.Log("Jump");
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
            Debug.Log("Reload");
        }
    }
}

