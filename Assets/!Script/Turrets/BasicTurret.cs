using UnityEngine;

public class BasicTurret : BaseTurret
{
    public float damage = 10f;
    public float fireRate = 2f;
    private float fireTimer = 0f;

    public GameObject bulletPrefab;
    public Transform muzzle;
    public AudioSource shootingSound;

    public override void Start()
    {
        base.Start();
        if (shootingSound == null)
        {
            Debug.LogError("AudioSource is not assigned");
        }
    }

    public override void Update()
    {
        fireTimer -= Time.deltaTime;
        base.Update();

        if (fireTimer <= 0f && target != null)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
    }

    private void Shoot()
    {
        if (target == null)
        {
            return;
        }
        GameObject go = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        Bullet bullet = go.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetupBullet(target, damage);
            if (shootingSound != null)
            {
                shootingSound.Play();
            }
        }
        else
        {
            Destroy(go);
        }
    }
}
