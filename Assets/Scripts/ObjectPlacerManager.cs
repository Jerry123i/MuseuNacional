using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPlacerManager : MonoBehaviour
{
	public static ObjectPlacerManager placer;

	public MenuManager menuManager;

	public MuseumObject heldObject;
	public Slot selectedSlot;

    public CameraHandler cameraHandler;

	public TextMeshProUGUI standTracker;
	public UnityEngine.UI.Button completeButton;

	public int maxStands;
	private int filledStands;

	
	public int FilledStands { get => filledStands;

		set {
			filledStands = value;
			standTracker.text = ($"{filledStands}/{maxStands}");
			completeButton.interactable = filledStands >= maxStands;
		} }

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
		FilledStands = 0;
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
        cameraHandler.enabled = true;
		if(heldObject != null && selectedSlot == null)
		{
			heldObject = null;
		}
		else if(heldObject != null && selectedSlot != null)
		{
			Build();
		}
        else
        {
            selectedSlot = null;
        }
	}

	private void Build()
	{
		selectedSlot.Place(heldObject);
		FilledStands++;
        selectedSlot = null;
		heldObject = null;
	}

}
