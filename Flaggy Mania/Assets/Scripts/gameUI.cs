using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameUI : MonoBehaviour
{
    public M3car m3car;
    public BuggyCar buggy;
    public AICarWork AIcar;
    public Playercollide playercar;
    public GameObject game_over;
    public GameObject RCCcanvas;
    public Text winorlose;
    public GameObject settings_menu;
   

    public RCC_AICarController car1;
    public RCC_AICarController car2;
    public RCC_AICarController car3;
    public RCC_CarControllerV3 car4;

    public float starting_countdown = 4;
    public Text countdown_time;
    public GameObject countdown_time_object;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        car1.enabled = false;
        car2.enabled = false;
        car3.enabled = false;
        car4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (starting_countdown > 0)
        {
            
            starting_countdown -= Time.deltaTime;
        }
        else
        {
            starting_countdown = 0;
        }
        countdowntimer(starting_countdown);



        if (m3car.points >= 75)
        {
            Time.timeScale = 0f;
            //RCCcanvas.SetActive(false);
            game_over.SetActive(true);
            winorlose.text = string.Format("You Lose");
          
            //Application.Quit();
        }
        if (AIcar.points >= 75)
        {
            Time.timeScale = 0f;
            //RCCcanvas.SetActive(false);
            game_over.SetActive(true);
            winorlose.text = string.Format("You Lose");
            
            //Application.Quit();
        }
        if (buggy.points >= 75)
        {
            Time.timeScale = 0f;
            //RCCcanvas.SetActive(false);
            game_over.SetActive(true);
            winorlose.text = string.Format("You Lose");
        
            //Application.Quit();
        }
        if (playercar.points >= 75)
        {
            Time.timeScale = 0f;
            //RCCcanvas.SetActive(false);
            game_over.SetActive(true);
            winorlose.text = string.Format("You Win");
         
            //Application.Quit();
        }
    }
    public void RestartButton()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Single_Player");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Time.timeScale = 1f;
    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        Time.timeScale = 0f;
        settings_menu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        settings_menu.SetActive(false);
    }

    public void Quit()
    {
       // mpcar.highestPoint = 0;
        PhotonNetwork.Disconnect();
        
        Time.timeScale = 0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void countdowntimer(float cntdwn)
    {
        
            if (cntdwn < 0)
            {               
                countdown_time_object.SetActive(false);
            }
            else if (cntdwn < 1)
            {
                countdown_time.GetComponent<Text>().text = "GO!";
                car1.enabled = true;
                car2.enabled = true;
                car3.enabled = true;
                car4.enabled = true;
        }
            else if ((cntdwn >= 1))
            {
                float seconds = Mathf.FloorToInt(cntdwn % 60);
                countdown_time.text = string.Format("{0}", seconds);
            }
        
    }
    
}
