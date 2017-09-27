using UnityEngine;
using System.Collections.Generic;

public class LimbControl : MonoBehaviour {

    public Rigidbody[] limbs;
    public float forceMultiplier;
    public float forceSubtracter;
    [Range(0, 1)]
    public float limbLerpValue;

    public List<CharacterJoint> leftLeg, leftArm, rightLeg, rightArm;

    float activeLimbMagnitude;

    Vector2[] inputAxes;
    Vector2[] inputLerp;

	void Start () {
        GetComponent<GripControl>().SetHands(limbs);
        foreach (Rigidbody limb in limbs)
        {
            limb.constraints = RigidbodyConstraints.FreezePositionZ; //only XY movement for input
        }

        inputAxes = new Vector2[4] { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
        inputLerp = new Vector2[4] { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
        activeLimbMagnitude = 0;
    }
	
	void Update () {
        activeLimbMagnitude = 0;
		for (int i = 0; i < 4; i++)
        {
            //directional input and lerping
            inputAxes[i].x = Input.GetAxis("Horizontal" + (i + 1));
            inputAxes[i].y = Input.GetAxis("Vertical" + (i + 1));

            inputLerp[i] = Vector2.Lerp(inputLerp[i], inputAxes[i], limbLerpValue);

            activeLimbMagnitude += inputLerp[i].magnitude;
        }
	}

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            limbs[i].AddForce( (forceMultiplier - (forceSubtracter * activeLimbMagnitude)) * (inputLerp[i].x * Vector3.right + inputLerp[i].y * Vector3.up));
        }
    }

    public void SetJointLimits(SoftJointLimit high, SoftJointLimit low, int id)
    {
        switch (id)
        {
            case 0: //left leg
                foreach (CharacterJoint joint in leftLeg)
                {
                    joint.highTwistLimit = high;
                    joint.lowTwistLimit = low;
                }
                    break;
            case 1: //left arm
                foreach(CharacterJoint joint in leftArm)
                {
                    joint.highTwistLimit = high;
                    joint.lowTwistLimit = low;
                }
                break;
            case 2: //right arm
                foreach(CharacterJoint joint in rightArm)
                {
                    joint.highTwistLimit = high;
                    joint.lowTwistLimit = low;
                }
                break;
            case 3:
                foreach(CharacterJoint joint in rightLeg)
                {
                    joint.highTwistLimit = high;
                    joint.lowTwistLimit = low;
                }
                break;
        }
    }
}
