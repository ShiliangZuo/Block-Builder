using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewMap {

	public Dictionary<Segment, LineType> lines;
	public int sizeX, sizeZ;

	public ViewMap(Dictionary<Segment, LineType> lines, int sizeX, int sizeZ) {
		this.lines = lines;
		this.sizeX = sizeX;
		this.sizeZ = sizeZ;
	}

}
