using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollerScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public Scrollbar scrollbar;

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
		FindObjectOfType<CameraHandler>().enabled = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("OnPointerUp");
		FindObjectOfType<PCCameraHandler>().enabled = true;
	}
}
