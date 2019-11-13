using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
	public ObjectPosition slotType;
	public MuseumObject placedObject;

	public Canvas canvas;
	public GameObject colorCanvas;
	public GameObject colorButton;

	private float clock;
	private IEnumerator holdMouseRoutine;

	[SerializeField]private SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;

	[SerializeField] private Animator animator;

	public bool debug;

	private void FixedUpdate()
	{
		debug = IsMouseOnTopOfObject();
	}

	private void Awake()
	{
		canvas.enabled = false;
		placedObject = null;
	}

	public void Place(MuseumObject museumObject)
	{
		if (placedObject != null)
			return;

		placedObject = museumObject;
		spriteRenderer.sprite = fullSprite;
		animator.SetTrigger("Place");
	}

	public void Empty()
	{
		if(placedObject == null)
			return;

		animator.SetTrigger("Empty");
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
		
		OpenCanvas();			
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (IsFull())
		{
			holdMouseRoutine = CountMouseHoldClock();
			StartCoroutine(holdMouseRoutine);
		}
	}

	private IEnumerator CountMouseHoldClock()
	{
		clock = 0;
		while(clock <= 1.0f)
		{
			clock += Time.deltaTime;
			yield return null;
		}
		CloseCanvas();
		InfoCanvas.instance.OpenCanvas(placedObject);

	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (CanvasIsOpen() && !IsMouseOnTopOfObject())
			{
				CloseCanvas();
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (holdMouseRoutine != null)
				StopCoroutine(holdMouseRoutine);
		}
	}

	public void OpenCanvas()
	{
		colorButton.SetActive(true);
		canvas.enabled = true;
	}
	public void CloseCanvas()
	{
		colorCanvas.SetActive(false);
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

	public bool IsMouseOnTopOfObject() //Acertar a mira disso

	{
		//Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePosition = Input.mousePosition;
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		//Vector2 position = transform.position;
		Vector2 position = Camera.main.WorldToScreenPoint(transform.position);

		//position += collider.offset;

		return (mousePosition - position).magnitude < 70.0f; //Falta multiplicar por uma funcao do tamanho da camera isso aqui

	}

	public void ToggleColorCanvas()
	{
		colorButton.SetActive(colorCanvas.activeInHierarchy);
		colorCanvas.SetActive(!colorCanvas.activeInHierarchy);
	}
	public void ChangeColor(Color color)
	{
		spriteRenderer.color = color;
	}


}
