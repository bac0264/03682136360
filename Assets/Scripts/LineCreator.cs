using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{

    public GameObject linePrefab;
    GameObject lineGO;
    Line activeLine;
    public GameObject lineContainer;
    bool touching;
    Vector3 temp;
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineGO = Instantiate(linePrefab);
            activeLine = lineGO.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

    }*/
    private void Start()
    {
        lineGO = Instantiate(linePrefab, lineContainer.transform); // khởi tạo line
        temp = Camera.main.WorldToScreenPoint(new Vector3(0,Camera.main.transform.position.y + 150f, Camera.main.transform.position.z)); // Vị trí của rắn                                                                            
    }
    void FixedUpdate()
    {
        //  if (activeLine.lineRenderer.positionCount >= 2)

        // Khi không touching sẽ Update theo vị trí cuối cùng
        if (!touching)
        {
            //updateLine(temp);
            if (Input.GetMouseButtonDown(0) && !touching)
            {
                touching = true;
            }
        }
        else
        {
            temp = Vector3.Lerp(temp, Input.mousePosition, 0.1f);
            if (Input.GetMouseButtonUp(0))
            {
                touching = false;
                Debug.Log("index: " + (activeLine.lineRenderer.GetPosition(activeLine.lineRenderer.positionCount - 1)));
                touchEnd(activeLine.lineRenderer.GetPosition(activeLine.lineRenderer.positionCount - 1));
            }
        }
        touchHold(temp);
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touching = true;
                touchBegin(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchHold(Input.GetTouch(0).position);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touching = false;
                touchEnd(Input.GetTouch(0).position);
            }
        }
    }
    void touchBegin(Vector2 screenPos)
    {

    }
    void touchHold(Vector2 screenPos)
    {
        updateLine(screenPos);
    }
    void touchEnd(Vector2 screenPos)
    {
        // setup vị trí cuối cùng
        temp = Camera.main.WorldToScreenPoint(screenPos);

    }
    void updateLine(Vector2 screenPos)
    {
        // update Line khi giữ chuột
        activeLine = lineGO.GetComponent<Line>();

        if (activeLine != null)
        {
            // vị trí mới
            screenPos.y = Camera.main.transform.position.y;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPos);
            activeLine.UpdateLine(mousePos);
        }
    }
}
