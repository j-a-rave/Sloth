using UnityEngine;

public class GoalFollow : MonoBehaviour {

    const int MAX_ZOOM = -40;

    public GameObject goal;
    [Range(0, 1)]
    public float slerpValue;
    Camera cam;

    Vector3 slerpTarget;

    void Start()
    {
        cam = GetComponent<Camera>();

        slerpTarget = new Vector3(goal.transform.position.x, goal.transform.position.y, MAX_ZOOM);
    }

    void Update()
    {
        cam.transform.position = Vector3.Slerp(cam.transform.position, slerpTarget, slerpValue);
    }
}
