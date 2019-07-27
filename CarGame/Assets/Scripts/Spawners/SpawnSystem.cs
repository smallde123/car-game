using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class SpawnSystem : MonoBehaviour
    {
        public int spawnHighRange;
        [Space(10)]
        public int prefabHighRange;

        public Transform[] spawner;

        public GameObject[] prefab;

        // Start is called before the first frame update
        void Start()
        {
            int spawner_num = Random.Range(0, spawnHighRange);

            int prefab_num = Random.Range(0, prefabHighRange);

            Instantiate(prefab[prefab_num], spawner[spawner_num].position, spawner[spawner_num].rotation);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
