using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		IntVector2 p1 = new IntVector2(0,0);
		IntVector2 p2 = new IntVector2(0,1);
		Segment testSegment = new Segment(p1, p2);
		DrawLine(testSegment, LineType.SolidLine);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawView(Dictionary<Segment, LineType> lineMap) {
		foreach (KeyValuePair<Segment, LineType> entry in lineMap) {
			if (entry.Value == LineType.NoLine) {
				continue;
			}
			DrawLine(entry.Key, entry.Value);
		}
	}
	
	//Draw a line on the panel
	private void DrawLine(Segment segment, LineType lineType) {
	}
}
