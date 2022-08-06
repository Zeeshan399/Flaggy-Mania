//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2021 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/RCC Repair Station")]
public class RCC_RepairStation : MonoBehaviour {

	private RCC_CarControllerV3 targetVehicle;

	void OnTriggerStay (Collider col) {

		if (targetVehicle == null) {

			if (col.gameObject.GetComponentInParent<RCC_CarControllerV3> ())
				targetVehicle = col.gameObject.GetComponentInParent<RCC_CarControllerV3> ();

		}

		if (targetVehicle)
			targetVehicle.repairNow = true;

	}

	void OnTriggerExit (Collider col) {

		if (col.gameObject.GetComponentInParent<RCC_CarControllerV3> ())
			targetVehicle = null;

	}

}
