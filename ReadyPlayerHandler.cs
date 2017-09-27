using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPlayerHandler : MonoBehaviour {

    public Rigidbody pelvis;
    public Text[] playersText;

    public GameObject timer;
	
	void Update () {
        bool ready = false;
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetButtonDown("Fire" + (i + 1)))
            {
                playersText[i].text = "Ready!";
                ready = CheckEm();
            }
        }
        if (ready)
        {
            //activate ragdoll, deactivate images and text, destroy self
            pelvis.isKinematic = false;
            GetComponent<InputDetect>().Deactivate();
            foreach(Text text in playersText)
            {
                text.gameObject.SetActive(false);
            }
            timer.SetActive(true);
            Destroy(this);
        }
	}

    bool CheckEm()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playersText[i].text != "Ready!")
            {
                return false;
            }
        }
        return true;
    }
}
