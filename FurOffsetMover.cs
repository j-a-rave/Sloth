using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurOffsetMover : MonoBehaviour {

    Renderer rend;
    public Vector2 speed;
    Vector2 offset;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        offset += Time.deltaTime * speed;
        offset.x -= Mathf.Floor(offset.x);
        offset.y -= Mathf.Floor(offset.y);
        rend.material.SetTextureOffset("_MainTex", offset);
	}
}
