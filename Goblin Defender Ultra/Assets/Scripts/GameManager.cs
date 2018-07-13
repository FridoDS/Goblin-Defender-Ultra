using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Spawner spawner; //the enemies spawner
    public Fort fort; //the fort
    public GameObject gameoverPanel; //game over panel for selecting retry or quit
    public Text scoreText;

    [SerializeField] private int enemiesOnField; //count remaining enemies on field


    //[SerializeField] private float fortMaxHp;


    private void Awake()
    {
        //int numGameSessions = FindObjectsOfType<GameManager>().Length;
        //if (numGameSessions > 1)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }
    // Use this for initialization
    void Start () {
        if (Time.timeScale == 0) Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update () {

        
        if (fort.fortIsDestroyed())
        {
            Time.timeScale = 0f;
            gameoverPanel.SetActive(true);
        }
        

	}

    //restart the game
    public void ResetGameSessions()
    {

        //Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    //unused yet
    public void PauseGame()
    {
        //pauseUI.SetActive(true);
        Time.timeScale = 0f;
        //EventSystem.current.SetSelectedGameObject(GameObject.Find("ResumeButton"));

    }

    //quit the game
    public void QuitGame ()
    {
        Application.Quit();
    }
}
