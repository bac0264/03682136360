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
    public GameObject circle;
    public GameObject objectPooling;
    public List<GameObject> objectsPooling = new List<GameObject>();
    int tempOP = 0;
    public int indexToTransform = 0;
    public int indexToSetActive = 0;
    public GameObject smokeEffect;
    public GameObject circleEffect;
    bool checkSmoke;
    public bool checkSpecial;
    public int currentHead;
    private void Awake()
    {
        currentHead = PlayerPrefs.GetInt("currentID") - 1 ;
        //Color startColor = ColorOfSnake[Random.Range(0, ColorOfSnake.Length)];
        // transform.GetChild(1).GetComponent<SpriteRenderer>().color = startColor;
        GameObject obj = GameObject.FindGameObjectWithTag("Tail");
        Destroy(obj, 3);
        int random = Random.Range(0, listTag.Count - 1);
        indexToTransform = random;
        Debug.Log("Random:" + random);
        gameObject.transform.GetChild(0).tag = listTag[random];
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = listSnakeTail[random];
    }
    IEnumerator timeToChange(int index, GameObject _circle)
    {
        if (!checkSpecial)
        {
            int div = currentHead * 4 + index % 4;
            Debug.Log("div: " + div);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSnakeHead[div];
            circle.GetComponent<LineRenderer>().material = listMaterial[index % 4];
            tag = listTag[index % 4];
            transform.GetChild(0).tag = tag;
            if (checkSmoke)
            {
                checkSmoke = false;
                smokeEffect.transform.SetParent(null);
                yield return new WaitForSeconds(2f);
                smokeEffect.SetActive(false);
                smokeEffect.transform.SetParent(gameObject.transform.GetChild(0).transform);
            }
            //smokeEffect.SetActive(false);
            //circleEffect.SetActive(false);
        }
        else
        {
            checkSmoke = true;

            smokeEffect.SetActive(true);
            // circleEffect.SetActive(true);
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSpecialHead[index % 4];
            _circle.GetComponent<LineRenderer>().material = listMaterial[index % 4];
            smokeEffect.transform.position = new Vector3(_circle.GetComponent<LineRenderer>().GetPosition(0).x, _circle.GetComponent<LineRenderer>().GetPosition(0).y, smokeEffect.transform.position.z);
            smokeEffect.transform.eulerAngles = transform.GetChild(0).eulerAngles;
            //circleEffect.transform.position = new Vector3(_circle.GetComponent<LineRenderer>().GetPosition(1).x, _circle.GetComponent<LineRenderer>().GetPosition(1).y, circleEffect.transform.position.z);
            //circleEffect.transform.eulerAngles = transform.GetChild(0).eulerAngles;
        }
        yield return null;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (objectPooling != null)
        {
            // circle = ObjectPooling.instance.getObjectPooling();
            circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
            if (circle != null)
            {
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.22f);
                // temp = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.1f);
                //transform.GetChild(0).position = new Vector3(temp.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
                // if (objectsPooling.Count < ObjectPooling.instance.amountToPool)
                if (objectsPooling.Count < objectPooling.GetComponent<ObjectPooling>().amountToPool)
                    objectsPooling.Add(circle);
            }
            else
            {
                objectsPooling[indexToSetActive].SetActive(false);
                indexToSetActive++;
                //if (indexToSetActive > ObjectPooling.instance.amountToPool - 1)
                if (indexToSetActive > objectPooling.GetComponent<ObjectPooling>().amountToPool - 1)
                    indexToSetActive = 0;
                //circle = ObjectPooling.instance.getObjectPooling();
                circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.45f);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
            }
            if (objectsPooling.Count > 0)
            {
                // objectsPooling.RemoveAt(0);//circle.transform.position = transform.GetChild(0).position;
                if (circle != null)
                {
                    //circle.GetComponent<LineRenderer>().material = listMaterial[1];
                    circle.SetActive(true);
                    circle.GetComponent<LineRenderer>().SetPosition(0, oldHeadPos);
                    circle.GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position);
                    StartCoroutine(timeToChange(indexToTransform, circle));
                   // Change(indexToTransform, circle);
                }
            }
        }
    }
}