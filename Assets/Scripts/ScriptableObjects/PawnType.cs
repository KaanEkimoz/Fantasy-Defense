using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pawn Type", menuName = "Pawn Type")]
    public class PawnType : ScriptableObject
    {
        [Header("Name"),Space]
        public new string name = "New Pawn";
        
        [Header("Combat"),Space]
        public int pawnRange = 5;
        public int damage = 1;
        public float timeBetweenAttacks = 2.0f;
        public GameObject missile;
        public float missileSpeed;
        
        [Header("Visual"),Space]
        public GameObject visual = null;
    }
}
