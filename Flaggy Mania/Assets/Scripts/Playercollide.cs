using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Playercollide : MonoBehaviour
{
    public GameObject pos1;
    [HideInInspector]
    public GameObject flag;
    public RCC_AICarController enemyCarScript;
    public RCC_AICarController buggycarscript;
    public RCC_AICarController m3carScript;
    public AICarWork AIcar;
    public BuggyCar buggy;
    public M3car m3car;
    public double playerpoints;
    public Text pointstext;
    public Text player_current_position;
    public GameObject playerCarPos;
    public int points;
    
   
    // Start is called before the first frame update
    void Start()
    {       
        playerpoints = 0;
        flag = GameObject.Find("flag_med");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag.transform.parent == this.gameObject.transform)
        {
            playerpoints += Time.deltaTime;
            DisplayScore(playerpoints);
        }
        racepositioning();
    }

    void racepositioning()
    {
        if (points >= AIcar.points && points >= buggy.points && points >= m3car.points)
        {
            player_current_position.text = string.Format ("1st");
        }
        else if (points >= AIcar.points && points <= buggy.points && points <= m3car.points)
        {
            player_current_position.text = string.Format("3rd");
        }
        else if (points >= AIcar.points && points >= buggy.points && points <= m3car.points)
        {
            player_current_position.text = string.Format("2nd");
        }
        else if (points >= AIcar.points && points <= buggy.points && points >= m3car.points)
        {
            player_current_position.text = string.Format("2nd");
        }
        else if (points <= AIcar.points && points >= buggy.points && points >= m3car.points)
        {
            player_current_position.text = string.Format("2nd");
        }
        else if (points <= AIcar.points && points >= buggy.points && points <= m3car.points)
        {
            player_current_position.text = string.Format("3rd");
        }
        else if (points <= AIcar.points && points <= buggy.points && points >= m3car.points)
        {
            player_current_position.text = string.Format("3rd");
        }
        else if (points <= AIcar.points && points <= buggy.points && points <= m3car.points)
        {
            player_current_position.text = string.Format("4th");
        }
        else{}
        
    }

    void OnTriggerEnter(Collider other) 
    {

        if ((other.CompareTag("cube2") && (flag.transform.parent == AIcar.AIcarjeep.gameObject.transform)) || 
            (other.CompareTag("cube3") && (flag.transform.parent == buggy.buggypos.gameObject.transform)) ||
            (other.CompareTag("cube4") && (flag.transform.parent == m3car.m3carpos.gameObject.transform)))
        {
            if (AIcar.notflag == false || buggy.notflag2 == false || m3car.notflag3 == false)
            {
                getflag();
                enemyCarScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                buggycarscript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                m3carScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                StartCoroutine(dont_take_flag());
            }
            else{}
        }
    }

    void OnCollisionEnter(Collision col)
    {
            if (col.gameObject.tag == "Flag")
            {
                //Console.Write(buggy.notflag2);
                if (AIcar.notflag == true && buggy.notflag2 == true && m3car.notflag3 == true)
                {
                    getflag();
                    //enemyCarScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                    AIcar.notflag = false;
                    buggy.notflag2 = false;
                    m3car.notflag3 = false;
                    StartCoroutine(dont_take_flag());
                }
            }
    }

    void getflag()
    {
        flag = GameObject.Find("flag_med");
        flag.transform.parent = this.gameObject.transform;
        flag.transform.position = new Vector3(pos1.transform.position.x, pos1.transform.position.y, pos1.transform.position.z);
        flag.transform.eulerAngles = new Vector3(pos1.transform.rotation.x, pos1.transform.rotation.y, pos1.transform.rotation.z);       
    }

    IEnumerator dont_take_flag()
    {
        yield return new WaitForSeconds(2.0f);       
        AIcar.notflag = true;
        buggy.notflag2 = true;
        m3car.notflag3 = true;
    }

    void DisplayScore(double pointstoDisplay)
    {
        points = (int)pointstoDisplay;
        pointstext.text = string.Format("{0:0}", points);
    }
    
}
