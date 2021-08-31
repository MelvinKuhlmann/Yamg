using UnityEngine;
using UnityEngine.Events;

namespace YAMG
{
    public class MaxHealthIncreaser : MonoBehaviour
    {
        public int healthAmount = 1;
        public UnityEvent OnIncreasingHealth;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == PlayerCharacter.PlayerInstance.gameObject)
            {
                PlayerCharacter.PlayerInstance.damageable.IncreaseHealth(healthAmount);
                OnIncreasingHealth.Invoke();
            }
        }
    }
}