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
		StartCoroutine(ResizeObjectRoutine(museumObject));
	}

	IEnumerator ResizeObjectRoutine(MuseumObject museumObject)
	{
		RectTransform rectTransform = image.rectTransform;

		image.texture = museumObject.image;
		rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0);
		rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0);

		yield return new WaitForEndOfFrame();

		float newImageHeight = 100;

		float objectWidth = (newImageHeight * museumObject.image.width) / museumObject.image.height;

		Debug.Log($"{newImageHeight}*{museumObject.image.width}/{museumObject.image.height}");

		rectTransform.sizeDelta = new Vector2(objectWidth * 1.75f, 0);

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

