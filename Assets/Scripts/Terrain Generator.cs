using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //y-coordinate of ground
    private const float groundY = -2.2f;
    //ground speed (perceived as player speed)
    private const float speed = .001f;
    private const float leftmostX = -10;
    private const int groundWidth = 21;
    private const int groundDepth = 4;

    private GameObject[,] terrainTiles;

    public GameObject[] groundTileOptions;
    public GameObject[] undergroundTileOptions;
    public GameObject[] deepGroundTileOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //generate terrain
        terrainTiles = new GameObject[groundWidth, groundDepth];
        int currentDepth = 0;
        for (int index = 0; index < groundWidth; index++)
        {
            currentDepth = 0;

            //generate ground
            Vector3 position = new Vector3(leftmostX + index * 0.99f, groundY, 0);
            GameObject tile = Instantiate(groundTileOptions[Random.Range(0, groundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            terrainTiles[index, currentDepth++] = tile;

            //generate underground
            position.y -= 0.87f;
            tile = Instantiate(undergroundTileOptions[Random.Range(0, undergroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            terrainTiles[index, currentDepth++] = tile;

            //generate deep ground (2 layers)
            position.y -= 1;
            tile = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            terrainTiles[index, currentDepth++] = tile;
            position.y -= 1;
            tile = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            terrainTiles[index, currentDepth] = tile;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update terrain
        for (int column = 0; column < groundWidth; column++)
        {
            //holds new tiles
            GameObject[] newTerrain = null;
            for (int row = 0; row < groundDepth; row++)
            {
                GameObject tile = terrainTiles[column, row];
                //terrain generation
                if (tile.transform.position.x < leftmostX)
                {
                    //destroy offscreen tiles
                    Destroy(tile);
                    //generate new tiles
                    if (newTerrain == null)
                    {
                        newTerrain = generateTerrainRight();
                    }
                    terrainTiles[column, row] = newTerrain[row];
                }

                //move terrain
                Vector3 position = tile.transform.position;
                position.x -= speed;
                tile.transform.position = position;
            }
        }
    }

    private GameObject[] generateTerrainRight()
    {
        GameObject[] newTerrain = new GameObject[groundDepth];
        Vector3 position = new Vector3(leftmostX + groundWidth * 0.99f, groundY, 0);
        newTerrain[0] = Instantiate(groundTileOptions[Random.Range(0, groundTileOptions.Length - 1)], position, Quaternion.identity);
        position.y -= 0.87f;
        newTerrain[1] = Instantiate(undergroundTileOptions[Random.Range(0, undergroundTileOptions.Length - 1)], position, Quaternion.identity);
        position.y -= 1;
        newTerrain[2] = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
        position.y -= 1;
        newTerrain[3] = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
        //adjust rendering order
        foreach (GameObject tile in newTerrain)
        {
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        return newTerrain;
    }
}
