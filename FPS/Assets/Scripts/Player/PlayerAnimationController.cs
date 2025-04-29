using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    public void PlayJump()
    {
        animator.SetTrigger("Jumping");
    }

    public void PlayShoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void PlayReload()
    {
        animator.SetTrigger("Reload");
    }
}
