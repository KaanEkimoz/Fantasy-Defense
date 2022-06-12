using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Missile Type", menuName = "Missile Type")]
    public class MissileType : ScriptableObject
    {
        [Header("Name"),Space]
        public new string name = "New Missile";
        
        [Header("Combat"),Space]
        public int damage = 1;
        public float speed;
        public Side giveDamageToThisType;
        
        [Header("Visual"),Space]
        public GameObject visual = null;


        public enum Side
        {
            Friendly,
            Hostile
        }
    }
}
