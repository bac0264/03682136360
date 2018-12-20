using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriAngle: _Object {
    public Vector3 spinStrength = new Vector3(0, 0, 1);
    public bool active = false;
    private void Awake()
    {
        // Color
        tag = color.ToString();
        gameObject.tag = tag;
        // Type

        layer = type.ToString();
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
    private void Update()
    {
        spin();
    }
    public override void spin()
    {
        if (active)
        {
            transform.Rotate(spinStrength);
        }
    }
    public override void move()
    {
    }
}
