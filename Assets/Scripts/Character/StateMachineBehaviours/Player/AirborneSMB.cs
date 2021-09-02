using UnityEngine;

namespace YAMG
{
    public class AirborneSMB : SceneLinkedSMB<PlayerCharacter>
    {
        int m_HashLandingHardState = Animator.StringToHash("LandingHard");
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.UpdateJump();
            m_MonoBehaviour.AirborneHorizontalMovement();
            m_MonoBehaviour.AirborneVerticalMovement();
            if (m_MonoBehaviour.CheckForGrounded() && m_MonoBehaviour.CheckLandingHard())
            {
                animator.Play(m_HashLandingHardState, layerIndex, stateInfo.normalizedTime);
            }
            m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForWallSlide();
            if (m_MonoBehaviour.CheckForMeleeAttackInput())
                m_MonoBehaviour.MeleeAttack();
            if (m_MonoBehaviour.CheckForDashInput())
                m_MonoBehaviour.Dash();
            m_MonoBehaviour.CheckAndFireGun();
        }
    }
}