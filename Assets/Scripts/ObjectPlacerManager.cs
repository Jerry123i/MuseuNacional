using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPlacerManager : MonoBehaviour
{
	public static ObjectPlacerManager placer;

	public MenuManager menuManager;

	public MuseumObject heldObject;
	public Slot objectPlacementSelectedSlot;
	private Slot moveSlotSelectedSlot;

    public CameraHandler cameraHandler;

	public TextMeshProUGUI standTracker;
	public UnityEngine.UI.Button completeButton;

	public List<Slot> slots;

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
		slots = new List<Slot>(GameObject.FindObjectsOfType<Slot>());
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
		if(heldObject != null && objectPlacementSelectedSlot == null)
		{
			heldObject = null;
		}
		else if(heldObject != null && objectPlacementSelectedSlot != null)
		{
			Build();
		}
        else
        {
            objectPlacementSelectedSlot = null;
        }
	}

	private void Build()
	{
		objectPlacementSelectedSlot.Place(heldObject);
		FilledStands++;
        objectPlacementSelectedSlot = null;
		heldObject = null;
	}

	public void StartSlotMovment(Slot movingSlot)
	{
		for (int i = 0; i < slots.Count; i++) {
			if (slots[i].state == SlotStates.VOID && slots[i] != movingSlot)
				slots[i].animator.SetBool("WaitingPlacement", true);
		}

		moveSlotSelectedSlot = movingSlot;

	}

}
