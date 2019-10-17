using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject tabPrefab;
	[SerializeField] private GameObject buttonPrefab;
	[SerializeField] private List<CategoryTab> categoryTabs;
	[SerializeField] private List<string> categoryNames;
	[SerializeField] private List<Sprite> categoryImages;
    [SerializeField] private GameObject objectsMenu;
	[SerializeField] private RectTransform toggleMenuButton;
	[SerializeField] private RectTransform toggleMenuArrow;

	private Object[] allObjects;

	private int selectedTabIndex;

	private bool isMenuOpen;

	public int tabRecoilHeight;
	public int screenHeight = 600;

	public int SelectedTabIndex { get => selectedTabIndex;

		set
		{
			selectedTabIndex = value;
			categoryTabs[selectedTabIndex].transform.SetSiblingIndex(categoryTabs.Count - 1);
		}

	}

	private void Awake()
	{
		SelectedTabIndex = 0;
	}

	private void Start()
	{
	//#if UNITY_ANDROID
	//		screenHeight = Screen.currentResolution.height;
	//#endif

		LoadObjects();
		CreateMenus();
	}

	private void LoadObjects()
	{
		allObjects = Resources.LoadAll("ScriptableObjects", typeof(MuseumObject));
	}

	private void CreateMenus()
	{
		for (int i = 0; i < categoryTabs.Count; i++)
		{
			CategoryTab categoryTab = categoryTabs[i];
			RectTransform tab = categoryTabs[i].tab;

			tab.anchorMin = new Vector2(i * (1.0f / categoryTabs.Count), 1.0f);
			tab.anchorMax = new Vector2((i + 1) * (1.0f / categoryTabs.Count), 1.0f);

			tab.offsetMin =  new Vector2(0, tab.offsetMin.y);
			tab.offsetMax =  new Vector2(0, tab.offsetMax.y);

			var j = i;

			tab.GetComponent<Button>().onClick.AddListener(() => { ChangeTab(j); });

			categoryTab.title.text = categoryNames[i];
			categoryTab.tabImage.sprite = categoryImages[i];
					
		}

		for (int i = 0; i < allObjects.Length; i++)
		{
			MuseumObject museumObject = (MuseumObject)allObjects[i];
			CategoryTab tab = categoryTabs[(int)museumObject.category];

			GameObject newButton = Instantiate(buttonPrefab);
			newButton.GetComponent<RectTransform>().SetParent(tab.content);
			ObjectButtonScript buttonScript = newButton.GetComponent<ObjectButtonScript>();

			buttonScript.SetObject(museumObject);

		}

		for (int i = 0; i < categoryTabs.Count; i++)
			categoryTabs[i].scrollbar.value = 1f;

	}
	
	void ChangeTab(int index)
	{
		Debug.Log($"Change Tab {index}");
		SelectedTabIndex = index;
	}

	public void SetScrollbar(bool state)
	{
		categoryTabs[SelectedTabIndex].GetComponentInChildren<ScrollRect>().enabled = state;
	}

    public void ToggleObjectsMenu()
    {
		//objectsMenu.SetActive(!objectsMenu.activeInHierarchy);

		if (!isMenuOpen)
		{
			RectTransform sample = categoryTabs[0].backgroundRect;
			float finalPosition = ((sample.anchorMax.y - sample.anchorMin.y) / 2f) + sample.anchorMin.y;

			finalPosition *= screenHeight;

			for (int i = 0; i < categoryTabs.Count; i++)
			{
				RectTransform bgrt = categoryTabs[i].backgroundRect;
				RectTransform tabrt = categoryTabs[i].tabRect;
				bgrt.DOMoveY(finalPosition, 0.8f);
				tabrt.DOMoveY(screenHeight - tabrt.sizeDelta.y / 2, 1.0f);

			}

			toggleMenuButton.DOMoveY((sample.anchorMin.y - 0.1f) * screenHeight, 0.8f);
			toggleMenuArrow.DORotate(new Vector3(0, 0, 180), 0.75f);

			isMenuOpen = true;
		}
		else
		{

			RectTransform sample = categoryTabs[0].backgroundRect;
			float finalPosition = ((sample.anchorMax.y - sample.anchorMin.y) * screenHeight) + screenHeight;

			for (int i = 0; i < categoryTabs.Count; i++)
			{
				RectTransform bgrt = categoryTabs[i].backgroundRect;
				RectTransform tabrt = categoryTabs[i].tabRect;

				bgrt.DOMoveY(finalPosition, 0.8f);
				tabrt.DOMoveY(screenHeight + tabrt.sizeDelta.y/2 - tabRecoilHeight, 1.0f);

			}

			toggleMenuButton.DOMoveY(screenHeight - tabRecoilHeight - toggleMenuButton.sizeDelta.y, 0.8f);
			toggleMenuArrow.DORotate(new Vector3(0, 0, 0), .75f);

			isMenuOpen = false;
		}


    }

}
