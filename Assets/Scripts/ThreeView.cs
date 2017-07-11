using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThreeView {

	public static void GetFrontView(BaseGridCell[,] cells) {

	}

	public static void GetRightView(BaseGridCell[,] cells) {

	}

	public static Dictionary<Segment, LineType> GetTopView(BaseGridCell[,] cells) {
		Dictionary<Segment, LineType> topView = new Dictionary<Segment, LineType>();
		int sizeX = cells.GetLength(0);
		int sizeZ = cells.GetLength(1);
		for (int x = 0; x < sizeX; ++x) {
			for (int z = 0; z < sizeZ; ++z) {
				IntVector2 p1 = new IntVector2(x,z);

				if (x < sizeX - 1) {
					IntVector2 p2 = new IntVector2(x+1, z);
					Segment segment = new Segment(p1, p2);
					LineType lineType = GetLineType(segment, cells, ViewType.TopView);
					topView.Add(segment, lineType);
				}

				if (z < sizeZ - 1) {
					IntVector2 p2 = new IntVector2(x, z+1);
					Segment segment = new Segment(p1, p2);
					LineType lineType = GetLineType(segment, cells, ViewType.TopView);
					topView.Add(segment, lineType);
				}
			}
		}
		return topView;
	}//End of GetTopView

	private static LineType GetLineType(Segment segment, BaseGridCell[,] cells, ViewType viewType) {
		switch (viewType) {
			case ViewType.TopView: {
				//Currently only supports cubes, and there cant be any "hollows"

				//Two cells are adjacent to this segment
				//If the height is the same, there should be NoLine
				//Else there should be a SolidLine

				BaseGridCell cell1 = cells[segment.p1.x, segment.p1.z];
				if (segment.p2.x == segment.p1.x + 1) {
					//The segment goes in the x direction
					
					//Need to check if out of bounds
					if (segment.p1.z == 0) {
						if (cell1.height > 0) {
							return LineType.SolidLine;
						}
						else {
							return LineType.NoLine;
						}
					}

					//The other cell should be "under" it (i.e., z - 1)
					else {
						BaseGridCell cell2 = cells[segment.p1.x, segment.p1.z-1];
						if (cell1.height != cell2.height) {
							return LineType.SolidLine;
						}
						else {
							return LineType.NoLine;
						}
					}
				}

				if (segment.p2.z == segment.p1.z + 1) {
					//The segment goes in the z direction

					//Need to check if out of bounds
					if (segment.p1.x == 0) {
						if (cell1.height > 0) {
							return LineType.SolidLine;
						}
						else {
							return LineType.NoLine;
						}
					}

					//The other cell should be "to the left" of it (i.e.,  x - 1)
					else {
						BaseGridCell cell2 = cells[segment.p1.x-1, segment.p1.z];
						if (cell1.height != cell2.height) {
							return LineType.SolidLine;
						}
						else {
							return LineType.NoLine;
						}
					}
				}
				
				break;
			}//End of case ViewType.TopView
			case ViewType.RightView: {
				break;
			}
			case ViewType.FrontView: {
				break;
			}
		}

		return LineType.NoLine;
	}//End of GetLineType

}
