using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject tabPrefab;
	[SerializeField] private GameObject buttonPrefab;
	[SerializeField] private List<CategoryTab> categoryTabs;
	[SerializeField] private List<string> categoryNames;

	private Object[] allObjects;

	private int selectedTabIndex;

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

	private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			LoadObjects();
			CreateMenus();
		}
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

}
