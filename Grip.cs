using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour {

    const int LAYER_LEVEL = 8;

    public int id;
    public GripControl gc;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LAYER_LEVEL) //collides with level
        {
            gc.SetGripCollision(id, true, col.transform);
            //sprite
            gc.hsf.SetSizeState(true, id);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == LAYER_LEVEL)
        {
            gc.SetGripCollision(id, false, col.transform);
            //sprite
            gc.hsf.SetSizeState(false, id);
       }
    }
}
