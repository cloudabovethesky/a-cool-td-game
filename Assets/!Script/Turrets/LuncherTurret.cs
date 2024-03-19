using UnityEngine;

public class LuncherTurret : BaseTurret
{
    public float damage = 10f;
    public float fireRate = 2f;
    public float bombRange = 3f;
    private float fireTimer = 0f;
    [Range(0f, 1f)]
    public float slowPercent;

    public GameObject rocketPrefab;
    public Transform muzzle;
    public AudioClip shootingSoundClip; 

    private AudioSource shootingAudioSource; 

    [Range(0f, 1f)]
    public float shootingVolume = 0.2f;

    public override void Start()
    {
        base.Start();
        shootingAudioSource = gameObject.AddComponent<AudioSource>();
        if (shootingSoundClip == null)
        {
            Debug.LogError("AudioClip is not assigned to LuncherTurret.");
        }
        else
        {
            shootingAudioSource.clip = shootingSoundClip; 
            shootingAudioSource.volume = shootingVolume; 
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
        GameObject go = Instantiate(rocketPrefab, muzzle.position, Quaternion.identity);
        Rocket missile = go.GetComponent<Rocket>();
        if (missile != null)
        {
            missile.SetupRocket(target, damage, bombRange);
            if (shootingAudioSource != null && shootingSoundClip != null)
            {
                shootingAudioSource.PlayOneShot(shootingSoundClip);
            }
        }
        else
        {
            Destroy(go);
        }
        targetEnemy.Slow(slowPercent);
    }
}
