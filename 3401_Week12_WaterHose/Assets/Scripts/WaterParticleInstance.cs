using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterParticleInstance : MonoBehaviour
{
    #region Variables

    public Rigidbody rb;
    public float initialForce = 10;

    #endregion


    // Called every time this gameObject is enabled
    void OnEnable ()
    {
        rb.AddForce (transform.forward * initialForce, ForceMode.Impulse);
    }


    // Called automatically when this object collides with something
    void OnCollisionEnter (Collision collision)
    {
        gameObject.SetActive (false);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
