using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fort : MonoBehaviour {

    public float maxHealthPoint; // fort HP

    [SerializeField] private float currentHealthPoint;// current fort HP
    [SerializeField] private Text healthText; //  fort HP text (bar)
    [SerializeField] private Text scoreText; //score

    [SerializeField] private int score = 0;




    // Use this for initialization
    void Start() {
        currentHealthPoint = maxHealthPoint;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update() {

    }

    public void reduceHP(float amount)
    {
        currentHealthPoint -= amount;
        UpdateHealthBar();
    }

    public void addScore(int amount)
    {
        score += amount;
        UpdateScore();
    }


    private void UpdateScore ()
    {
        scoreText.text = "Score : " + score; 
    }
    private void UpdateHealthBar()
    {
        healthText.text = "Fort HP = " + " " + currentHealthPoint + "/" + maxHealthPoint;
    }

    public bool fortIsDestroyed()
    {
        return currentHealthPoint <= 0;
    }

}
