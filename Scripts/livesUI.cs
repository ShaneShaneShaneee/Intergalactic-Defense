using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStats.lives <= 1)
        {
            livesText.text = "Life: " + PlayerStats.lives.ToString();
        }
        else
        {
            livesText.text = "Lives: " + PlayerStats.lives.ToString();
        }
    }
}
