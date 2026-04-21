using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //y-coordinate of ground
    private static float groundY = -2.2f;

    public GameObject[] groundTileOptions;
    public GameObject[] undergroundTileOptions;
    public GameObject[] deepGroundTileOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //generate terrain
        float leftmostX = -10;
        for (int index = 0; index < 22; index++)
        {
            //generate ground
            Vector3 position = new Vector3(leftmostX + index, groundY, 0);
            GameObject tile = Instantiate(groundTileOptions[Random.Range(0, groundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;

            //generate underground
            position.y -= 0.87f;
            tile = Instantiate(undergroundTileOptions[Random.Range(0, undergroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;

            //generate deep ground (2 layers)
            position.y -= 1;
            tile = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
            position.y -= 1;
            tile = Instantiate(deepGroundTileOptions[Random.Range(0, deepGroundTileOptions.Length - 1)], position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
