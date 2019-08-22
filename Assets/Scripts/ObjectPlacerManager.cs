using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacerManager : MonoBehaviour
{
	public static ObjectPlacerManager placer;

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
