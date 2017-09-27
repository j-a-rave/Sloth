using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLimbControl : MonoBehaviour {

    public Rigidbody[] limbs;
    public float forceMultiplier;
    [Range(0, 1)]
    public float limbLerpValue;

    float[] autoValues;

    void Start()
    {
        GetComponent<GripControl>().SetHands(limbs);
        foreach (Rigidbody limb in limbs)
        {
            limb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        autoValues = new float[4] { Random.Range(0, 2 * Mathf.PI), Random.Range(0, 2 * Mathf.PI), Random.Range(0, 2 * Mathf.PI), Random.Range(0, 2 * Mathf.PI) };
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            autoValues[i] += Random.Range(-10*Time.deltaTime,10*Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            limbs[i].AddForce(forceMultiplier * (Mathf.Cos(autoValues[i]) * Vector3.right + Mathf.Sin(autoValues[i]) * Vector3.up));
        }
    }
}
