using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnSystem : MonoBehaviour
{

    public Transform spawner;
    public int carPrefabHighRange;
    public GameObject[] carPrefab;
    public float minWait;
    public float maxWait;

    private bool isSpawning;

    // Start is called before the first frame update
    void Awake()
    {
        isSpawning = false;

        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnCar", timer);
            isSpawning = true;
        }
    }

    void SpawnCar()
    {
        int prefab_num = Random.Range(0, carPrefabHighRange);
        Instantiate(carPrefab[prefab_num], spawner.position, spawner.rotation);
        isSpawning = false;
    }
}
