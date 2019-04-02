using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterLine : MonoBehaviour
{
    #region Variables

    public LineRenderer waterLine;
    public Transform hoseSpawnPosition;
    public Transform splashParticles;

    private List <GameObject> _activeParticles;
    private Vector3 _targetSplashPosition;

    #endregion


    // Awake is called once before the first frame update
    void Awake ()
    {
        _activeParticles = new List <GameObject> ();
    }


    // This public function is called from WaterSpawner.cs when a new water particle is spawned
    public void AddWaterParticle (GameObject g)
    {
        _activeParticles.Add (g);
    }


    // Called automatically every frame
    void Update ()
    {
        // If there are literally no water particles currently active, there's no need to continue
        if (_activeParticles.Count <= 1)
            return;

        // For every currently active particle...
        // (Notice we're doing this FOR loop backwards!)
        for (int i = _activeParticles.Count - 1; i >= 0; i--)
        {
            // If the particle we're checking now is NOT active, that means it's just been destroyed
            if (!_activeParticles [i].activeSelf)
            {
                // Move our splash particles effect to the position of this water particle
                if (splashParticles != null)
                    splashParticles.position = _activeParticles [i].transform.position;

                // Since this particle is no longer active, remove from the list of active particles
                _activeParticles.RemoveAt (i);
            }
        }

        // How many positions does our line need to contain?
        // We need one for every water particle, plus one to connect to the hose
        waterLine.positionCount = _activeParticles.Count + 1;

        // Set the positions of our line renderer component to match the positions of the active water particles
        for (int i = 0; i < _activeParticles.Count; i++)
        {
            waterLine.SetPosition (i, _activeParticles [i].transform.position);
        }

        // We also need to manually set the last position of our line renderer to be that of the water hose.
        // This ensures a smooth connection to the most recent water particle
        waterLine.SetPosition (waterLine.positionCount - 1, hoseSpawnPosition.position);
    }
}
