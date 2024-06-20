using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Set up Fields")]
    public Transform partToRotate;
    public float rotationSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        // If target is in range, set it to be the target
        foreach (Enemy enemy in EntitySpawner.EnemiesInGame)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If we don't have a target, don't do anything
        if (target == null)
        {
            return;
        }

        // Tower Target Lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Using lerping to rotate the tower smoothly when chaning targets
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject currentBullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        MelonBullet melonBullet = currentBullet.GetComponent<MelonBullet>(); 

        if (melonBullet != null)
        {
            melonBullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if (firePoint != null)
        {
            Debug.Log("fire point is here");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(firePoint.position, 0.2f); // Draw a small blue sphere at the firePoint position
        }
    }
}
