using UnityEngine;

public class DashSMB : SceneLinkedSMB<PlayerCharacter>
{
    int m_HashAirborneState = Animator.StringToHash("Airborne");

    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.IncrementHorizontalMovement(m_MonoBehaviour.dashSpeed * m_MonoBehaviour.GetFacing());
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_MonoBehaviour.CheckForGrounded())
            animator.Play(m_HashAirborneState, layerIndex, stateInfo.normalizedTime);
        m_MonoBehaviour.GroundedHorizontalMovement(false);
    }
}