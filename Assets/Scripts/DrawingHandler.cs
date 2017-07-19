using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DrawingHandler : MonoBehaviour {

	public GameObject targetTopViewPanel;
	public GameObject currentTopViewPanel;
	public GameObject targetFrontViewPanel;
	public GameObject currentFrontViewPanel;
	public GameObject targetRightViewPanel;
	public GameObject currentRightViewPanel;

	private Dictionary<Segment,LineType> targetTopView;
	private Dictionary<Segment,LineType> targetFrontView;
	private Dictionary<Segment,LineType> targetRightView;

	// Use this for initialization
	void Start () {
		int[,] height = new int[Configuration.gridSize.x, Configuration.gridSize.z];
		height[0,0] = 5;
		Dictionary<IntVector3, bool> targetBlock = To3DMapping(height);
		
		targetTopView = ThreeView.GetTopView(targetBlock);
		targetFrontView = ThreeView.GetFrontView(targetBlock);
		targetRightView = ThreeView.GetRightView(targetBlock);

		targetTopViewPanel.GetComponent<ViewPanel>().DrawView(targetTopView);
		targetFrontViewPanel.GetComponent<ViewPanel>().DrawView(targetFrontView);
		targetRightViewPanel.GetComponent<ViewPanel>().DrawView(targetRightView);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawMultiView(BaseGridCell[,] cells) {
		Dictionary<IntVector3, bool> cubes = To3DMapping(To2DMapping(cells));
		bool flag;

		Dictionary<Segment, LineType> topView = ThreeView.GetTopView(cubes);
		currentTopViewPanel.GetComponent<ViewPanel>().DrawView(topView);
		flag = CompareCurrentAndTargetView(topView, targetTopView);
		targetTopViewPanel.GetComponent<ViewPanel>().ChangeColorOnCompare(flag);

		Dictionary<Segment, LineType> frontView = ThreeView.GetFrontView(cubes);
		currentFrontViewPanel.GetComponent<ViewPanel>().DrawView(frontView);
		flag = CompareCurrentAndTargetView(frontView, targetFrontView);
		targetFrontViewPanel.GetComponent<ViewPanel>().ChangeColorOnCompare(flag);

		Dictionary<Segment, LineType> rightView = ThreeView.GetRightView(cubes);
		currentRightViewPanel.GetComponent<ViewPanel>().DrawView(rightView);
		flag = CompareCurrentAndTargetView(rightView, targetRightView);
		targetRightViewPanel.GetComponent<ViewPanel>().ChangeColorOnCompare(flag);
	}

	private bool CompareCurrentAndTargetView(Dictionary<Segment, LineType> currentView, Dictionary<Segment,LineType> targetView) {
		foreach (KeyValuePair<Segment, LineType> entry in currentView) {
			if (targetView[entry.Key] != entry.Value) {
				return false;
			}
		}
		return true;
	}

	private int[,] To2DMapping(BaseGridCell[,] cells) {
		int[,] mapping = new int[Configuration.gridSize.x, Configuration.gridSize.z];
		for (int x = 0; x < Configuration.gridSize.x; ++x) {
			for (int z = 0; z < Configuration.gridSize.z; ++z) {
				int height = cells[x,z].height;
				mapping[x,z] = height;
			}
		}
		return mapping;
	}

	private Dictionary<IntVector3, bool> To3DMapping(int[,] heightArray) {
		Dictionary<IntVector3, bool> cubes = new Dictionary<IntVector3, bool>();
		for (int x = 0; x < Configuration.gridSize.x; ++x) {
			for (int z = 0; z < Configuration.gridSize.z; ++z) {
				int height = heightArray[x,z];
				for (int h = 0; h < height; ++h) {
					IntVector3 coords = new IntVector3(x,z,h);
					cubes[coords] = true;
				}
			}
		}
		return cubes;
	}
	
}
