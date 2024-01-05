using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    [SerializeField] float speed, impactRadius = 0f;
    [SerializeField] GameObject ImpactFx;
    [SerializeField] int damage = 50;
    Turret turret;
    AudioManager audioManager;
    public void SeekTarget(Transform _target)
    {
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        turret = GetComponent<Turret>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;


        if (dir.magnitude <= distanceThisFrame )
        {
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt (target);
    }

    void HitTarget()
    {
        GameObject FxInstance = Instantiate(ImpactFx, transform.position, transform.rotation);
        Destroy(FxInstance, 5f);
        if (impactRadius > 0f)
        {
            Explode();
        }
        else
        {
            audioManager.Play("Bullet");
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, impactRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
                audioManager.Play("Missile");
            }
        }
    }

    void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, impactRadius);
    }
}
