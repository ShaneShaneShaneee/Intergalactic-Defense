using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money, lives, waves;
    [SerializeField] int startMoney = 200, startLives = 20;
    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        lives = startLives;
        waves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageLife()
    {

    }
}
