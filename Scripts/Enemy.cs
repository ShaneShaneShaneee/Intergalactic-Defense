using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float turnSpeed = 5f, startSpeed = 8f, startSlowResist = 1.5f;

    [HideInInspector]
    public float speed, slowResist; 

    public int damage;
    public static bool isSlow = false;
    [SerializeField] float startHealth;
    float health;
    [SerializeField] int value;
    [SerializeField] GameObject deathFX;

    [Header("UnityShizz")]
    [SerializeField] Image HealthBar;

    bool isDead = false;

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        health = startHealth;
        speed = startSpeed;
        slowResist = startSlowResist;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HealthBar.fillAmount = health / startHealth;
        HealthBar.color = Color.Lerp(Color.red, Color.green, health*Time.deltaTime);
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f  - amount);
        isSlow = true;

        if(isSlow)
        {
            slowResist -= Time.deltaTime;
        }

        if(slowResist <= 0)
        {
            isSlow = false;
        }
    }

    

    void Die()
    {
        isDead = true;
        audioManager.Play("EnemyDeath");
        PlayerStats.money += value;
        GameObject effect = (GameObject)Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }
}
