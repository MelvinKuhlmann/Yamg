using UnityEngine;

public class HealingSMB : SceneLinkedSMB<PlayerCharacter>
{
    private float timer;
    int m_HashLocomotionState = Animator.StringToHash("Locomotion");

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Time.time;
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.CheckForHealing();
        if (Time.time - timer > m_MonoBehaviour.healingDuration)
        {
            Debug.LogError("Healing Done");
            m_MonoBehaviour.Heal();
            animator.Play(m_HashLocomotionState, layerIndex, stateInfo.normalizedTime);
        }
    }
}