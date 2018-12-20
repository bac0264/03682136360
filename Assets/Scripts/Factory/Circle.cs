using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Circle
    : _Object{
    public bool active;
    public float duration;
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
        move();
    }
    public override void spin()
    {
    }
    public override void move()
    {
        StartCoroutine(timeToMove());
    }
    IEnumerator timeToMove()
    {
        if (active)
        {
            active = false;
            Tween m = transform.DOMoveX(-transform.transform.position.x, duration);
            yield return m.WaitForCompletion();
            Tween m_2 = transform.DOMoveX(transform.transform.position.x, duration);
            yield return m_2.WaitForCompletion();
            active = true;
        }
    }
}
