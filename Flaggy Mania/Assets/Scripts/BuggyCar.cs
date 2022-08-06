using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuggyCar : MonoBehaviour
{
    public GameObject cubePos;
    [HideInInspector]
    public GameObject flag;
    public RCC_AICarController carScript;
    public RCC_AICarController jeepCarScript;
    public RCC_AICarController m3CarScript;
    public AICarWork AIcar;
    public M3car m3car;
    public bool notflag2 = true;
    public Playercollide playercar;
    public double Enemy2points;
    public Text pointstext;
    public GameObject buggypos;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        Enemy2points = 0;
        flag = GameObject.Find("flag_med");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag.transform.parent == this.gameObject.transform)
        {
            Enemy2points += Time.deltaTime;
            DisplayScore(Enemy2points);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Player" && (flag.transform.parent == playercar.playerCarPos.transform)) || 
            (col.gameObject.tag == "Flag") || (col.gameObject.tag == "enemy1" && (flag.transform.parent == AIcar.AIcarjeep.transform)) ||
            (col.gameObject.tag == "enemy3" && (flag.transform.parent == m3car.m3carpos.transform)))
        {
            if (notflag2 == true && ((AIcar.notflag == false || AIcar.notflag == true) &&(m3car.notflag3 == false || m3car.notflag3 == true)))
            {
                getflag();
                carScript.navigationMode = RCC_AICarController.NavigationMode.FollowWaypoints;
                jeepCarScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
                m3CarScript.navigationMode = RCC_AICarController.NavigationMode.ChaseTarget;
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
        notflag2 = false;
        AIcar.notflag = true;
        m3car.notflag3 = true;
    }

    void DisplayScore(double pointstoDisplay)
    {
        points = (int)pointstoDisplay;
        pointstext.text = string.Format("{0:0}", points);
    }
}
