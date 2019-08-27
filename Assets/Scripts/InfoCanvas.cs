using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoCanvas : MonoBehaviour
{
	[SerializeField] private RawImage image;
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private GameObject background;

	public static InfoCanvas instance;

	private void Awake()
	{
		if (instance!= null)
		{
			if (instance != this)
				Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	public void SetInfo(MuseumObject museumObject)
	{
		image.texture = museumObject.image;
		title.text = museumObject.name;
		descriptionText.text = museumObject.description;
	}

	public void OpenCanvas(MuseumObject museumObject)
	{
		SetInfo(museumObject);
		background.SetActive(true);
	}

	public void CloseCanvas()
	{
		background.SetActive(false);
	}

}

