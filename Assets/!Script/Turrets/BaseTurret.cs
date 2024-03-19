using System.Collections;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public int sellprice;
    public Transform target;
    public EnemyController targetEnemy;
    public float range = 5f;
    public Transform partToRotate;

    public virtual int GetSellPrice(int cost)
    {
        return Mathf.RoundToInt(cost * 0.7f);
    }

    public virtual void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    public virtual void Update()
    {
        if (MainGameController.instance.endGame)
        {
            return;
        }
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        partToRotate.rotation = Quaternion.LookRotation(dir);
    }

    private IEnumerator UpdateTarget()
    {
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float shortestDistance = float.MaxValue;
            GameObject nearestEnemy = null;

            for (int i = 0; i < enemies.Length; i++)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemies[i];
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = target.GetComponent<EnemyController>();
            }
            else
            {
                target = null;
            }

            yield return new WaitForSeconds(0.25f);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}