using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Pawn
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private PawnType pawn;
        
        //animation
        private bool _isAttacking = false;
        private bool _canAttack = false;
        private Animator _animator;
        
        //raycast detect enemy
        private RaycastHit _hit;
        private Ray _detectRay;
        private void Start()
        {
            _detectRay = new Ray(transform.position, Vector3.forward);
            
            if (pawn == null)
            {
                Debug.LogError("PAWN TYPE HAS NOT BEEN ASSIGNED!");
            }
            if (pawn.visual)
            {
                _animator = pawn.visual.GetComponent<Animator>();
            }
        }

        private void Update()
        {
            
            if (_canAttack && !_isAttacking)
            {
                StartCoroutine(AttackRepeating());
            }
        }
        void FixedUpdate()
        {
            _canAttack = CheckEnemyInDistance();
            Debug.Log("Can Attack: " + _canAttack);
        }

        private bool CheckEnemyInDistance()
        {
            //drawing lines for debug the ray
            var startPosition = transform.position;
            Debug.DrawLine(startPosition,startPosition + Vector3.forward * pawn.pawnRange, Color.red);
            
            if (Physics.Raycast(_detectRay, out _hit, pawn.pawnRange)) ;
            {
                Debug.Log(_hit.collider);
                if (_hit.collider && _hit.collider.CompareTag("Enemy"))
                {
                    return true;
                }
            }
            return false;
        }
        private void Attack()
        {
            if (_animator)
            {
                _animator.SetTrigger("t_attack");
            }
        }
        private IEnumerator AttackRepeating()
        {
            _isAttacking = true;
            Attack();
            yield return new WaitForSeconds(pawn.timeBetweenAttacks);
            _isAttacking = false;
        }
    }
}
