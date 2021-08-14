﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        m_MonoBehaviour.SetMoveVector(Vector2.zero);
    }
}