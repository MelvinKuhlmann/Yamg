using UnityEngine;

public class CrawlidDeathSMB : SceneLinkedSMB<EnemyBehaviour>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.DisableDamage();
    }
}
