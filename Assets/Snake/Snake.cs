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
        // set up
        transform.GetChild(0).gameObject.SetActive(true);
        currentHead = PlayerPrefs.GetInt("currentID") - 1 ;
        int random = Random.Range(0, listTag.Count - 1);
        indexToTransform = random;
        gameObject.transform.GetChild(0).tag = listTag[random];
    }
    // thay doi Material khi an special item or change color
    IEnumerator timeToChange(int index, GameObject _circle)
    {
        
        if (!checkSpecial)
        {
            int div = currentHead * 4 + index % 4;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSnakeHead[div];
            circle.GetComponent<LineRenderer>().material = listMaterial[index % 4];
            tag = listTag[index % 4];
            transform.GetChild(0).tag = tag;
            //smokesEffect[index % 4].SetActive(false);
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
            _circle.GetComponent<LineRenderer>().material = listMaterial[index % 4];
            smokeEffect.transform.position = new Vector3(_circle.GetComponent<LineRenderer>().GetPosition(0).x, _circle.GetComponent<LineRenderer>().GetPosition(0).y, smokeEffect.transform.position.z);
            smokeEffect.transform.eulerAngles = transform.GetChild(0).eulerAngles;
        }
        yield return null;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // setup vi tri cac object pooling
        if (objectPooling != null)
        {
            circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
            if (circle != null)
            {
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.25f);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
                // neu full object pooling => circle = null
                if (objectsPooling.Count < objectPooling.GetComponent<ObjectPooling>().amountToPool)
                {
                    objectsPooling.Add(circle);
                    objectsPooling[objectsPooling.Count - 1].transform.SetAsFirstSibling();
                }
            }
            // neu circle = null
            else
            {
                // exchange new object pooling
                objectsPooling[indexToSetActive].SetActive(false);
                objectsPooling[indexToSetActive].transform.SetAsFirstSibling();
                indexToSetActive++;
                if (indexToSetActive > objectPooling.GetComponent<ObjectPooling>().amountToPool - 1)
                    indexToSetActive = 0;
                circle = objectPooling.GetComponent<ObjectPooling>().getObjectPooling();
                oldHeadPos = transform.GetChild(0).position;
                Vector3 temp = transform.GetChild(0).position;
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.25f);
                Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
            }
            // render
            if (objectsPooling.Count > 0)
            {
                if (circle != null)
                {
                    circle.SetActive(true);
                    circle.GetComponent<LineRenderer>().SetPosition(0, oldHeadPos);
                    circle.GetComponent<LineRenderer>().SetPosition(1, transform.GetChild(0).position);
                    StartCoroutine(timeToChange(indexToTransform, circle));
                }
            }
        }
    }
}