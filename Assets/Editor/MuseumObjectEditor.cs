using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MuseumObject))]
public class MuseumObjectEditor : Editor
{

	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical("Box");
		base.OnInspectorGUI();
		GUILayout.EndVertical();
		
		var mo = target as MuseumObject;

		//var w = EditorGUIUtility.currentViewWidth;
		//var h = w * (86f / 124f);

		//Rect baseRect = EditorGUILayout.GetControlRect(false, h);

		//if(mo.image != null)
		//	GUI.DrawTexture(baseRect, mo.image);

		GUILayout.Space(30f);

		mo.name = EditorGUILayout.TextField("Name",mo.name);
		mo.description = EditorGUILayout.TextField(mo.description, GUILayout.Height(30.0f));

		

	}

	public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
	{
		var mo = target as MuseumObject;

		return mo.image as Texture2D;
	}

}
