using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectPosition { FLOOR, WALL, FLOOR_WALL}
public enum Category { DINOSSAURO, NAODINOSSAURO }

[CreateAssetMenu(fileName = "NewMuseumObject", menuName = "ScriptableObject/Museum Object")]
public class MuseumObject : ScriptableObject
{
	public string name;
	public Texture image;
	public string description;
	public ObjectPosition position;
	public Category category;
}

