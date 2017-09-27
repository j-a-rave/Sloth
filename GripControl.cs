using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripControl : MonoBehaviour {

    public HandSpriteFollow hsf;

    Rigidbody[] hands;
    bool[] isCollidingWithLevel; //from individual grip class owners (the hands)
    Transform[] collisionTransforms; //also from the hands. for moving platforms.

    SoftJointLimit grabLimit, releaseHighLimit, releaseLowLimit;

	void Start () {
        isCollidingWithLevel = new bool[4] { false, false, false, false };
        collisionTransforms = new Transform[4];

        grabLimit = new SoftJointLimit();
        releaseHighLimit = new SoftJointLimit();
        releaseLowLimit = new SoftJointLimit();
        grabLimit.limit = 0;
        releaseHighLimit.limit = 45;
        releaseLowLimit.limit = -45;
	}
	
	void Update () {
		for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Fire" + (i + 1)))
            {
                //grab
                if (hands[i].constraints == RigidbodyConstraints.FreezePositionZ && isCollidingWithLevel[i])
                {
                    hands[i].transform.parent = collisionTransforms[i]; //child of gripped object, for moving colliders
                    hands[i].constraints = RigidbodyConstraints.FreezePosition;
                    hands[i].GetComponent<CharacterJoint>().highTwistLimit = grabLimit;
                    hands[i].GetComponent<CharacterJoint>().lowTwistLimit = grabLimit;
                    GetComponent<LimbControl>().SetJointLimits(grabLimit, grabLimit, i);
                    //hook-in for sfx
                    SFXController.PlaySound(0);
                    //sprite
                    hsf.SetOpacityState(true, i);
                }

                //release
                else if (hands[i].constraints == RigidbodyConstraints.FreezePosition)
                {
                    hands[i].transform.parent = transform; //child back to this
                    hands[i].constraints = RigidbodyConstraints.FreezePositionZ;
                    hands[i].GetComponent<CharacterJoint>().highTwistLimit = releaseHighLimit;
                    hands[i].GetComponent<CharacterJoint>().lowTwistLimit = releaseLowLimit;
                    GetComponent<LimbControl>().SetJointLimits(releaseHighLimit, releaseLowLimit, i);
                    //sfx
                    SFXController.PlaySound(1);
                    //sprite
                    hsf.SetOpacityState(false, i);
                }
            }
        }

	}

    public void SetHands(Rigidbody[] hands)
    {
        this.hands = hands;
        for (int i = 0; i < 4; i++)
        {
            hands[i].gameObject.AddComponent<Grip>();
            hands[i].gameObject.GetComponent<Grip>().id = i;
            hands[i].gameObject.GetComponent<Grip>().gc = this;
        }
    }

    public void SetGripCollision(int id, bool isColliding, Transform collisionTransform)
    {
        isCollidingWithLevel[id] = isColliding;
        collisionTransforms[id] = collisionTransform;
    }
}
