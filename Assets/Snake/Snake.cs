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
    public List<Sprite> listSpecialHead;
    public List<Material> specialList;
    public string tag;
    public GameObject special;
    public GameObject circle;
    public GameObject objectPooling;
    public GameObject specialPooling;
    public List<GameObject> specialsPooling = new List<GameObject>();
    public List<GameObject> objectsPooling = new List<GameObject>();
    int tempOP = 0;
    public int indexToTransform = 0;
    public int indexToSetActive = 0;
    public int indextoSetActiveSpecailOb = 0;
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
    public void Change(int index)
    {
        if (index < 4)
        {
            specialPooling.SetActive(false);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSnakeHead[index];
            circle.GetComponent<LineRenderer>().material = listMaterial[index];
            tag = listTag[index];
            transform.GetChild(0).tag = tag;
        }
        else
        {
            specialPooling.SetActive(true);
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSpecialHead[index % 4];
            circle.GetComponent<LineRenderer>().material = listMaterial[index % 4];
            special = specialPooling.GetComponent<ObjectPooling>().getObjectPooling();
            if (special != null)
            {
                if (specialsPooling.Count < specialPooling.GetComponent<ObjectPooling>().amountToPool)
                    specialsPooling.Add(special);
            }
            else
            {
                specialsPooling[indextoSetActiveSpecailOb].SetActive(false);
                indextoSetActiveSpecailOb++;
                //if (indexToSetActive > ObjectPooling.instance.amountToPool - 1)
                if (indextoSetActiveSpecailOb > specialPooling.GetComponent<ObjectPooling>().amountToPool - 1)
                    indextoSetActiveSpecailOb = 0;
                //circle = ObjectPooling.instance.getObjectPooling();
                special = specialPooling.GetComponent<ObjectPooling>().getObjectPooling();
            }
            if (specialsPooling.Count > 0)
            {
                // objectsPooling.RemoveAt(0);//circle.transform.position = transform.GetChild(0).position;
                if (special != null)
                {
                    special.SetActive(true);
                    special.GetComponent<LineRenderer>().SetPosition(0, oldHeadPos);
                    special.GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position);
                    special.GetComponent<LineRenderer>().SetWidth(0.2f, 0.5f);
                    special.GetComponent<LineRenderer>().material = specialList[0];
                }
            }
        }
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

        //if (ObjectPooling.instance != null)
        if(objectPooling != null)
        {
            // circle = ObjectPooling.instance.getObjectPooling();
            circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
            if (circle != null)
            {
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.3f);
                // temp = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.1f);
                //transform.GetChild(0).position = new Vector3(temp.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
               // if (objectsPooling.Count < ObjectPooling.instance.amountToPool)
               if(objectsPooling.Count < objectPooling.GetComponent<ObjectPooling>().amountToPool)
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
                        Change(indexToTransform);
                    }
                }
            }
            else
            {
                objectsPooling[indexToSetActive].SetActive(false);
                indexToSetActive++;
                //if (indexToSetActive > ObjectPooling.instance.amountToPool - 1)
                if(indexToSetActive > objectPooling.GetComponent<ObjectPooling>().amountToPool - 1)
                    indexToSetActive = 0;
                //circle = ObjectPooling.instance.getObjectPooling();
                circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
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
                        Change(indexToTransform);
                    }
                }
            }

        }
        //}
        //}
    }
    IEnumerator timeToSetActive()
    {
        yield return new WaitForSeconds(10f);
        for (int i = tempOP * 100; i < ((objectsPooling.Count / 2) * (tempOP + 1)); i++)
        {
            objectsPooling[i].SetActive(false);
        }
        tempOP = (tempOP + 1) % 2;
    }
}
