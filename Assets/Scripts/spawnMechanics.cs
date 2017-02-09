using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMechanics : MonoBehaviour {

    [SerializeField]
    GameObject pointcube;

    [Space]
    [Header("Spawnpoints")]
    [SerializeField]
    GameObject throttleSpawn;

    [SerializeField]
    GameObject breakSpawn;

    [SerializeField]
    GameObject clutchSpawn;

    [Space]
    [Header("Variables")]
    [SerializeField]
    bool spawning = false;

    [SerializeField]
    int maxChain;
    [SerializeField]
    [Range(0,10)]
    int chainProbabilty;
    [SerializeField]
    float spawnTime;

    float spawnTimer;

   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (spawning && spawnTimer >= spawnTime)
        {
            float randomSpawn = Random.Range(0, 10f);
            if(randomSpawn < chainProbabilty)
            {
                spawnCube(true);
            }
            else spawnCube(false);

            spawnTimer = 0;
        }
        else
        {
            float randomTime = Random.Range(0.7f, 1.7f);
            
            spawnTimer += randomTime * Time.deltaTime;
        }

	}

    void spawnCube(bool chain)
    {
        float randomType = Random.Range(0, 0.9f);


        if(randomType < 0.3f)
        {
            if (chain)
            {
                int cubes = Random.Range(0, maxChain);
                GameObject[] chainObj = new GameObject[cubes];

                for (int i = 0; i < cubes; i++)
                {
                    GameObject tempCube = Instantiate<GameObject>(pointcube, throttleSpawn.transform.position, throttleSpawn.transform.rotation);
                    Vector3 newPos = tempCube.transform.position;
                    newPos.y += i;
                    tempCube.transform.position = newPos;
                    tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.THROTTLE);
                    chainObj[i] = tempCube;
                }
                chainCubes(chainObj);

            }
            else
            {
                GameObject tempCube = Instantiate<GameObject>(pointcube, throttleSpawn.transform.position, throttleSpawn.transform.rotation);
                tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.THROTTLE);
            }
            
        }
        else if(randomType > 0.6f)
        {
            if (chain)
            {
                int cubes = Random.Range(0, 5);
                GameObject[] chainObj = new GameObject[cubes];

                for (int i = 0; i < cubes; i++)
                {
                    GameObject tempCube = Instantiate<GameObject>(pointcube, breakSpawn.transform.position, breakSpawn.transform.rotation);
                    Vector3 newPos = tempCube.transform.position;
                    newPos.y += i;
                    tempCube.transform.position = newPos;
                    tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.BREAKS);
                    chainObj[i] = tempCube;
                }
                chainCubes(chainObj);

            }
            else
            {
                GameObject tempCube = Instantiate<GameObject>(pointcube, breakSpawn.transform.position, breakSpawn.transform.rotation);
                tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.BREAKS);
            }
           
            
        }
        else
        {
            if (chain)
            {
                int cubes = Random.Range(0, 5);
                GameObject[] chainObj = new GameObject[cubes];

                for (int i = 0; i < cubes; i++)
                {
                    GameObject tempCube = Instantiate<GameObject>(pointcube, clutchSpawn.transform.position, clutchSpawn.transform.rotation);
                    Vector3 newPos = tempCube.transform.position;
                    newPos.y += i;
                    tempCube.transform.position = newPos;
                    tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.CLUTCH);
                    chainObj[i] = tempCube;
                }
                chainCubes(chainObj);

            }
            else
            {
                GameObject tempCube = Instantiate<GameObject>(pointcube, clutchSpawn.transform.position, clutchSpawn.transform.rotation);
                tempCube.GetComponent<cubeMechanics>().setType(cubeMechanics.actionType.CLUTCH);
            }
            
        }


    }

    void chainCubes(GameObject[] objs)
    {
        for (int i = 1; i < objs.Length; i++)
        {
            if(i < objs.Length)
            {
                objs[i-1].GetComponent<cubeMechanics>().follower = objs[i];
            }
            else
            {
                objs[i].GetComponent<cubeMechanics>().follower = null;
            }
        }
    }
}
