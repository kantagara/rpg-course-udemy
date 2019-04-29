using RPG.Core;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        private float _timeSinceLastAttack = 0f;
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead) return;
            
            
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);

            }
            else
            {
                AttackBehaviour();
                GetComponent<Mover>().Cancel();
            }
        }

        public bool CanAttack(CombatTarget tar)
        {
            if (!tar) return false;
            var health = tar.GetComponent<Health>();
            return health && !health.IsDead;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (!(_timeSinceLastAttack > timeBetweenAttacks)) return;
            
            TriggerAttack();
            _timeSinceLastAttack = 0;
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stop_attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stop_attack");
        }

        void Hit()
        {
            if(target)
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }
    }
}