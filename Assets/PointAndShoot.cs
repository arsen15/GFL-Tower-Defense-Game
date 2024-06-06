using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;

public class Point : MonoBehaviour
{
    private GameObject closestEnemy = null;
    private float closestEnemyDistance = Mathf.Infinity;
    private Collider[] enemyHitCollidersInRange;

    public float range;
    public LayerMask layerMask;
    public float shootCooldownPeriod = 1;
    public float timeSincePreviousShot = 0;
    public float velocity;
    public GameObject apple;
    public GameObject barrelHole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePreviousShot += Time.deltaTime;
        UpdateClosestEnemy();
        if (closestEnemy != null) { 
            AimAtClosestEnemy(); 
            if (timeSincePreviousShot >= shootCooldownPeriod)
            {
                Shoot();
                timeSincePreviousShot = 0;
            }
        }

        // Point to closest enemy
    }

    private void UpdateClosestEnemy()
    {
        enemyHitCollidersInRange = Physics.OverlapSphere(transform.position, range, layerMask);

        if (closestEnemy != null) 
        {  
            if (!enemyHitCollidersInRange.Contains(closestEnemy.GetComponent<BoxCollider>()))
            {
                closestEnemy = null;
                closestEnemyDistance = Mathf.Infinity;
            }
        }
       
        foreach (var hitCollider in enemyHitCollidersInRange)
        {
            float thisHitColliderDistanceToMe = Vector3.Distance(hitCollider.gameObject.transform.position, transform.position);
            if (thisHitColliderDistanceToMe < closestEnemyDistance)
            {
                closestEnemy = hitCollider.gameObject;
                closestEnemyDistance = thisHitColliderDistanceToMe;
            }

        }
    }

    private void AimAtClosestEnemy()
    {
        Vector3 lookDirection = closestEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = rotation;
    }

    private void Shoot()
    {
        GameObject newApple = Instantiate(apple, barrelHole.transform);
        newApple.GetComponent<Rigidbody>().velocity = barrelHole.transform.forward * velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

