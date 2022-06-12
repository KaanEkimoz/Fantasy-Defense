using System;
using System.Collections;
using System.Collections.Generic;
using Pawn;
using ScriptableObjects;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //raycast detect enemy
    private RaycastHit _hit;
    private Ray _detectRay;
    [SerializeField] private MissileType missile;
    private Vector3 _missileDirection;
    private void Start()
    {
        Instantiate(missile.visual,transform.position,Quaternion.identity,gameObject.transform);
        if (missile.giveDamageToThisType.ToString() == "Hostile")
        {
            _missileDirection = Vector3.forward;
        }
        else
        {
            _missileDirection = -Vector3.forward;
            
            var localScale = missile.visual.transform.localScale;
            localScale = new Vector3(localScale.x, localScale.y, -localScale.z);
            missile.visual.transform.localScale = localScale;
        }
    }
    void Update()
    {
        transform.Translate(missile.speed * Time.deltaTime * _missileDirection);
    }

    private void FixedUpdate()
    {
        CheckEnemyInDistanceAndGiveDamage();
    }

    //TO DO: Change this method
    private bool CheckEnemyInDistanceAndGiveDamage()
    {
        _detectRay = new Ray(transform.position, Vector3.forward);
        //drawing lines for debug the ray
        var startPosition = transform.position;
        Debug.DrawLine(startPosition,startPosition + _missileDirection * 0.5f, Color.blue);
            
        //checking range
        if (Physics.Raycast(_detectRay, out _hit, 0.5f))
        {
            Debug.Log(_hit.collider);
            if (_hit.collider && _hit.collider.CompareTag(missile.giveDamageToThisType.ToString()))
            {
                Debug.Log("EnemyInDistance");
                if (_hit.transform.gameObject.GetComponent<HealthSystem>())
                {
                    _hit.transform.gameObject.GetComponent<HealthSystem>().TakeDamage(missile.damage);
                }
                Destroy(gameObject);
                return true;
            }
        }
        return false;
    }
}
