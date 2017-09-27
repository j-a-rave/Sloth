using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpriteFollow : MonoBehaviour {

    public Transform[] handPositions;
    static Color unstuck = new Color(1, 1, 1, .5f);

	void Start () {
		foreach(Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = unstuck;
            child.localScale = Vector3.one * .5f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 4; i++) //absolutely garbage, please no
        {
            transform.GetChild(i).transform.position = (handPositions[i].position.x * Vector3.right + handPositions[i].position.y * Vector3.up + -.6f*Vector3.forward);
        }
	}

    public void SetOpacityState(bool isStuck, int id)
    {
        if (isStuck)
        {
            transform.GetChild(id).GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            transform.GetChild(id).GetComponent<SpriteRenderer>().color = unstuck;
        }
    }

    public void SetSizeState(bool isColliding, int id)
    {
        if (isColliding)
        {
            transform.GetChild(id).localScale = Vector3.one;
        }
        else
        {
            transform.GetChild(id).localScale = Vector3.one * .5f;
        }
    }
}
