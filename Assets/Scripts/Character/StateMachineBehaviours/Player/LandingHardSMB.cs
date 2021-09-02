using UnityEngine;

namespace YAMG
{
    public class LandingHardSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.SetHorizontalMovement(0f);
            m_MonoBehaviour.SetVerticalMovement(0f);
        }
    }
}