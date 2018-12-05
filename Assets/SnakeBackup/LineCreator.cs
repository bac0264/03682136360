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
    [Range(0, 1)]
    public float speed = 0.5f;
    Vector3 temp; // vị trí update
    Vector3 _temp; // camera update 
    Vector3 start = Vector3.zero;
    Vector3 end;
    Vector3 test = new Vector3(1,1,1);
    Vector3 Target;
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
        temp = Camera.main.WorldToScreenPoint(new Vector3(0, Camera.main.transform.position.y + 150f, Camera.main.transform.position.z)); // Vị trí của rắn                                                                            
    }
    void FixedUpdate()
    {
       // Time.timeScale = speed;
        processing();
    }
    void processing()
    {
        _temp = Camera.main.transform.position;
        _temp.y += Time.fixedDeltaTime * 2;
        Camera.main.WorldToScreenPoint(_temp);
        Camera.main.transform.position = _temp;
        //Vector3 oldHeadPos = transform.GetChild(0).position;
        transform.position = new Vector3(0, transform.position.y + Time.fixedDeltaTime * 2, -10);
        //  if (activeLine.lineRenderer.positionCount >= 2)

        // Khi không touching sẽ Update theo vị trí cuối cùng
        if (!touching)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touching = true;
                start = Input.mousePosition ;

            }
        }
        else
        {
            //end = Input.mousePosition;
            temp = Vector3.Lerp(temp, Input.mousePosition, 0.1f);
            //if (end != start)
            //{
            //    // temp = Vector3.Lerp(temp, Input.mousePosition, 0.1f);
            //    float distance = end.x - start.x;
            //    Vector3 vectorDistance = end - start;
            //    Debug.Log("distance: " + distance);

            //    temp.x += distance;
            //    //temp = Vector3.Lerp(temp, end, 0.5f/distance);
            //    Debug.Log("Temp: " + temp);
            //    start = end;
            //}
            if (Input.GetMouseButtonUp(0))
            {
               // end = start;
                touching = false;
                // Debug.Log("index: " + (activeLine.lineRenderer.GetPosition(activeLine.lineRenderer.positionCount - 1)));
                touchEnd(activeLine.lineRenderer.GetPosition(activeLine.lineRenderer.positionCount - 1));
            }
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touching = true;
                touchBegin(Input.GetTouch(0).position);
                start = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                end = Input.GetTouch(0).position;
                if (end != start)
                {
                    float distance = end.x - start.x;
                    Vector3 vectorDistance = end - start;
                  //  Debug.Log("distance: " + distance);
                    temp = Vector3.Lerp(temp, Input.mousePosition, 0.1f);
                    temp.x += distance;
                    //temp = Vector3.Lerp(temp, end, 0.5f/distance);
                    Debug.Log("Temp: " + temp);
                    start = end;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touching = false;
                touchEnd(activeLine.lineRenderer.GetPosition(activeLine.lineRenderer.positionCount - 1));
            }
        }
        touchHold(temp);
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
            screenPos.y = _temp.y;
            screenPos.x = Camera.main.ScreenToWorldPoint(screenPos).x;
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPos);
            activeLine.UpdateLine(screenPos);
        }
    }
}
