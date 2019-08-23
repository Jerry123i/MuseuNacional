using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public ObjectPosition slotType;
	public MuseumObject placedObject;

	public Canvas canvas;

	[SerializeField]private SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;

	private void Awake()
	{
		canvas.enabled = false;
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

	public void OnPointerClick(PointerEventData eventData)
	{
		if (IsFull())
		{
			OpenCanvas();
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (CanvasIsOpen() && !IsMouseOnTopOfObject())
				CloseCanvas();
		}
	}

	public void OpenCanvas()
	{
		canvas.enabled = true;
	}
	public void CloseCanvas()
	{
		canvas.enabled = false;
	}

	public bool IsFull()
	{
		return placedObject != null;
	}

	public bool CanvasIsOpen()
	{
		return canvas.isActiveAndEnabled;
	}

	public bool IsMouseOnTopOfObject()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		Vector2 position = transform.position;

		position += collider.offset;

		return ((mousePosition.x > position.x - (collider.size.x / 2) && mousePosition.x < position.x + (collider.size.x / 2)) &&
			(mousePosition.y > position.y - (collider.size.x / 2) && mousePosition.y < position.y + collider.size.y / 2));

	}


}
