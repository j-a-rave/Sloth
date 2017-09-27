using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public Vector3 speed;
	Vector3 rotEuler;

	// Use this for initialization
	void Start () {
		rotEuler = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		rotEuler -= speed;
		transform.rotation = Quaternion.Euler (rotEuler);
	}
}
