using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public ObjectPosition slotType;
	public MuseumObject placedObject;

	[SerializeField]private SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;

	public bool IsFull()
	{
		return placedObject != null;
	}

	public void Place(MuseumObject museumObject)
	{
		placedObject = museumObject;
		spriteRenderer.sprite = fullSprite;
	}

	public void Empty()
	{
		placedObject = null;
		spriteRenderer.sprite = emptySprite;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ObjectPlacerManager.placer.selectedSlot = this;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StartCoroutine(LateOnPointerExit());
	}

	private IEnumerator LateOnPointerExit()
	{
		yield return new WaitForEndOfFrame();
		if (ObjectPlacerManager.placer.selectedSlot == this)
			ObjectPlacerManager.placer.selectedSlot = null;

	}

}
