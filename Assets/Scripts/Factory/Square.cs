using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : _Object {

    private void Awake()
    {
        // Color
        tag = color.ToString();
        gameObject.tag = tag;
        // Type

        layer = type.ToString();
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
    public override void spin()
    {
    }
    public override void move()
    {
    }
}
