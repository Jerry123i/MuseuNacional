using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class SlotColorButton : MonoBehaviour
{
    public Color color;
	public UnityEngine.UI.Image sprite;

	
	private void Update() {

#if UNITY_EDITOR

		if (sprite == null) return;
		sprite.color = color;
#endif
	}

	public void SetColor(Slot slot)
	{
		slot.ChangeColor(color);
	}

	public void SetColorTilemap(Tilemap tilemap)
	{
		tilemap.color = color;
	}

}
