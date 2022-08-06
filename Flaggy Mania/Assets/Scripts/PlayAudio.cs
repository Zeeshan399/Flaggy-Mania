using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public GameObject AudioOb;
    // Start is called before the first frame update
    public void DropAudio()
    {
        Instantiate(AudioOb,transform.position,transform.rotation);
    }


}
