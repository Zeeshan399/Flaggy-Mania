using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapscript_singleplayer : MonoBehaviour
{
    public Transform player;
    void LateUpdate()       //Runs after update and fixed update...updates after the player has moved
    {        
        Vector3 newposition = player.position;          //The player(car) position is being stored in a new vector3 variable
        newposition.y = transform.position.y;           //The y transform needs to be at a distance so the y transform of camera which is above the player is saved to the newvector y transform position
        transform.position = newposition;               //The newposition is then stored in transform.position which is the tranform of camera...it means that minimap camera updates its position 
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);      //This is for the rotation of minimap camera as the player rotates
    }
    
}
