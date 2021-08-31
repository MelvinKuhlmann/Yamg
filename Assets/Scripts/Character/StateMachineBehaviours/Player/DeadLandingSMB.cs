using UnityEngine;

namespace YAMG
{
    public class DeadLandingSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.GroundedHorizontalMovement(false);
        }
    }
}