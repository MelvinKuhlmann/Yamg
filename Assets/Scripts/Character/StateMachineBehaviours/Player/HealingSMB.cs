using UnityEngine;

namespace YAMG
{
    public class HealingSMB : SceneLinkedSMB<PlayerCharacter>
    {
        private float timer;
        int m_HashLocomotionState = Animator.StringToHash("Locomotion");
        int m_HashAirborneState = Animator.StringToHash("Airborne");
        private string vfxName = "HealingEffect";
        private Vector3 offset = Vector3.zero;
        private float startDelay = 0;

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer = Time.time;
            m_MonoBehaviour.SetHorizontalMovement(0f);
            m_MonoBehaviour.SetVerticalMovement(0f);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!m_MonoBehaviour.CheckForGrounded())
            {
                animator.Play(m_HashAirborneState, layerIndex, stateInfo.normalizedTime);
            }
            m_MonoBehaviour.CheckForHealing();
            if (Time.time - timer > m_MonoBehaviour.healingDuration)
            {
                m_MonoBehaviour.Heal();
                VFXController.Instance.Trigger(vfxName, offset, startDelay, false, animator.transform);
                // To prevent all missing health will be added at once we explicitly set the state to locomotion, so if you keep down healing, it will trigger the proces all over again.
                animator.Play(m_HashLocomotionState, layerIndex, stateInfo.normalizedTime);
            }
        }
    }
}