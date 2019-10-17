using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectPosition { FLOOR, WALL, FLOOR_WALL}
public enum Category { BOTANICA, VERTEBRADOS, INVERTEBRADOS, PALEONTOLOGIA,
						MINERAIS, ARQUEOLOGIA, ETNOLOGIA, ANTROPOLOGIA, PALACIO}

[CreateAssetMenu(fileName = "NewMuseumObject", menuName = "ScriptableObject/Museum Object")]
public class MuseumObject : ScriptableObject
{
	public string name;
	public Texture image;
	public float imageOffset;
	public string description;
	public ObjectPosition position;
	public Category category;
}

