using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class scripts a few simple behaviors: point camera to the goal
//fill a UI sprite so it circles the goal
//then, continue after a short time, activating the slerpfollow behavior
public class ExplanationCamera : MonoBehaviour {

    public GameObject target;
    [Range(0, 1)]
    public float slerpValue;
    [Range(0, 5)]
    public float timer;
    public GameObject controllerText;

    Vector3 slerpTarget;
    IEnumerator coroutine;

	void Start () {
        slerpTarget = new Vector3(target.transform.position.x, target.transform.position.y, -60);
        coroutine = TransitionAfterWait(timer);
        StartCoroutine(coroutine);
	}
	
	void Update () {
        transform.position = Vector3.Slerp(transform.position, slerpTarget, slerpValue);
	}

    IEnumerator TransitionAfterWait(float timer)
    {
        yield return new WaitForSeconds(timer);
        controllerText.SetActive(true);
        GetComponent<SlerpFollow>().enabled = true;
        Destroy(this);
    }
}
