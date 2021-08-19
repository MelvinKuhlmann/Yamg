using UnityEngine;

public class AirborneSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        m_MonoBehaviour.UpdateJump();
        m_MonoBehaviour.AirborneHorizontalMovement();
        m_MonoBehaviour.AirborneVerticalMovement();
        m_MonoBehaviour.CheckForGrounded();
        m_MonoBehaviour.CheckForHoldingGun();
        if (m_MonoBehaviour.CheckForMeleeAttackInput())
            m_MonoBehaviour.MeleeAttack();
        if (m_MonoBehaviour.CheckForDashInput())
            m_MonoBehaviour.Dash();
        m_MonoBehaviour.CheckAndFireGun();
        m_MonoBehaviour.CheckForCrouching();
    }
}
