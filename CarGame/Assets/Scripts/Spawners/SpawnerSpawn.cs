using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class SpawnerSpawn : MonoBehaviour
    {
        public Transform[] spawnerSpawn;//Pick random spawn point of list

        public GameObject[] prefab;//Pick random prefab to spawn of list

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;

            int prefab_num = Random.Range(0, 3);

            Instantiate(prefab[prefab_num], spawnerSpawn[prefab_num].position, spawnerSpawn[prefab_num].rotation);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
