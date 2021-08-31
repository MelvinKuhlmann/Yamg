using UnityEngine;
using UnityEngine.Events;

namespace YAMG
{
    public class MaxDamageIncreaser : MonoBehaviour
    {
        public int damageAmount = 1;
        public UnityEvent OnIncreasingDamage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == PlayerCharacter.PlayerInstance.gameObject)
            {
                PlayerCharacter.PlayerInstance.meleeDamager.IncreaseDamage(damageAmount);
                OnIncreasingDamage.Invoke();
            }
        }
    }
}