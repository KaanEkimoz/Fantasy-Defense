using System;
using System.Collections;
using System.Security.Cryptography;
using ScriptableObjects;
using UnityEngine;

namespace Pawn
{
    [RequireComponent(typeof(HealthSystem))]
    public class Pawn : MonoBehaviour
    {
        [SerializeField] protected PawnType pawn;
        //[SerializeField] private Transform missilePoint;

        protected GameObject currentEnemy;
        
        private HealthSystem _healthSystem;
        private Animator _animator;
        private bool _isAttacking = false;
        private bool _canAttack = false;

        //raycast detect enemy
        private RaycastHit _hit;
        private Ray _detectRay;
        private void Start()
        {
            _healthSystem = gameObject.GetComponent<HealthSystem>();
            ResetPawn();
            
            
            if (pawn == null)
            {
                Debug.LogError("PAWN TYPE HAS NOT BEEN ASSIGNED!");
            }
            if (pawn.visual)
            {
                Instantiate(pawn.visual,transform.position,Quaternion.identity,gameObject.transform);
                _animator = pawn.visual.GetComponent<Animator>();
            }
        }

        private void Update()
        {
            if (_canAttack && !_isAttacking)
            {
                StartCoroutine(AttackRepeating());
            }

            if (_healthSystem && _healthSystem.Health <= 0)
            {
                Destroy(gameObject);
            }
        }
        void FixedUpdate()
        {
            _canAttack = CheckEnemyInDistance();
            Debug.Log(gameObject.name+ " Can Attack: " + _canAttack);
        }

        private bool CheckEnemyInDistance()
        {
            var startPosition = transform.position;
            _detectRay = new Ray(startPosition, Vector3.forward);
            
            //drawing lines for debug the ray
            Debug.DrawLine(startPosition,startPosition + Vector3.forward * pawn.range, Color.red);
            
            //checking range
            if (Physics.Raycast(_detectRay, out _hit, pawn.range))
            {
                Debug.Log(_hit.collider);
                if (_hit.collider && !_hit.collider.CompareTag(gameObject.tag))
                {
                    currentEnemy = _hit.transform.gameObject;
                    return true;
                }
            }
            return false;
        }
        protected virtual void Attack()
        {
            if (_animator)
            {
                _animator.SetTrigger("t_attack");
            }
            //Instantiate(pawn.missile, missilePoint.position, Quaternion.identity);
        }
        protected virtual IEnumerator AttackRepeating()
        {
            _isAttacking = true;
            //Attack();
            yield return new WaitForSeconds(pawn.timeBetweenAttacks);
            _isAttacking = false;
        }

        private void ResetPawn()
        {
            _canAttack = false;
            _isAttacking = false;
            _healthSystem.ChangeMaxHealth(pawn.health);
        }
    }
}
