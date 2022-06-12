using System.Collections;
using UnityEngine;

namespace Pawn
{
    public class MeleePawn : Pawn
    {
        [SerializeField] private int meleeDamage = 20;
        protected override void Attack()
        {
            base.Attack();
            currentEnemy.GetComponent<HealthSystem>().TakeDamage(meleeDamage);
        }

        protected override IEnumerator AttackRepeating()
        {
            Attack();
            return base.AttackRepeating();
        }
    }
}
