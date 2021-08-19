using UnityEngine;

public class AirborneDashSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnSLStatePostEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.IncrementHorizontalMovement(m_MonoBehaviour.dashSpeed * m_MonoBehaviour.GetFacing());
    }

    public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateJump();
        m_MonoBehaviour.AirborneHorizontalMovement();
        m_MonoBehaviour.AirborneVerticalMovement();
        m_MonoBehaviour.CheckForGrounded();
        m_MonoBehaviour.GroundedHorizontalMovement(false);
    }
}