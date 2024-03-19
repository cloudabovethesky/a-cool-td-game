using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTurret : BaseTurret
{
    public float damage = 5f;
    [Range(0f, 1f)]
    public float slowPercent;
    public LineRenderer lineRend;

    public Transform muzzle;
    public Transform head; // turret's head

    public override void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (target == null)
        {
            if (lineRend.enabled)
            {
                lineRend.enabled = false;
            }
            return;
        }

        Lazer();

        // turret's head look at enemy
        head.LookAt(target);
    }

    private void Lazer()
    {
        if (!lineRend.enabled)
        {
            lineRend.enabled = true;
        }

        lineRend.SetPosition(0, muzzle.position);
        lineRend.SetPosition(1, target.position);

        targetEnemy.TakeDamage(damage * Time.deltaTime);
        targetEnemy.Slow(slowPercent);
    }
}
