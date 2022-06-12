using System.Collections;
using UnityEngine;

namespace Pawn
{
    public class RangedPawn : Pawn
    {
        [SerializeField] private Transform missilePoint;
        public GameObject missile;
        
        protected override void Attack()
        {
            base.Attack();
            Instantiate(missile, missilePoint.position, Quaternion.identity);
        }

        protected override IEnumerator AttackRepeating()
        {
            Attack();
            return base.AttackRepeating();
        }
    }
}
