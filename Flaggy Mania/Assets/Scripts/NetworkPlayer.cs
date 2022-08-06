
//This script prevent control of other player's cars


using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : Photon.MonoBehaviour,IPunObservable
{
    public GameObject localCam;
    Vector3 realposition;
    Quaternion realrotation;
  

    void Start()
    {
        
        if (!photonView.isMine)
        {
            localCam.SetActive(false);
            
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i] is NetworkPlayer) { }

                else if (scripts[i] is PhotonView) { }

                else if (scripts[i] is Multi_playercar) { }              

                else { scripts[i].enabled = false; }
            }
        }
        else { }
    }

    void Update()
    {
        if (photonView.isMine) {}
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, realposition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realrotation, 0.1f);
          
        }
    }


        //in this function every player will send its own data of position and rotation to other remote players in stream.writing
        //and the remote players will receive data in the else portion...This whole function is obviously on every player individuall so every car
        //sends and receives some data about other players
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            
        }

        else
        {
            realposition = (Vector3)stream.ReceiveNext();
            realrotation = (Quaternion)stream.ReceiveNext();
           
        }
    }
}

