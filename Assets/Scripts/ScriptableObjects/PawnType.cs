using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pawn Type", menuName = "Pawn Type")]
    public class PawnType : ScriptableObject
    {
        [Header("General Information"),Space]
        public new string name = "New Pawn";
        public int pawnCost;
        public float moveSpeed = 10f;
        public PawnSide pawnSide = PawnSide.Left;
        

        [Header("Combat"), Space] 
        public int maxHealth = 30;
        public int damagePerAttack = 10;
        public int range = 5;
        public float timeBetweenAttacks = 2.0f;
        
        public enum PawnSide
        {
            Right, //Enemy Side
            Left //Player Side
        }
    }
}
