using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpManager : MonoBehaviour      //manages and spawns power ups
{
    private MeshFilter meshFilter;
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private int[] powerUpWeights;

    private List<GameObject> currentPowerUps = new List<GameObject>();
    public int maxPowerUps;
    public int spawnInterval = 3;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private Vector3 getPosition()
    {
        var vertices = meshFilter.sharedMesh.vertices;
        //gets bottom left and upper right point of spawn area
        var max = (transform.TransformPoint(vertices[0]));
        var min = (transform.TransformPoint(vertices[120]));
        var randomSpawnPoint = new Vector3(Random.Range(min.x,max.x),Random.Range(min.y,max.y), 0);
        return randomSpawnPoint;
    }

    public void RemovePowerUp(GameObject powerUpToRemove)
    {
        Debug.Log("RemovePowerUp: "+currentPowerUps.Contains(powerUpToRemove));
        currentPowerUps.Remove(powerUpToRemove);
    }

    private int GetRandomWeightedIndex(int[] weights)
    {
        int totalWeight = weights.Sum();
        float r = Random.value; //random value between 0 and 1
        float s = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] <= 0) continue; //in case r=0

            s += (float)weights[i] / totalWeight;
            if (s >= r) return i;
        }
        return -1; 
    }

    IEnumerator SpawnObjects()
    {
        while (true) // Infinite loop 
        {
            //check if we are not maxed out on power ups
            Debug.Log(currentPowerUps.Count+" / "+maxPowerUps);
            if (currentPowerUps.Count >= maxPowerUps) {
                yield return new WaitForSeconds(1);
                continue;
            }

            //wait for spawnInterval
            yield return new WaitForSeconds(spawnInterval);

            // Instantiate a new object from the prefab

            //get random position
            var spawnPos = getPosition();
            //get random powerup
            var prefabToSpawn = powerUps[GetRandomWeightedIndex(powerUpWeights)];


            Debug.Log("instantiated");
            var spawnedPrefab = Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation, gameObject.transform);
            currentPowerUps.Add(spawnedPrefab);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }
}
