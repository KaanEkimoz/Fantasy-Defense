using ScriptableObjects;
using UnityEngine;

namespace Pawn
{
    //Requires health system to assign the pawns start health and reset the pawn
    [RequireComponent(typeof(HealthSystem))]
    public abstract class Pawn : MonoBehaviour
    {
        //scriptable object pawn, stores the pawn data
        public PawnType pawn;

        //stores current enemy of the pawn
        protected GameObject CurrentEnemy;

        //Combat
        private HealthSystem _healthSystem;
        private bool _canAttack = false;
        private Ray _detectRay;
        private int _rayDirection = 1;
        private RaycastHit _hit;
        
        //Animation
        private Animator _animator;
        private bool _isAttacking = false;
        private bool _isWalking = false;
        private bool _isIdling = true;
                  
        private void Start()
        {
            _healthSystem = gameObject.GetComponent<HealthSystem>();
            _animator = GetComponent<Animator>();

            ResetPawn();

            if (pawn == null)
                Debug.LogError("PAWN TYPE HAS NOT BEEN ASSIGNED!");

            if (pawn.pawnSide == PawnType.PawnSide.Right)
            {
                transform.Rotate(Vector3.up, -180f);
                _rayDirection = -1;
            }
        }

        private void Update()
        {
            if (_canAttack && !_isAttacking)
                Attack();

            if (!_canAttack)
                Walk();

            UpdateAnimator();
        }
        void FixedUpdate()
        {
            if (CheckEnemyInDistance())
            {
                _canAttack = true;
                CurrentEnemy = _hit.collider.gameObject;
            }
            else
                _canAttack = false;
        }
        private bool CheckEnemyInDistance()
        {
            var startPosition = transform.position;
            _detectRay = new Ray(startPosition, Vector3.forward * _rayDirection);
            
            //draw line to debug the ray
            Debug.DrawLine(startPosition,startPosition + _rayDirection * pawn.range * Vector3.forward, Color.red);
            
            //checks the range
            if (Physics.Raycast(_detectRay, out _hit, pawn.range) && _hit.collider)
                return _hit.collider.GetComponent<Pawn>().pawn.pawnSide != pawn.pawnSide; //checks if it is in same side or not

            return false;
        }
        protected virtual void Attack()
        {
            _isWalking = false;
            _isAttacking = true;
        }
        private void Walk()
        {
            _isAttacking = false;
            _isWalking = true;
            transform.position += Time.deltaTime * (transform.forward * pawn.moveSpeed);
        }
        private void ResetPawn()
        {
            _canAttack = false;
            _isWalking = true;
            _isAttacking = false;
            _isIdling = false;
            _healthSystem.ChangeMaxHealth(pawn.maxHealth);
        }
        protected virtual void UpdateAnimator()
        {
            _animator.SetBool("b_isIdling", _isIdling);
            _animator.SetBool("b_isWalking", _isWalking);
            _animator.SetBool("b_isAttacking", _isAttacking);
        }
    }
}
