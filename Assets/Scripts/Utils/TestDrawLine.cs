using UnityEngine;
using System.Collections;

public class TestDrawLine : MonoBehaviour {
	
	void OnGUI () {
		Vector2 pointA = new Vector2(Screen.width/2, Screen.height/2);
		Vector2 pointB = Event.current.mousePosition;
		DrawingUtil.DrawLine(pointA, pointB);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
