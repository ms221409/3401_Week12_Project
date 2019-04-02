using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    #region Variables

    public GameObject waterParticlePrefab;
    public Transform waterSpawnerRotatorReference;
    public WaterLine waterLine;
    public float particleSpawnRate = 0.2f;
    public int waterParticlePoolCount = 10;

    private GameObject [] _particlePool;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        // Spawn our pool of water particles
        SpawnParticlePool ();

        // Begin our water spawning coroutine; this is a function that will be automaticaly called
        // repeatedly at a set interval
        StartCoroutine ("WaterSpawnTick");
    }


    // Spawns all of the water particle prefabs at the beginning to avoid unnecessary garbage collection
    // during gameplay
    void SpawnParticlePool ()
    {
        _particlePool = new GameObject [waterParticlePoolCount];
        for (int i = 0; i < _particlePool.Length; i++)
        {
            GameObject thisParticle = Instantiate (waterParticlePrefab);
            thisParticle.SetActive (false);
            _particlePool [i] = thisParticle;
        }
    }


    // Spawns new water particles 
    IEnumerator WaterSpawnTick ()
    {
        // This is a WHILE loop; written like this, it will loop forever (or until the coroutine is manually ended)
        while (true)
        {
            // Find the next available (AKA inactive) water particle
            for (int i = 0; i < _particlePool.Length; i++)
            {
                // If this particle is NOT active already...
                if (!_particlePool [i].activeSelf)
                {
                    // Activate this particle!
                    _particlePool [i].transform.position = waterSpawnerRotatorReference.position;
                    _particlePool [i].transform.rotation = waterSpawnerRotatorReference.rotation;
                    _particlePool [i].SetActive (true);

                    // Update the water line (used in scenes 3 & 4)
                    if (waterLine != null)
                        waterLine.AddWaterParticle (_particlePool [i]);

                    // We use BREAK to get out of the FOR loop prematurely
                    break;
                }
            }

            // Wait an amount of seconds equal to particleSpawnRate before calling this coroutine again
            yield return new WaitForSeconds (particleSpawnRate);
        }
    }
}
