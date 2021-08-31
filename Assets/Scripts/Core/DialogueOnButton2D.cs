using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace YAMG
{
    public class DialogueOnButton2D : InteractOnTrigger2D
    {
        public UnityEvent OnButtonPress;
        public UnityEvent OnButtonPressAfterPreConditions;

        bool m_CanExecuteButtons;
        bool m_preConditionsMet = false;

        protected override void ExecuteOnEnter(Collider2D other)
        {
            m_CanExecuteButtons = true;
            OnEnter.Invoke();
        }

        protected override void ExecuteOnExit(Collider2D other)
        {
            m_CanExecuteButtons = false;
            m_preConditionsMet = false;
            OnExit.Invoke();
        }

        void Update()
        {
            if (m_CanExecuteButtons)
            {
                if (OnButtonPress.GetPersistentEventCount() > 0 && PlayerInput.Instance.Interact.Down && !m_preConditionsMet)
                {
                    OnButtonPress.Invoke();
                    m_preConditionsMet = true;
                }
                else if (OnButtonPressAfterPreConditions.GetPersistentEventCount() > 0 && PlayerInput.Instance.Interact.Down && m_preConditionsMet)
                {
                    OnButtonPressAfterPreConditions.Invoke();
                    // This avoids being retriggered again in the same frame update
                    StartCoroutine(WaitForRetrigger());
                }
            }
        }

        private IEnumerator WaitForRetrigger()
        {
            yield return new WaitForSeconds(0.1f);
            m_preConditionsMet = false;
        }
    }
}