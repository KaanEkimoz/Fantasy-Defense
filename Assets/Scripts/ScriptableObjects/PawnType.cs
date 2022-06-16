using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pawn Type", menuName = "Pawn Type")]
    public class PawnType : ScriptableObject
    {
        [Header("Name"),Space]
        public new string name = "New Pawn";
        public int pawnCost;
        public float walkingSpeed = 10f;
        public SpawnSide spawnSide = SpawnSide.Kingdom;
        

        [Header("Combat"), Space] 
        public int health;
        public int range = 5;
        public float timeBetweenAttacks = 2.0f;

        [Header("Visual"),Space]
        public GameObject visual;
        
        public enum SpawnSide
        {
            Enemy, //Right
            Kingdom //Left
        }
    }
}
