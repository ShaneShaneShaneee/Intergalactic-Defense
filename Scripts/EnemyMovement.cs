using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        target = Waypoints.waypoints[0];
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        Quaternion lookRotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemy.turnSpeed);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        if (!Enemy.isSlow)
        {
            enemy.speed = enemy.startSpeed;
            enemy.slowResist = enemy.startSlowResist;
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }

    void EndPath()
    {
        audioManager.Play("BaseHit");
        PlayerStats.lives -= enemy.damage;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
