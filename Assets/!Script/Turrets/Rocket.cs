using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;
    public float bombRange;
    public Transform target;

    public float speed = 15f;

    public void SetupRocket(Transform mTarget, float mDamage, float mBombRange)
    {
        target = mTarget;
        damage = mDamage;
        bombRange = mBombRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float deltaDistance = speed * Time.deltaTime;

        if (dir.magnitude <= deltaDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * deltaDistance, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        {
            EnemyController enemy = target.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}