using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptionsScript : MonoBehaviour
{
	private Animator playerOptionsAnimator;

	private void Awake() {
		playerOptionsAnimator = GetComponent<Animator>();
	}

	public void ToggleWallColorPallet() {
		playerOptionsAnimator.SetBool("colorCanvas", !playerOptionsAnimator.GetBool("colorCanvas"));	

	}


}
