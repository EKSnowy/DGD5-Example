using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public AudioSource AS;

    public TextMeshPro TimeText;
    public float timer;
    public float minutes;

    public Player_Controller PC;

    public int amp = 0;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        minutes = Mathf.FloorToInt(timer / 60);

        if (minutes > 0)
        {
            TimeText.text = (minutes + ":00");

            if (minutes == 2)
            {
                amp = 1;
            }

            if (minutes == 4)
            {
                amp = 2;
            }
            
            if (minutes == 6)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
        
    }

    public int Accelerate()
    {
        return amp;
    }

    public void setAmp(int num)
    {
        amp = num;
    }
}
