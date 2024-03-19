using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float hp = 100;
    private float hpDamage;

    public Transform[] path;
    public float speed = 5.0f;
    private float speedMultiply = 1f;
    private float slowTimer = 0f;
    private int targetWaypointIndex;
    public Image hpBar;
    public int goldDrop = 50;



    // Start is called before the first frame update
    public void Setup(Transform[] waypoints)
    {
        // MainGameController.instance.enemyCount++;
        path = waypoints;
        transform.position = path[0].position;
        targetWaypointIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameController.instance.endGame)
        {
            Destroy(gameObject);
        }

        Vector3 dir = path[targetWaypointIndex].position - transform.position;
        transform.Translate(dir.normalized * speed * speedMultiply * Time.deltaTime, Space.World);
        transform.LookAt(transform.position + dir);

        if (dir.magnitude < 0.1f)
        {
            targetWaypointIndex++;
            if (targetWaypointIndex >= path.Length)
            {
                MainGameController.instance.life--;
                Destroy(gameObject);
            }
        }

        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
        }
        else
        {
            speedMultiply = 1f;
        }
    }


    public void TakeDamage(float damage)
    {
        hpDamage += damage;

        hpBar.fillAmount = (hp - hpDamage) / hp;

        if (hpDamage >= hp)
        {
            // MainGameController.instance.enemyCount--;
            MainGameController.instance.gold += goldDrop;
            Destroy(gameObject);
        }
    }

    public void Slow(float slowPercent)
    {
        speedMultiply = 1f - slowPercent;
        slowTimer = 1f;
    }
}
