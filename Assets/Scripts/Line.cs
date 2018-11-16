using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;
    public GameObject snakeHead;
    GameObject snake;
    List<Vector2> points;
    // Update
    private void Start()
    {
        snake = Instantiate(snakeHead);
    }
    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount-1), mousePos) > 0.1f)
            SetPoint(mousePos);
    }

    // Set vị trí cho line 
    void SetPoint(Vector2 point)
    {
        Vector2 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        Vector2 direct = (endPoint - point).normalized;
        points.Add(direct);
       /* var startPos : Vector3 = drawArray[0];
        var endPos : Vector3 = drawArray[drawArray.Count - 1];
        direction = (endPos - startPos).normalized;
        player.transform.position.x += direction.x * 0.4;*/
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
        snake.transform.position = lineRenderer.GetPosition(points.Count - 1);
        if (points.Count > 1)
            edgeCol.points = points.ToArray();
    }
    
}
