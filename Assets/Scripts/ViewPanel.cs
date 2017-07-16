using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour {

	public GameObject solidLine;
	public GameObject dashedLine;

	[SerializeField]
	ViewType viewType;

	private List<GameObject> lines = new List<GameObject>();

	private IntVector2 size;

	private int lengthPerBlock = Configuration.panelLengthPerBlock;

	// Use this for initialization
	void Start () {
		if (viewType == ViewType.TopView) {
			size.x = Configuration.gridSize.x;
			size.z = Configuration.gridSize.z;
		}

		if (viewType == ViewType.FrontView) {
			size.x = Configuration.gridSize.x;
			size.z = Configuration.maxHeight;
		}

		if (viewType == ViewType.RightView) {
			size.x = Configuration.gridSize.z;
			size.z = Configuration.maxHeight;
		}

		this.GetComponent<RectTransform>().sizeDelta = new Vector2(lengthPerBlock * size.x, lengthPerBlock * size.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Init() {

	}

	public void DrawView(Dictionary<Segment, LineType> lineMap) {
		foreach (KeyValuePair<Segment, LineType> entry in lineMap) {
			if (entry.Value == LineType.NoLine) {
				continue;
			}
			DrawSegment(entry.Key, entry.Value);
		}
	}
	
	//Draw a line on the panel
	private void DrawSegment(Segment segment, LineType lineType) {
	}
}
