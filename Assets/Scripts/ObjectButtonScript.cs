﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ObjectButtonScript : MonoBehaviour, IPointerDownHandler
{
	public MuseumObject museumObject;
	public RawImage image;
	public TextMeshProUGUI label;
	public Animator animator;

	public void OnPointerDown(PointerEventData eventData)
	{
		ObjectPlacerManager.placer.heldObject = museumObject;
		ObjectPlacerManager.placer.menuManager.SetScrollbar(false);
        ObjectPlacerManager.placer.cameraHandler.enabled = false;
		animator.SetBool("Pressed", true);
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			animator.SetBool("Pressed", false);
		}
	}

	public void SetObject(MuseumObject o)
	{
		museumObject = o;
		image.texture = o.image;
		label.text = o.name;
	}

	public void OpenInfoCanvas()
	{
		InfoCanvas.instance.OpenCanvas(museumObject);
	}

}
