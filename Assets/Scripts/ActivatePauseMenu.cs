using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePauseMenu : MonoBehaviour {

	public GameObject menu; // Assign in inspector
	private bool isShowing;

	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			Debug.Log ("Pause Menu");
			isShowing = !isShowing;
			menu.SetActive(isShowing);
		}
	}

}
