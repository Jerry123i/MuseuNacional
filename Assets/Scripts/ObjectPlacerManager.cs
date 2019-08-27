using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacerManager : MonoBehaviour
{
	public static ObjectPlacerManager placer;

	public MenuManager menuManager;

	public MuseumObject heldObject;
	public Slot selectedSlot;

	private void Awake()
	{
		if(placer != null)
		{
			if (placer != this)
				Destroy(this);
		}
		else
		{
			placer = this;
		}

		menuManager = GetComponent<MenuManager>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			TouchUp();
		}
	}

	private void TouchUp()
	{
		menuManager.SetScrollbar(true);
		if(heldObject != null && selectedSlot == null)
		{
			heldObject = null;
		}
		else if(heldObject != null && selectedSlot != null)
		{
			Build();
		}
	}

	private void Build()
	{
		selectedSlot.Place(heldObject);
		heldObject = null;
	}

}
