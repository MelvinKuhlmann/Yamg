using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Damageable representedDamageable;
    public GameObject healthIconPrefab;

    protected List<Animator> m_HealthIconAnimators = new List<Animator>();

    protected readonly int m_HashActivePara = Animator.StringToHash("Active");
    protected readonly int m_HashInactiveState = Animator.StringToHash("Inactive");
    protected const float k_HeartIconAnchorWidth = 0.041f;

    IEnumerator Start()
    {
        if (representedDamageable == null)
            yield break;

        yield return null;
        FillHealthList();
    }

    private void FillHealthList()
    {
        for (int i = 0; i < representedDamageable.startingHealth; i++)
        {
            GameObject healthIcon = Instantiate(healthIconPrefab);
            healthIcon.transform.SetParent(transform);
            RectTransform healthIconRect = healthIcon.transform as RectTransform;
            healthIconRect.anchoredPosition = Vector2.zero;
            healthIconRect.sizeDelta = Vector2.zero;
            healthIconRect.anchorMin += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
            healthIconRect.anchorMax += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
            m_HealthIconAnimators.Add(healthIcon.GetComponent<Animator>());

            if (representedDamageable.CurrentHealth < i + 1)
            {
                m_HealthIconAnimators[i].Play(m_HashInactiveState);
                m_HealthIconAnimators[i].SetBool(m_HashActivePara, false);
            }
        }
    }

    public void UpdateUI()
    {
        m_HealthIconAnimators.Clear();
        FillHealthList();
    }

    public void ChangeHitPointUI(Damageable damageable)
    {
        if (m_HealthIconAnimators.Count == 0)
            return;

        for (int i = 0; i < m_HealthIconAnimators.Count; i++)
        {
            m_HealthIconAnimators[i].SetBool(m_HashActivePara, damageable.CurrentHealth >= i + 1);
        }
    }
}