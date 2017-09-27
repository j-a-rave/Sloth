using UnityEngine;

public class SlerpFollow: MonoBehaviour {

    const int MAX_ZOOM = -40;

    public GameObject sloth;
    [Range(0,1)]
    public float slerpValue;
    LimbControl lc;
    Camera cam;
    Vector2 averagePosition;
    float maxDistance;

    Vector3 slerpTarget;

	void Start () {
        lc = sloth.GetComponent<LimbControl>();
        cam = GetComponent<Camera>();
        //cam.orthographicSize = MAX_ZOOM;

        averagePosition = GetAveragePosition();
        maxDistance = GetMaxDistance();

        slerpTarget = new Vector3(averagePosition.x, averagePosition.y, MAX_ZOOM);
	}
	
	void Update () {
        averagePosition = GetAveragePosition();
        maxDistance = GetMaxDistance();
        slerpTarget.x = averagePosition.x;
        slerpTarget.y = averagePosition.y;
        slerpTarget.z = Mathf.Min(MAX_ZOOM, maxDistance*-2);
        cam.transform.position = Vector3.Slerp(cam.transform.position, slerpTarget, slerpValue);
	}

    Vector2 GetAveragePosition()
    {
        Vector2 positionSum = Vector2.zero;
        foreach(Rigidbody rb in lc.limbs)
        {
            positionSum += rb.position.x * Vector2.right + rb.position.y * Vector2.up;
        }
        return positionSum / lc.limbs.Length;
    }

    float GetMaxDistance()
    {
        float maxDistance = 0;
        foreach(Rigidbody rb in lc.limbs)
        {
            float rbMag = (rb.position.x * Vector2.right + rb.position.y * Vector2.up).magnitude;
            if (maxDistance < rbMag)
            {
                maxDistance = rbMag;
            }
        }
        return maxDistance;
    }
}
