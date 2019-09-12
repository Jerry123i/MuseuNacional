using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Texture2D map;
	[SerializeField]private float slotSize;

	public GameObject emptyFloor;
    public GameObject slotFloor;
	public GameObject emptyWall;
    public GameObject slotWall;

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

        if (pixelColor == Color.white)
            SpawnTile(emptyFloor, position);

        if (pixelColor == Color.red)
            SpawnTile(slotFloor, position);

        if (pixelColor == Color.black)
            SpawnTile(emptyWall, position);

        if (pixelColor == Color.blue)
        {
            Color up = map.GetPixel(x, y + 1);
            Color down = map.GetPixel(x, y - 1);
            Color right = map.GetPixel(x + 1, y);
            Color left = map.GetPixel(x - 1, y);

            if (up == Color.white && down == Color.black)
                SpawnWallFacingUp(position);
            else if (right == Color.white && left == Color.black)
                SpawnWallFacingRight(position);
            else if (down == Color.white && up == Color.black)
                SpawnWallFacingDown(position);
            else if (left == Color.white && right == Color.black)
                SpawnWallFacingLeft(position);
            else
                SpawnTile(emptyFloor, position);

        }


    }

    void SpawnWallFacingLeft(Vector2 position)
    {
        SpawnTile(slotWall, position, Quaternion.Euler(0, 0, -90f));
    }

    void SpawnWallFacingUp(Vector2 position)
    {
        SpawnTile(slotWall, position, Quaternion.Euler(0, 0, 180f));
    }

    void SpawnWallFacingRight(Vector2 position)
    {
        SpawnTile(slotWall, position, Quaternion.Euler(0, 0, 90f));
    }

    void SpawnWallFacingDown(Vector2 position)
    {
        SpawnTile(slotWall, position);
    }

    void SpawnTile(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        Instantiate(prefab, position, rotation);
    }

    void SpawnTile(GameObject prefab, Vector2 position)
    {
        Quaternion rotation = Quaternion.identity;
        Instantiate(prefab, position, rotation);
    }

    bool isEmptySlot(Color pixel)
    {
        return (pixel == Color.clear || pixel == Color.white || pixel == Color.red);
    }

    bool isFloorSlot(Color pixel)
    {
        return (pixel == Color.white || pixel == Color.red);
    }

}
