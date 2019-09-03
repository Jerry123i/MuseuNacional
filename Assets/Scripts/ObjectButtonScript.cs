using System.Collections;
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


	public void OnPointerDown(PointerEventData eventData)
	{
		ObjectPlacerManager.placer.heldObject = museumObject;
		ObjectPlacerManager.placer.menuManager.SetScrollbar(false);
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
