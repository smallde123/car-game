using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;//Creates array to put each tile in the editor

    private Transform playerTransform;//The player(car) transform
    private float spawnZ = -50.0f;//Starting Tile spawn behind the car(Increase if camera starts seeing off map)
    private float tileLength = 50.0f;//[IMPORTANT] MUST BE THE EXACT LENGTH OF 1 SINGLE TILE!
    private float safeZone = 70.0f;//Length car must pass before end tile deletes and new tile spawns
    private int amnTilesOnScreen = 4;//Number of tiles on screen at any given time
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

	// Use this for initialization
	void Start ()
    {
        activeTiles = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//Stores player(car) position into playerTransform

        for(int i = 0; i<amnTilesOnScreen; i++)
        {
            if (i < 4)//Number of tiles on screen at any given time
                SpawnTile(0);
            else
                SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Spawns tile if car passes safezone and deletes end tile
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
	}
    //Tile spawn function
    private void SpawnTile(int prefabIndex = 0)//-1
    {
        GameObject go;
        if (prefabIndex == 0)//-1
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    //Tile delete function
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    //Random Tile Function
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
            return randomIndex;
    }
}
