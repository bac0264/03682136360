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
    List<Vector2> points;
    const int count = 100;
    // Update
    private void Start()
    {
        snake = Instantiate(snakeHead);
    }
    public void UpdateLine(Vector2 mousePos, Vector2 __temp)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos, __temp);
            return;
        }

        if (Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), mousePos) > 0.1f)
            SetPoint(mousePos, __temp);
    }

    // Set vị trí cho line 
    void SetPoint(Vector2 point, Vector2 __temp)
    {
        points.Add(point);
        // Nếu rắn dài hơn 80 point thì xóa phần tử đầu đi
        if (points.Count > 80)
        {
            points.RemoveAt(0);
        }
        lineRenderer.positionCount = points.Count;
        //Update lại vị trí mới.
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
        float rotation = SetRotation(point, __temp);
        Debug.Log("rota: "+ rotation);
      //  snake.transform.DORotate.(new Vector3(0, 0, rotation),0.1f,RotateMode.Fast);//new Vector3(snake.transform.rotation.x, snake.transform.rotation.y, rotation);
        snake.transform.DOMove(lineRenderer.GetPosition(points.Count - 1), 0f);
    }
    
    // Set rotate

    float SetRotation(Vector2 point, Vector2 __temp)
    {
        Debug.Log("temp before: " + __temp);
        __temp = Camera.main.ScreenToWorldPoint(__temp);
        Debug.Log("temp after: " + __temp);
        Debug.Log("point: " + point);
        float tan = (__temp.y - point.y) / (__temp.x - point.x);
        float rotation = Mathf.Atan(tan);

        return rotation/Mathf.PI*-180;
    }
}
