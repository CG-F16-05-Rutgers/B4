using UnityEngine;
using System.Collections;
public class LocomotionSMB : StateMachineBehaviour
{
    public float m_Damping = 0.15f;


    private readonly int m_HashHorizontalPara = Animator.StringToHash("Horizontal");
    private readonly int m_HashVerticalPara = Animator.StringToHash("Vertical");
    //private int Sprint = Animator.StringToHash("Speed");



    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (animator.GetFloat("Strafe") == 0)
            {
                animator.SetFloat("Strafe", 1F);
            } else
            {
                animator.SetFloat("Strafe", 0F);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetFloat("Speed", animator.GetFloat("Speed") + .1f);
            if (animator.GetFloat("Speed") > 1)
                animator.SetFloat("Speed", 1f);
        } else
        {
            animator.SetFloat("Speed", animator.GetFloat("Speed") - .1f);
            if (animator.GetFloat("Speed") < 0)
                animator.SetFloat("Speed", 0f);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        Vector2 input = new Vector2(horizontal, vertical).normalized;

        animator.SetFloat(m_HashHorizontalPara, input.x, m_Damping, Time.deltaTime);
        animator.SetFloat(m_HashVerticalPara, input.y, m_Damping, Time.deltaTime);
    }
    
}
