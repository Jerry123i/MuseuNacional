using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotStates { VOID, EMPTY, FULL};

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
	public ObjectPosition slotType;
	public SlotStates state;
	public MuseumObject placedObject;

	public Canvas canvas;
	public GameObject colorCanvas;
	public GameObject colorButton;

	private float clock;
	private IEnumerator holdMouseRoutine;

	public SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;
	[SerializeField] private Sprite voidSprite;

	public Animator animator;

	public bool debug;

	private void FixedUpdate()
	{
		debug = IsMouseOnTopOfObject();
	}

	private void Awake()
	{
		canvas.enabled = false;
		placedObject = null;

		if (state == SlotStates.VOID)
		{
			spriteRenderer.enabled = false;
			animator.SetBool("IsVoid", true);
		}

	}

	public void Place(MuseumObject museumObject)
	{
		if (placedObject != null)
			return;

		placedObject = museumObject;
		spriteRenderer.sprite = fullSprite;
		animator.SetTrigger("Place");
		animator.SetBool("IsVoid", false);
		state = SlotStates.FULL;
	}

	public void Empty()
	{
		if(placedObject == null)
			return;

		animator.SetTrigger("Empty");
		placedObject = null;
		spriteRenderer.sprite = emptySprite;
		ObjectPlacerManager.placer.FilledStands--;
		state = SlotStates.EMPTY;
	}

	public void Remove()
	{
		placedObject = null;
		animator.SetBool("IsVoid", true);
		state = SlotStates.VOID;
	}

	public void Replace(Slot slot)
	{
		if (slot.placedObject != null)
			Place(slot.placedObject);
		else
			state = SlotStates.EMPTY;

		animator.SetBool("IsVoid", false);
		animator.SetBool("WaitingPlacement", false);

		spriteRenderer.color = slot.spriteRenderer.color;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ObjectPlacerManager.placer.objectPlacementSelectedSlot = this;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StartCoroutine(LateOnPointerExit());
	}

	private IEnumerator LateOnPointerExit()
	{
		yield return new WaitForEndOfFrame();
		if (ObjectPlacerManager.placer.objectPlacementSelectedSlot == this)
			ObjectPlacerManager.placer.objectPlacementSelectedSlot = null;

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
			if(state == SlotStates.VOID && !IsMouseOnTopOfObject())
			{
				animator.SetBool("WaitingPlacement", false);
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

	public void StartSlotReplacement()
	{
		ObjectPlacerManager.placer.StartSlotReplacement(this);
	}

	public void PlaceSlotHere()
	{
		ObjectPlacerManager.placer.ReplaceSlot(this);
	}

	public void ChangeColor(Color color)
	{
		spriteRenderer.color = color;
	}


}
