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
		label.text = o.name;
		RectTransform rt = image.rectTransform;

		if (o.image == null) return;

		//float buttonImageProportion = (rt.anchorMax.x - rt.anchorMin.x) / (rt.anchorMax.y - rt.anchorMin.y);

		float buttonImageProportion = 124f / 86f;
		float inverseButtonImageProportion = 86f / 124f;

		float originalImageProportion = (float)o.image.width / (float)o.image.height;
		float inverseOriginalImageProportion = (float)o.image.height / (float)o.image.width;

		Debug.Log($"{o.name}: {o.image.width}/{o.image.height} ({originalImageProportion} vs {buttonImageProportion})");

		image.texture = o.image;

		if (originalImageProportion < 1)
		{
			image.uvRect = new Rect(0, 0, 1, originalImageProportion* inverseButtonImageProportion);

		}
		else if (originalImageProportion > 1)
		{

			image.uvRect = new Rect(0, 0, inverseOriginalImageProportion* buttonImageProportion, 1);
		}

	}

	public void OpenInfoCanvas()
	{
		InfoCanvas.instance.OpenCanvas(museumObject);
	}

}
