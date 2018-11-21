using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : _Object {

    private void Awake()
    {
        tag = type.ToString();
        gameObject.tag = tag;
    }
    public override void spin()
    {
    }
    public override void move()
    {
    }
}
