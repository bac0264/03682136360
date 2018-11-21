using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
    : _Object{

    private void Awake()
    {
        tag = type.ToString();
        gameObject.tag = tag;
        Vector2 bound = gameObject.GetComponent<SpriteRenderer>().bounds.size +transform.position;
        bound = transform.InverseTransformDirection(bound);
        Debug.Log(bound);
    }
    public override void spin()
    {
    }
    public override void move()
    {
    }
}
