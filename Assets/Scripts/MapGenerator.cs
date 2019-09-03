using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Texture2D map;
	[SerializeField]private float slotSize;

	public GameObject floor;
	public GameObject wall;

    void Start()
    {
		GenerateMap();
    }

	void GenerateMap()
	{
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x,y);
			}
		}
	}

	void GenerateTile(int x , int y)
	{
		Color pixelColor = map.GetPixel(x, y);

		if (pixelColor.a == 0)
			return;

		Vector2 position = new Vector2(x * slotSize, y * slotSize);

		Instantiate(floor, position, Quaternion.identity);


	}
    
}
