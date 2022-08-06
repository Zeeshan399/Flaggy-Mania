using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside1 : MonoBehaviour {

	public Transform MinimapCam;
	public float MinimapSize;
	Vector3 TempV3;

	//updating the position of the minimap icon of flag
	void Update () {
		TempV3 = transform.parent.transform.position;		//taking the position of flag in tempv3...flag is the parent of flagminimapicon
		TempV3.y = transform.position.y;					//saving the y tranform of minimap in tempv3
		transform.position = TempV3;						//updating the minimap icon position by giving it the transform of tempv3
	}

	void LateUpdate () {
		// Center of Minimap
		Vector3 centerPosition = MinimapCam.transform.localPosition;

		// Just to keep a distance between Minimap camera and this Object (So that camera don't clip it out)
		centerPosition.y -= 2f;

		// Distance from the gameObject to Minimap
		float Distance = Vector3.Distance(transform.position, centerPosition);

		// If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
		// But if the Distance is greater than the MinimapSize, then do this
		if (Distance > MinimapSize)
		{
			// Gameobject - Minimap
			Vector3 fromOriginToObject = transform.position - centerPosition;

			// Multiply by MinimapSize and Divide by Distance
			fromOriginToObject *= MinimapSize / Distance;

			// Minimap + above calculation
			transform.position = centerPosition + fromOriginToObject;
		}
	}
}
