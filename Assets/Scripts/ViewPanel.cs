using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour {

	public GameObject solidLine;
	public GameObject dashedLine;

	[SerializeField]
	private float panelDisplayScale = 0.8f; //E.g., if this were set to 1, then a full display would fill up the whole panel

	[SerializeField]
	private ViewType viewType;

	private List<GameObject> lines = new List<GameObject>();
	
	private IntVector2 blockSize; //E.g., the max display of this panel is 3 by 5
	private Vector2 panelSize; //E.g., the length is 300 by 500, equals blockSize * lengthPerBlock
	private int lengthPerBlock = Configuration.panelLengthPerBlock;


	// Use this for initialization
	void Start () {
		if (viewType == ViewType.TopView) {
			blockSize.x = Configuration.gridSize.x;
			blockSize.z = Configuration.gridSize.z;
		}

		if (viewType == ViewType.FrontView) {
			blockSize.x = Configuration.gridSize.x;
			blockSize.z = Configuration.maxHeight;
		}

		if (viewType == ViewType.RightView) {
			blockSize.x = Configuration.gridSize.z;
			blockSize.z = Configuration.maxHeight;
		}

		panelSize.x = blockSize.x * lengthPerBlock;
		panelSize.y = blockSize.z * lengthPerBlock;

		this.GetComponent<RectTransform>().sizeDelta = new Vector2(panelSize.x, panelSize.y);

		IntVector2 start = new IntVector2(0,0);
		IntVector2 end = new IntVector2(3,0);
		DrawSegment(new Segment(start,end), LineType.SolidLine);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawView(Dictionary<Segment, LineType> lineMap) {
		foreach (GameObject line in lines) {
			Destroy(line);
		}
		foreach (KeyValuePair<Segment, LineType> entry in lineMap) {
			if (entry.Value == LineType.NoLine) {
				continue;
			}
			DrawSegment(entry.Key, entry.Value);
		}
	}
	
	//Draw a line on the panel
	private void DrawSegment(Segment segment, LineType lineType) {

		IntVector2 pointA = segment.p1;
		IntVector2 pointB = segment.p2;
		Vector2 startPosition = new Vector2(pointA.x * lengthPerBlock, pointA.z * lengthPerBlock);
		Vector2 endPosition = new Vector2(pointB.x * lengthPerBlock, pointB.z * lengthPerBlock);
		startPosition -= panelSize/2;
		endPosition -= panelSize/2;
		startPosition *= panelDisplayScale;
		endPosition *= panelDisplayScale;

		GameObject lineGameObject;
		if (lineType == LineType.SolidLine) {
			lineGameObject = Instantiate(solidLine) as GameObject;
		}
		else if (lineType == LineType.DashedLine) {
			lineGameObject = Instantiate(solidLine) as GameObject;
		}
		else {
			return;
		}
		lineGameObject.transform.SetParent(this.transform, false);
		LineRenderer lineRenderer = lineGameObject.GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, startPosition);
		lineRenderer.SetPosition(1, endPosition);
		lines.Add(lineGameObject);

	}

}
