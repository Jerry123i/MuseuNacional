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
        {
            SpawnTile(emptyFloor, position);

            Color up = map.GetPixel(x, y + 1);
            Color down = map.GetPixel(x, y - 1);
            Color right = map.GetPixel(x + 1, y);
            Color left = map.GetPixel(x - 1, y);

            if(isEmptySlot(up) && isEmptySlot(down))
            {
                if (up == Color.white || up == Color.red)
                    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 180f));
                if (down == Color.white || down == Color.red)
                    SpawnTile(emptyWall, position);
            }
            else if (isEmptySlot(right) && isEmptySlot(left))
            {
                if (right == Color.white || right == Color.red)
                    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 90f));
                if (left == Color.white || left == Color.red)
                    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, -90f));
            }

            else if(isFloorSlot(left) && right == Color.black 
                && ((isFloorSlot(up) && down == Color.black) || (isFloorSlot(down) && up == Color.black)) 
                && (map.GetPixel(x+1,y-1) == Color.black || map.GetPixel(x+1,y+1) == Color.black))
            {
                SpawnWallFacingLeft(position);
            }

            else if(isFloorSlot(up) && down == Color.black 
                && ((isFloorSlot(right) && left == Color.black) || (isFloorSlot(left) && right == Color.black))
                &&(map.GetPixel(x - 1, y - 1) == Color.black || map.GetPixel(x + 1, y - 1) == Color.black))
            {
                SpawnWallFacingUp(position);
            }

            else if (isFloorSlot(right) && left == Color.black
                && ((isFloorSlot(down) && up == Color.black)||(isFloorSlot(up) && down == Color.black))
                && (map.GetPixel(x - 1, y + 1) == Color.black || map.GetPixel(x - 1, y - 1) == Color.black))
            {
                SpawnWallFacingRight(position);
            }

            else if((isFloorSlot(down) && up == Color.black
                && ((isFloorSlot(left) &&  right == Color.black )||(isFloorSlot(right) && left == Color.black)))
                && (map.GetPixel(x - 1, y + 1) == Color.black || map.GetPixel(x + 1, y + 1) == Color.black))
            {
                SpawnWallFacingDown(position);
            }

            else if((up == Color.white || up == Color.red) && down == Color.black)
            {
                SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 180f));
            }

            else if((down == Color.white || down == Color.red) && up == Color.black)
            {
                SpawnTile(emptyWall, position);
            }

            else if((right == Color.white || right == Color.red) && down == Color.black)
            {
                SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 90f));
            }

            else if ((left== Color.white || left == Color.red)&& right == Color.black)
            {
                SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, -90f));
            }




            //else if(right == Color.black && down == Color.black && (map.GetPixel(x+1,y-1) == Color.white))
            //{
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, -90f));
            //    SpawnTile(emptyWall, position);
            //}

            //else if(down == Color.black && left == Color.black && (map.GetPixel(x-1,y-1) == Color.white))
            //{
            //    SpawnTile(emptyWall, position);
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 90f));
            //}

            //else if(left == Color.black && up == Color.black && (map.GetPixel(x-1,y+1) == Color.white))
            //{
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 90f));
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0,0,180f));
            //}

            //else if(up == Color.black && right == Color.black && (map.GetPixel(x+1,y+1) == Color.white))
            //{
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 180f));
            //    SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, -90f));
            //}



        }

        if (pixelColor == Color.blue)
            SpawnTile(slotWall, position);


    }

    void SpawnWallFacingLeft(Vector2 position)
    {
        SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, -90f));
    }

    void SpawnWallFacingUp(Vector2 position)
    {
        SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 180f));
    }

    void SpawnWallFacingRight(Vector2 position)
    {
        SpawnTile(emptyWall, position, Quaternion.Euler(0, 0, 90f));
    }

    void SpawnWallFacingDown(Vector2 position)
    {
        SpawnTile(emptyWall, position);
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
