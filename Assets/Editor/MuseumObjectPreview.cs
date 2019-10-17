using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPreview(typeof(MuseumObject))]
public class MuseumObjectPreview : ObjectPreview
{
	//public override void OnPreviewGUI(Rect r, GUIStyle background)
	//{
	//	var mo = target as MuseumObject;
			
	//	GUI.DrawTexture(r, mo.image);

	//	base.OnPreviewGUI(r, background);
	//}

	//public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
	//{
	//	var mo = target as MuseumObject;

	//	GUI.DrawTexture(r, mo.image);

	//	//base.OnInteractivePreviewGUI(r, background);
	//}

	//public override bool HasPreviewGUI()
	//{
	//	var mo = target as MuseumObject;
	//	return mo.image != null;
	//}

}
