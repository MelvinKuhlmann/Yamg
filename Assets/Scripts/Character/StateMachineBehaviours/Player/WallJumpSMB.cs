using UnityEngine;

namespace YAMG
{
    public class WallJumpSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float newFacing = m_MonoBehaviour.GetFacing() * -1;
            m_MonoBehaviour.UpdateFacing(newFacing == -1);
            m_MonoBehaviour.SetHorizontalMovement(newFacing * m_MonoBehaviour.wallJumpHorizontalSpeed);
            m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.wallJumpHeight);

        }
    }
}