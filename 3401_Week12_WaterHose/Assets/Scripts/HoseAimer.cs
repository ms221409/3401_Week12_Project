using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoseAimer : MonoBehaviour
{
    #region Variables

    public float rotationLerpRate = 10;
    public Transform hoseAimTargetTransform;
    public LayerMask aimLayer;

    #endregion


    // FixedUpdate is called once per physics tick
    void FixedUpdate ()
    {
        // We want to raycast through the camera view to the wall ahead of us
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        // If our raycast hits something, we want to aim the hose in that direction
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, aimLayer))
        {
            hoseAimTargetTransform.position = hit.point;
        }
    }


    // Called automatically every frame
    void Update ()
    {
        // We smoothly rotate the hose to match the target rotation every frame, so that movement stays nice and fluid
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (hoseAimTargetTransform.position - transform.position), Time.deltaTime * rotationLerpRate);
    }
}
