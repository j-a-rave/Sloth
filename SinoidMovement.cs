using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SinoidMovement : MonoBehaviour {

    public Vector2 movementScale;
    public float speed;

    [HideInInspector]
    public Vector3 pos;

	void Start () {
        pos = transform.position;
	}
	
	void Update () {
        transform.position = pos + (Mathf.Sin(Time.timeSinceLevelLoad * speed) * movementScale.x * Vector3.right + Mathf.Cos(Time.timeSinceLevelLoad * speed) * movementScale.y * Vector3.up);
	}
}


#if UNITY_EDITOR
[CustomEditor(typeof(SinoidMovement))]
public class SinoidMovementEditor : Editor
{
    SinoidMovement sm;

    public void OnSceneGUI()
    {
        sm = target as SinoidMovement;
        Handles.color = Color.cyan;
        Handles.DrawLine(sm.pos - sm.movementScale.x * Vector3.right, sm.pos + sm.movementScale.x * Vector3.right);
        Handles.DrawLine(sm.pos - sm.movementScale.y * Vector3.up, sm.pos + sm.movementScale.y * Vector3.up);
    }
}


#endif