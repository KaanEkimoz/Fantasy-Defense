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
        public PawnType pawn;
        //[SerializeField] private Transform missilePoint;

        protected GameObject currentEnemy;
        private GameObject currentVisual;
        
        private HealthSystem _healthSystem;
        private Animator _animator;
        private bool _isAttacking = false;
        private bool _canAttack = false;
        private int rayDirection = 1;

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
                currentVisual = Instantiate(pawn.visual,transform.position,Quaternion.identity,gameObject.transform) as GameObject;
                if (pawn.spawnSide.ToString() == "Enemy")
                {
                    transform.Rotate(Vector3.up,-180f);
                    rayDirection = -1;

                }
                _animator = currentVisual.GetComponent<Animator>();
            }
        }

        private void Update()
        {
            if (_canAttack && !_isAttacking)
            {
                StartCoroutine(AttackRepeating());
            }

            if (!_canAttack && pawn.spawnSide.ToString() == "Enemy")
            {
                transform.position += Time.deltaTime * (Vector3.back * pawn.walkingSpeed);
                _animator.SetBool("b_isWalking",true);
            }
            else
            {
                _animator.SetBool("b_isWalking",false);
            }

            if (_healthSystem && _healthSystem.Health <= 0)
            {
                Destroy(gameObject);
                if (!_canAttack && pawn.spawnSide.ToString() == "Enemy")
                {
                    GameObject.FindObjectOfType<GoldController>().AddGold(pawn.pawnCost);
                }
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
            _detectRay = new Ray(startPosition, Vector3.forward * rayDirection);
            
            //drawing lines for debug the ray
            Debug.DrawLine(startPosition,startPosition + Vector3.forward * pawn.range * rayDirection, Color.red);
            
            //checking range
            if (Physics.Raycast(_detectRay, out _hit, pawn.range))
            {
                Debug.Log(_hit.collider);
                if (_hit.collider && !_hit.collider.CompareTag(gameObject.tag) && _hit.collider.gameObject.GetComponent<Pawn>())
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
            else
            {
                Debug.LogError("THERE IS NO ANIMATOR IN VISUAL");
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
