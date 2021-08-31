using UnityEngine;

namespace YAMG
{
    public class WallSlideSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.SetVerticalMovement(0f);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.AirborneHorizontalMovement();
            m_MonoBehaviour.WallSlideVerticalMovement();
            m_MonoBehaviour.CheckForWallSlide();
            m_MonoBehaviour.CheckForGrounded();

            if (m_MonoBehaviour.CheckForJumpInput())
                m_MonoBehaviour.WallJump();

        }
    }
}