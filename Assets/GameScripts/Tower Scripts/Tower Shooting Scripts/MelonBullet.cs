using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonBullet : MonoBehaviour
{
    // The enemy is the target
    private Transform target;

    public float speed = 70f;
    public int damage = 10;

    public GameObject bulletImpactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Direction in which the bullet needs to point in
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // if distance between bullet and target is less than the distance that we move this frame(overshooting)
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        

        // Get the Enemy component from target
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
        // Instantiate impact effect
        GameObject effectInstance = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        Destroy(gameObject);
    }
}
