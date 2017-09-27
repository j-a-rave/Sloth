using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSpawnTrigger : MonoBehaviour {

    static bool spawned = false;

    void Update()
    {
        spawned = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!spawned)
        {
            Destroy(other.transform.parent.parent.parent.gameObject);
            Instantiate((GameObject)Resources.Load("AutoSloth"));
            spawned = true;
        }
    }
}
