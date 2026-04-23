using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    private const float rightMostX = 10;
    private const float y = -2.2f;
    private const float speed = 7f;
    private const float spawnCooldown = 5f;
    private float spawnAllowed;
    private GameObject[] currentBuildings;
    private int buildingsUp;
    public GameObject[] buildingOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBuildings = new GameObject[5];
        buildingsUp = 0;
        spawnAllowed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //generate buildings
        if (Random.Range(0, 10000) < 3 && buildingsUp < currentBuildings.Length)
        {
            Vector3 position = new Vector3(rightMostX, y, 0);
            GameObject newBuilding = Instantiate(buildingOptions[Random.Range(0, buildingOptions.Length - 1)], position, Quaternion.identity);
            bool spotFound = false;
            int index = 0;
            while (!spotFound)
            {
                if (currentBuildings[index] == null)
                {
                    currentBuildings[index] = newBuilding;
                    spotFound = true;
                }
                index++;
            }
            buildingsUp++;
        }

        //update buildings
        for (int index = 0; index < currentBuildings.Length; index++)
        {
            GameObject building = currentBuildings[index];
            if (building != null)
            {
                //destroy buildings
                if (building.transform.position.x <= -10)
                {
                    Destroy(building);
                    currentBuildings[index] = null;
                    buildingsUp--;
                } else
                {
                    Vector3 position = currentBuildings[index].transform.position;
                    position.x -= speed * Time.deltaTime;
                    currentBuildings[index].transform.position = position;
                }
            }
        }
    }
}
