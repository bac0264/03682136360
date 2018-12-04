using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {
    float  widthScreen = 6;
    // Use this for initialization
    void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;
        float ortho = widthScreen / Screen.width * Screen.height / 2;
        float WorldHeight = ortho * 2;
        float WorldWidth = WorldHeight * Screen.width / Screen.height;
        tempScale.x = WorldWidth / width;
        tempScale.y = WorldHeight/height;
        transform.localScale = tempScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
