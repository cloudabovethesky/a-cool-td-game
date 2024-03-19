using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public Transform target;

    public float speed = 20f;

    public void SetupBullet(Transform mTarget, float mDamage)
    {
        target = mTarget;
        damage = mDamage;
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
    }

    private void HitTarget()
    {
        EnemyController enemy = target.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
