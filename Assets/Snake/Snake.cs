using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // blue, green, red, yellow
    public static Color[] ColorOfSnake = new Color[] { new Color(83f / 255f, 201f / 255f, 219f / 255f), new Color(83f / 255f, 219f / 255f, 166f / 255f), new Color(232f / 255f, 72f / 255f, 124f / 255f), new Color(232f / 255f, 192f / 255f, 85f / 255f) };
    public Transform Target;
    public GameObject bodyPrefab;
    public ContactFilter2D ContactFilter;
    Vector3 oldHeadPos;
    List<Vector3> bodyPos = new List<Vector3>();
    Collider2D[] touchHead = new Collider2D[100];
    GameObject body;
    public List<Sprite> listSnakeHead; // color of snake head
    public List<Material> listMaterial; // color of snake
    public List<string> listTag = new List<string>();
    public List<Sprite> listSnakeTail; // color of tail color
    public string tag;
    public GameObject circle;
    public List<GameObject> objectsPooling = new List<GameObject>();
    int tempOP = 0;
    public int indexToTransform = 0;
    public int indexToSetActive = 0;
    private void Awake()
    {
        //Color startColor = ColorOfSnake[Random.Range(0, ColorOfSnake.Length)];
        // transform.GetChild(1).GetComponent<SpriteRenderer>().color = startColor;
        GameObject obj = GameObject.FindGameObjectWithTag("Tail");
        Destroy(obj, 3);
        int random = Random.Range(0, listSnakeHead.Count);
        indexToTransform = random;
        gameObject.transform.GetChild(0).tag = listTag[random];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = listSnakeTail[random];
    }
    public void Change(int index,GameObject _circle)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSnakeHead[index];
        //Destroy(body, 3f); //xoa body cu
                           //body = Instantiate(bodyPrefab, transform);
                           //tao body moi

        // body.GetComponent<LineRenderer>().material = listMaterial[index];
        circle.GetComponent<LineRenderer>().material = listMaterial[index];
        tag = listTag[index];
        transform.GetChild(0).tag = tag;
       // bodyPos.Clear();
        //for (int i = 0; i < body.transform.GetComponent<LineRenderer>().positionCount; i++)
        //{
        //    bodyPos.Add(transform.GetChild(0).position);
        //}
        //body.GetComponent<LineRenderer>().SetPositions(bodyPos.ToArray());

    }
    // Update is called once per frame
    void LateUpdate()
    {
        //if (MainGameManager.gameStatus == GameStatus.PLAYING)
        //{
        //if (transform.childCount > 2)
        //{
        //    GameObject obj = GameObject.FindGameObjectWithTag("Tail");
        //    Destroy(obj, 3);
        //}
        //if (bodyPos.Count > 0)
        //{
        //    bodyPos.RemoveAt(0);
        // bodyPos.Add(transform.GetChild(0).position);
        // body.GetComponent<LineRenderer>().SetPositions(bodyPos.ToArray());
        if (ObjectPooling.instance != null)
        {
            circle = ObjectPooling.instance.getObjectPooling();
            if (circle != null)
            {
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.3f);
                // temp = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.1f);
                //transform.GetChild(0).position = new Vector3(temp.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
                if (objectsPooling.Count < ObjectPooling.instance.amountToPool)
                    objectsPooling.Add(circle);
                if (objectsPooling.Count > 0)
                {
                    // objectsPooling.RemoveAt(0);//circle.transform.position = transform.GetChild(0).position;
                    if (circle != null)
                    {
                        //circle.GetComponent<LineRenderer>().material = listMaterial[1];
                        circle.SetActive(true);
                        circle.GetComponent<LineRenderer>().SetPosition(0, oldHeadPos);
                        circle.GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position);
                        Change(indexToTransform, circle);
                    }
                }
            }
            else
            {
                objectsPooling[indexToSetActive].SetActive(false);
                indexToSetActive++;
                if (indexToSetActive > ObjectPooling.instance.amountToPool - 1) indexToSetActive = 0;
                circle = ObjectPooling.instance.getObjectPooling();
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.45f);              
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
                if (objectsPooling.Count > 0)
                {
                    // objectsPooling.RemoveAt(0);//circle.transform.position = transform.GetChild(0).position;
                    if (circle != null)
                    {
                        //circle.GetComponent<LineRenderer>().material = listMaterial[1];
                        circle.SetActive(true);
                        circle.GetComponent<LineRenderer>().SetPosition(0, oldHeadPos);
                        circle.GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position);
                        Change(indexToTransform, circle);
                    }
                }
            }
        }
        //}
        //}
    }
    IEnumerator tiemToSetActive()
    {
        yield return new WaitForSeconds(10f);
        for (int i = tempOP * 100; i < ((objectsPooling.Count / 2) * (tempOP + 1)); i++)
        {
            objectsPooling[i].SetActive(false);
        }
        tempOP = (tempOP + 1) % 2;
    }
}
