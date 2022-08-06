using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


//public class NetworkConnectionManager : MonoBehaviourPunCallbacks
//{
//    public Button connect_to_master;
//    public Button join_random_room;
//    public GameObject game_join_panel;

//    public bool master_connect_try;
//    public bool join_room_try;
//    // Start is called before the first frame update
//    void Start()
//    {
//        game_join_panel.gameObject.SetActive(true);
//        master_connect_try = false;
//        join_room_try = false; 
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//        connect_to_master.gameObject.SetActive(!PhotonNetwork.IsConnected && !master_connect_try);
//        join_random_room.gameObject.SetActive(PhotonNetwork.IsConnected && !master_connect_try && !join_room_try);       

//    }
//    public void Connect_to_Master_btn()
//    {
//        PhotonNetwork.OfflineMode = false;
//        PhotonNetwork.NickName = "Zeeshan";
//        PhotonNetwork.AutomaticallySyncScene = true;
//        PhotonNetwork.GameVersion = "v1";

//        master_connect_try = true;
//        PhotonNetwork.ConnectUsingSettings();
//    }

//    public override void OnDisconnected(DisconnectCause cause)
//    {
//        base.OnDisconnected(cause);
//        master_connect_try = false;
//        join_room_try = false;
//        Debug.Log(cause);
//    }

//    public override void OnConnectedToMaster()
//    {
//        base.OnConnectedToMaster();
//        master_connect_try = false;
//        Debug.Log("Connected to Master successfully");
//        //game_join_panel.gameObject.SetActive(false);
//    }

//    public void OnClickConnectToRoom()
//    {
//        if (!PhotonNetwork.IsConnected)
//            return;

//        join_room_try = true;
//        //PhotonNetwork.CreateRoom("Peter's Game 1"); //Create a specific Room - Error: OnCreateRoomFailed
//        //PhotonNetwork.JoinRoom("Peter's Game 1");   //Join a specific Room   - Error: OnJoinRoomFailed  
//        PhotonNetwork.JoinRandomRoom();              //Join a random Room     - Error: OnJoinRandomRoomFailed  
//    }

//    public override void OnJoinRandomFailed(short returnCode, string message)
//    {
//        base.OnJoinRandomFailed(returnCode, message);
//        //no room available
//        //create a room (null as a name means "does not matter")
//        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
//    }

//    public override void OnJoinedRoom()
//    {
//        base.OnJoinedRoom();
//        join_room_try = false;
//        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
//        SceneManager.LoadScene("Multi_Player");
        
//    }

//    public override void OnCreateRoomFailed(short returnCode, string message)
//    {
//        base.OnCreateRoomFailed(returnCode, message);
//        Debug.Log(message);
//        base.OnCreateRoomFailed(returnCode, message);
//        join_room_try = false;
//    }
//}

public class NetworkConnectionManager : Photon.PunBehaviour
{
    public Transform spawnpoint;
    public Text statusText;
    public const string Version = "1.0";
    public const string Room_name = "Zeeshan";
    public string Player_Prefab_Name = "playercar";
    public GameObject LobbyUI;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(Version);    //Connect to Photon as configured in the PhotonServerSettings file.
    }

    void Update()
    {
        statusText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }
    
    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Connection Failed"+cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 8 };  //room is not visible so it can not be joined randomly by everybody...
        PhotonNetwork.JoinOrCreateRoom(Room_name, roomOptions, TypedLobby.Default);         //Join an existing room with the same name or make another room with this name and room properties and lobby u want a room to be listed in...
    }
    public override void OnJoinedRoom()
    {       
        LobbyUI.SetActive(false);          
        GameObject player = PhotonNetwork.Instantiate(Player_Prefab_Name, spawnpoint.position, spawnpoint.rotation, 0);     //spawnpoint made in hierarchy already
        Debug.Log("Object Instantiated.");
        PhotonNetwork.player.NickName = "Zee";
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)        //Called when a remote player entered the room. This PhotonPlayer is already added to the playerlist at this time.
    {        
        Debug.Log("New Player Connected");
        PhotonNetwork.player.NickName = "NickName";
    }
    
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)       //Called when a remote player left the room.This PhotonPlayer is already removed from the playerlist at this time.
    {
        Debug.Log("Player Disconnected");
    }
    public override void OnDisconnectedFromPhoton()     //Called after disconnecting from the Photon server.
    {
        base.OnDisconnectedFromPhoton();
        FindObjectOfType<MiniMapScript>().disConText.SetActive(true);
    }

}

