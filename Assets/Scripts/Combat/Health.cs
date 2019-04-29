using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]private float _health = 100f;
        [SerializeField] private Animator _animator;
        private static readonly int Death = Animator.StringToHash("die");
        private bool _isDead;

        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            if (_health > 0) return;
            Die();
        }

        private void Die()
        {
            if(_isDead) return;
            
            _isDead = true;
            _animator.SetTrigger(Death);
        }
    }
}