using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Projectile Type", menuName = "Projectile Type")]
    public class ProjectileType : ScriptableObject
    {
        [Header("Name"),Space]
        public new string name = "New Projectile";
        
        [Header("Combat"),Space]
        public int damage = 1;
        public float projectileSpeed;
        public PawnType.PawnSide damageableSide;
    }
}
