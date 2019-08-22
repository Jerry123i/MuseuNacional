using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectButtonScript : MonoBehaviour, IPointerDownHandler
{
	public MuseumObject museumObject;

	public void OnPointerDown(PointerEventData eventData)
	{
		ObjectPlacerManager.placer.heldObject = museumObject;
	}
}
