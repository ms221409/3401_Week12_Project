using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinker : MonoBehaviour
{
    #region Variables

    public GameObject lightObj;
    public Vector2 blinkRateRange;

    #endregion


    // Start is called before the first frame update
    void Start ()
    {
        // Call BlinkLight for the first time
        // After it's called, it will continue to call itself forever
        BlinkLight ();
    }


    // Turns the light on if it's off and off if it's on
    void BlinkLight ()
    {
        // Set the light to be off or on
        lightObj.SetActive (!lightObj.activeSelf);

        // Get a new random time to wait
        float waitTime = Random.Range (blinkRateRange.x, blinkRateRange.y);
        if (Random.value < 0.1f)
            waitTime = 2;

        // Call this function again
        Invoke ("BlinkLight", waitTime);
    }
}
