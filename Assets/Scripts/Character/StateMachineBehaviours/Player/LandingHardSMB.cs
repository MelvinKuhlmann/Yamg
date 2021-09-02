using UnityEngine;

namespace YAMG
{
    public class LandingHardSMB : SceneLinkedSMB<PlayerCharacter>
    {
        private float timer;
        int m_HashLocomotionState = Animator.StringToHash("Locomotion");

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer = Time.time;
            m_MonoBehaviour.SetHorizontalMovement(0f);
            m_MonoBehaviour.SetVerticalMovement(0f);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Time.time - timer > m_MonoBehaviour.hardLandingDuration)
            {
                animator.Play(m_HashLocomotionState, layerIndex, stateInfo.normalizedTime);
            }
        }
    }
}