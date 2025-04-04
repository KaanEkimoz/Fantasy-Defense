using UnityEngine;

namespace Pawn
{
    public class RangedPawn : Pawn
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;
        protected override void Attack()
        {
            base.Attack();
        }
        private void FireProjectile()
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
}
