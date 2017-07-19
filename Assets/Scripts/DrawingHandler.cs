using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawingHandler : MonoBehaviour {

	public GameObject targetTopViewPanel;
	public GameObject currentTopViewPanel;
	public GameObject targetFrontViewPanel;
	public GameObject currentFrontViewPanel;
	public GameObject targetRightViewPanel;
	public GameObject currentRightViewPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawMultiView(BaseGridCell[,] cells) {
		Dictionary<IntVector3, bool> cubes = new Dictionary<IntVector3, bool>();
		for (int x = 0; x < Configuration.gridSize.x; ++x) {
			for (int z = 0; z < Configuration.gridSize.z; ++z) {
				int height = cells[x,z].height;
				for (int h = 0; h < height; ++h) {
					IntVector3 coords = new IntVector3(x,z,h);
					cubes[coords] = true;
				}
			}
		}

		Dictionary<Segment, LineType> topView = ThreeView.GetTopView(cubes);
		currentTopViewPanel.GetComponent<ViewPanel>().DrawView(topView);

		Dictionary<Segment, LineType> frontView = ThreeView.GetFrontView(cubes);
		currentFrontViewPanel.GetComponent<ViewPanel>().DrawView(frontView);

		Dictionary<Segment, LineType> rightView = ThreeView.GetRightView(cubes);
		currentRightViewPanel.GetComponent<ViewPanel>().DrawView(rightView);
	}
	
}
