using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    //public static Color[] ColorOfSnake = new Color[] { new Color(83f/255f,201f/255f,219f/255f), new Color(232f / 255f, 72f / 255f, 124f / 255f), new Color(83f / 255f, 219f / 255f, 166f / 255f), new Color(232f / 255f, 192f / 255f, 85f / 255f) };
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
    private void Start()
    {
        //Color startColor = ColorOfSnake[Random.Range(0, ColorOfSnake.Length)];
        // transform.GetChild(1).GetComponent<SpriteRenderer>().color = startColor;
        Destroy(transform.GetChild(1).gameObject, 3);
        int random = Random.Range(0, listSnakeHead.Count);
        Change(random);
    }
    public void Change(int index)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSnakeHead[index];
        Destroy(body, 3f); //xoa body cu
        body = Instantiate(bodyPrefab,transform); //tao body moi
        body.GetComponent<LineRenderer>().material = listMaterial[index];
        tag = listTag[index];
        transform.GetChild(0).tag = tag;
        bodyPos.Clear();
        for (int i = 0; i < body.transform.GetComponent<LineRenderer>().positionCount; i++)
        {
            bodyPos.Add(transform.GetChild(0).position);
        }
        body.GetComponent<LineRenderer>().SetPositions(bodyPos.ToArray());

    }
    // Update is called once per frame
    void FixedUpdate () {
        if (MainGameManager.gameStatus == GameStatus.PLAYING)
        {
            oldHeadPos = transform.GetChild(0).position;
            transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Target.position, 0.15f);
            Vector2 headDirection = (transform.GetChild(0).position - oldHeadPos);
            transform.GetChild(0).eulerAngles = new Vector3(0, 0, -Mathf.Atan(headDirection.x / headDirection.y) * 180 / Mathf.PI);
            if (bodyPos.Count > 0)
            {
                bodyPos.RemoveAt(0);
                bodyPos.Add(transform.GetChild(0).position);
                body.GetComponent<LineRenderer>().SetPositions(bodyPos.ToArray());
            }
        }
    }
}
