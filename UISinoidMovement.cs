using UnityEngine;

public class UISinoidMovement : MonoBehaviour {

    public Vector2 movementScale;
    public float speed;

    RectTransform rt;
    Vector2 pos;

	void Start () {
        rt = GetComponent<RectTransform>();
        pos = rt.localPosition;
	}
	
	void Update () {
        rt.localPosition = pos + (Mathf.Sin(Time.timeSinceLevelLoad*speed) * movementScale.x * Vector2.right + Mathf.Cos(Time.timeSinceLevelLoad*speed) * movementScale.y * Vector2.up);
	}
}
