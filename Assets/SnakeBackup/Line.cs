using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;
    public GameObject snakeHead;
    GameObject snake;
    List<Vector3> points;
    const int count = 100;
    // Update
    private void Start()
    {
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            points.Add(lineRenderer.GetPosition(i));
        }
        snake = Instantiate(snakeHead);
    }
    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector3>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), mousePos) > 0.1f)
            SetPoint(mousePos);
    }

    // Set vị trí cho line 
    void SetPoint(Vector2 point)
    {
        points.RemoveAt(0);
        points.Add(point);
        // Nếu rắn dài hơn 80 point thì xóa phần tử đầu đi
        //lineRenderer.positionCount = points.Count;
        //Update lại vị trí mới.
        //for (int i = 0; i < lineRenderer.positionCount; i++)
        //{
        //    lineRenderer.SetPosition(i, points[i]);
        //}
        lineRenderer.SetPositions(points.ToArray());
        if (points.Count > 2)
        {
            Vector3 __temp = points[points.Count - 2];
            float rotation = SetRotation(point, __temp);
            snake.transform.rotation = Quaternion.Euler(snake.transform.rotation.x, snake.transform.rotation.y, rotation);
            snake.transform.DOMove(lineRenderer.GetPosition(points.Count - 1), 0f);

        }
    }
    
    // Set rotate

    float SetRotation(Vector2 point, Vector2 __temp)
    {
        float tan = (__temp.x - point.x)/(__temp.y - point.y);
        float rotation = -Mathf.Atan(tan)*180/Mathf.PI;
        return rotation % 90;
    }
}
