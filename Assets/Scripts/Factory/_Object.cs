using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Object
    : MonoBehaviour {
    public enum Color
    {
        Blue,
        Green,
        Red,
        Yellow
    }
    public enum Type
    {
        Triangle,
        Circle,
        HCN,
        Square,
        ChangeColor
    }
    public Type type;
    public Color color;
    public string tag;
    public string layer;
    public virtual Square getSquare(Square sf) { return sf; }
    public virtual Circle getCircle(Circle cf) { return cf; }
    public virtual HCN getHCN(HCN hcn) { return hcn; }
    public virtual TriAngle getTriangle(TriAngle tf) { return tf; }
    public virtual void spin() { }
    public virtual void move(){}
}
