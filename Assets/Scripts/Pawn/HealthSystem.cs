using UnityEngine;

namespace Pawn
{
    public class HealthSystem : MonoBehaviour
    {
        private int _maxHealth = 100;
        private int _currentHealth = 100;
        private void Start()
        {
            ResetHealth();
        }
        public int Health
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }
        public int MaxHealth
        {
            get => _maxHealth;
            private set
            {
                if (value > 0)
                {
                    _maxHealth = value;
                }
            }
        }
        public void ChangeMaxHealth(int newMaxHealth)
        {
            if (newMaxHealth > 0)
            {
                MaxHealth = newMaxHealth;
                Health = MaxHealth;
            }
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.Log(gameObject.name + " Took Damage, New HP: " + Health);
        }
        public void Heal(int healPoint)
        {
            Health += healPoint;
        }
        private void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    
    }
}
