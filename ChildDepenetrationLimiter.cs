using UnityEngine;

public class ChildDepenetrationLimiter : MonoBehaviour {

    void Awake()
    {
        foreach(Rigidbody child in transform.GetComponentsInChildren<Rigidbody>())
        {
            child.maxDepenetrationVelocity = .1f;
        }
    }
}
