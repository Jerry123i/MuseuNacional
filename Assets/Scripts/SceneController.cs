using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public void GoToScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void GoToTitle()
	{
		SceneManager.LoadScene("InitialScene");
	}

	public void GoToGame()
	{
		SceneManager.LoadScene("IsoSceneTest");
	}
}
