using UnityEngine;

public class ChomperDeathSMB : SceneLinkedSMB<EnemyBehaviour>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.DisableDamage();
    }
}
