using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;
    public GameObject disConText;

    void LateUpdate()
    {
        player = GameObject.Find("playercar(Clone)").transform;
        Vector3 newposition = player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
    
}
