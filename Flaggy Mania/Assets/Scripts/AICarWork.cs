using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICarWork : MonoBehaviour
{
    public GameObject cubePos;
    [HideInInspector]       //to hide flag gameobject in script
    public GameObject flag;
    public RCC_AICarController carScript;
    public RCC_AICarController buggycarscript;
    public RCC_AICarController m3carScript;
    public BuggyCar buggy;
    public M3car m3car;
    public bool notflag = true;
    public Playercollide playercar;
    public double Enemy1points;
    public Text pointstext;
    public GameObject AIcarjeep;
    public int points;

    // Start is called before the first frame update
    void Start()
    {        
        Enemy1points = 0;
        flag = GameObject.Find("flag_med");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag.transform.parent == this.gameObject.transform)
        {
            Enemy1points += Time.deltaTime;
            DisplayScore(Enemy1points);
        }
    }

   
    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Player" && (flag.transform.parent == playercar.playerCarPos.transform))   || 
            (col.gameObject.tag == "Flag") || (col.gameObject.tag == "enemy2" && flag.transform.parent == buggy.buggypos.transform) ||
            (col.gameObject.tag == "enemy3" && flag.transform.parent == m3car.m3carpos.transform))
            {
                if (notflag == true && ((buggy.notflag2 == false || buggy.notflag2 == true)  &&(m3car.notflag3 == false || m3car.notflag3 == true)))
                {
                    getflag();
                    carScript.navigationMode = RCC_AICarController.NavigationMode.FollowWaypoints;
                    buggycarscript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                    m3carScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                    //notflag = false;
                    StartCoroutine(dont_take_flag());                 
                }
                else
                {                    
                }
            }
    }
    

    void getflag()
    {
        flag = GameObject.Find("flag_med");
        flag.transform.parent = this.gameObject.transform;
        flag.transform.position = new Vector3(cubePos.transform.position.x, cubePos.transform.position.y, cubePos.transform.position.z);
        flag.transform.eulerAngles = new Vector3(cubePos.transform.rotation.x, cubePos.transform.rotation.y, cubePos.transform.rotation.z);
    }

    IEnumerator dont_take_flag()
    {
        yield return new WaitForSeconds(2.0f);
        notflag = false;
        buggy.notflag2 = true;
        m3car.notflag3 = true;
    }

    void DisplayScore(double pointstoDisplay)
    {
        points = (int)pointstoDisplay;
        pointstext.text = string.Format("{0:0}", points);
    }
}
