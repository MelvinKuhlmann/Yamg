using UnityEngine;

namespace YAMG
{
    public class AirborneMeleeAttackSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.ForceNotHoldingGun();
        }

        public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.EnableMeleeAttack();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateJump();
            m_MonoBehaviour.AirborneHorizontalMovement();
            m_MonoBehaviour.AirborneVerticalMovement();
            m_MonoBehaviour.CheckForGrounded();
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.DisableMeleeAttack();
        }
    }
}