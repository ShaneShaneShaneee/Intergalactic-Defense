using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Transform target;
    Enemy targetEnemy;

    [Header("General")]
    [SerializeField] float range;

    [Header("UseBullets")]
    [SerializeField] float turnSpeed;
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;

    [Header("UseLaser")]
    [SerializeField] bool useLaser = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactFX;
    [SerializeField] Light impactLight;
    [SerializeField] int dps = 25;

    [Header("UsePoison")]
    [SerializeField] bool usePoison = false;
    [SerializeField] ParticleSystem poisonFX;
    [SerializeField] float slowEffect = .5f, poisonDuration;
    public float startPoisonDuration = 2f;

    [Header("Setup")]
    [SerializeField] Transform head;
    [SerializeField] Transform bulletLoc;
    float fireCountdown = 0f;
    Animator turretAnim;

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        turretAnim = head.GetComponent<Animator>();
        poisonDuration = startPoisonDuration;
        audioManager = FindFirstObjectByType<AudioManager>();
    }


    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                    turretAnim.SetBool("Shoot", false);
                    impactFX.Stop();
                    audioManager.Stop("Laser");
                }
            }
            if(usePoison)
            {
                poisonFX.Stop();
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            laser();
        }
        else
        {
           fire();
        }
    }

    void fire()
    {
        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
            turretAnim.SetBool("Shoot", true);
        }
        else
        {
            turretAnim.SetBool("Shoot", false);
        }
        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotate = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotate, Time.deltaTime * turnSpeed).eulerAngles;
        head.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void laser()
    {
        targetEnemy.TakeDamage (dps * Time.deltaTime);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactFX.Play();
            audioManager.Play("Laser");
        }
        turretAnim.SetBool("Shoot", true);
        lineRenderer.SetPosition(0, bulletLoc.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = bulletLoc.position - target.position;

        impactFX.transform.rotation = Quaternion.LookRotation(dir);

        impactFX.transform.position = target.position + dir.normalized;
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletClone = (GameObject)Instantiate (bullet, bulletLoc.position, Quaternion.identity);
        Bullet _bullet = bulletClone.GetComponent<Bullet>();

        if(_bullet != null)
        {
            _bullet.SeekTarget(target);
        }
    }
}
