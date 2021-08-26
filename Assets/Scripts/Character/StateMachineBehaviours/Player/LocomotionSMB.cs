﻿using UnityEngine;

public class LocomotionSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnSLStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.TeleportToColliderBottom();
    }

    public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        m_MonoBehaviour.GroundedHorizontalMovement(true);
        m_MonoBehaviour.GroundedVerticalMovement();
       // m_MonoBehaviour.CheckForCrouching();
        m_MonoBehaviour.CheckForGrounded();
        m_MonoBehaviour.CheckForHealing();
        m_MonoBehaviour.CheckForWallSlide();
        m_MonoBehaviour.CheckForPushing();
        m_MonoBehaviour.CheckAndFireGun();
        if (m_MonoBehaviour.CheckForJumpInput())
            m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpSpeed);
        else if(m_MonoBehaviour.CheckForMeleeAttackInput())
            m_MonoBehaviour.MeleeAttack();
        if (m_MonoBehaviour.CheckForDashInput())
            m_MonoBehaviour.Dash();
    }
}