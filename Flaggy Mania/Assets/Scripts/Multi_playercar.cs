using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class Multi_playercar : Photon.MonoBehaviour, IPunObservable
{
    public GameObject pos1;
    [HideInInspector]
    public GameObject flag;
    public double playerpoints;
    public Text pointstext;
    public Text player_current_position;
    public GameObject playerCarPos;
    public int points;
    public bool not_take_flag = false;
    public GameObject minimapcamera;
    public Text HighestPointText;
    public static double highestPoint;
    public Canvas game_over;
    public Canvas RCCcanvas;
    public Text winorlose;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        flag = GameObject.Find("Multi_Flag");
        pointstext = GameObject.Find("yourpoints").GetComponent<Text>();
        HighestPointText = GameObject.Find("enemypoints").GetComponent<Text>();      
        winorlose = GameObject.Find("you_winorlose").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (photonView.isMine)
        {
            if (playerpoints >= 100)
            {
                GameObject.Find("RCCCanvas_Multi").gameObject.SetActive(false);
                Time.timeScale = 0f;
                GameObject.Find("Canvas").transform.GetChild(9).gameObject.SetActive(true);
                //GameObject.Find("RCCCanvas_Multi").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.GetChild(9).transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "You Win";
            }
        }
        else
        {
            if (playerpoints >= 100)
            {
                GameObject.Find("RCCCanvas_Multi").gameObject.SetActive(false);
                Time.timeScale = 0f;
                GameObject.Find("Canvas").transform.GetChild(9).gameObject.SetActive(true);
                //GameObject.Find("RCCCanvas_Multi").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.GetChild(9).transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "You Lose";
            }
        }
        
    }

    void FixedUpdate()
    {
       
        if (flag.transform.parent == this.gameObject.transform)
        {
            playerpoints += Time.deltaTime;
            if (this.photonView.isMine)
            {
            points = (int)playerpoints;
            pointstext.text = string.Format("{0:0}", points);
            }
        }

        if (highestPoint < playerpoints)
        {
            highestPoint = playerpoints;
        }
        HighestPointText.text = string.Format("{0:0}", highestPoint);

    }
    
    void OnTriggerEnter(Collider other)
    {
         
            if (other.gameObject.tag == "Flag" && not_take_flag == false)
            {
            if (flag.transform.parent == this.gameObject.transform) {}

            else
            {
                //not_take_flag = true;
                this.photonView.RPC("getflag", PhotonTargets.All);
                this.photonView.RPC(" StartCoroutine(dont_take_flag())", PhotonTargets.All);
                StartCoroutine(dont_take_flag());
            }              
            }   
         
    }
    [PunRPC]
    IEnumerator dont_take_flag()
    {
        
            not_take_flag = true;
            flag.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(3.0f);
            flag.GetComponent<BoxCollider>().enabled = true;
            not_take_flag = false;
        
       
    }

    [PunRPC]
    void getflag()
    {      
      
            flag = GameObject.Find("Multi_Flag");
            flag.transform.parent = this.gameObject.transform;
            flag.transform.position = new Vector3(pos1.transform.position.x, pos1.transform.position.y, pos1.transform.position.z);
            flag.transform.eulerAngles = new Vector3(pos1.transform.rotation.x, pos1.transform.rotation.y, pos1.transform.rotation.z);   
           
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
    
    void DisplayScore(double pointstoDisplay)
    {
        
        points = (int)pointstoDisplay;
        pointstext.text = string.Format("{0:0}", points);
        
    }

}

