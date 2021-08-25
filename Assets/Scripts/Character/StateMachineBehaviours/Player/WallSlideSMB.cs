using UnityEngine;

public class WallSlideSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnSLStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.SetVerticalMovement(0f);
    }

    public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // if (m_MonoBehaviour.CheckForJumpInput())
            
        m_MonoBehaviour.UpdateFacing();
       // m_MonoBehaviour.UpdateJump();
        m_MonoBehaviour.AirborneHorizontalMovement();
        m_MonoBehaviour.WallSlideVerticalMovement();




        /* m_MonoBehaviour.UpdateFacing();
         m_MonoBehaviour.GroundedHorizontalMovement(true);
         m_MonoBehaviour.GroundedVerticalMovement();
         m_MonoBehaviour.CheckForCrouching();*/
        m_MonoBehaviour.CheckForWallSlide();
        m_MonoBehaviour.CheckForGrounded();
        /*    if (m_MonoBehaviour.CheckForJumpInput())
                m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);*/
        // m_MonoBehaviour.CheckForPushing();
        //  m_MonoBehaviour.CheckForHoldingGun();
        /* m_MonoBehaviour.CheckAndFireGun();
         if (m_MonoBehaviour.CheckForJumpInput())
             m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);
         else if(m_MonoBehaviour.CheckForMeleeAttackInput())
             m_MonoBehaviour.MeleeAttack();
         if (m_MonoBehaviour.CheckForDashInput())
             m_MonoBehaviour.Dash();*/
    }
}