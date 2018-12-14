using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueChanger {


	public static Color hueChanger(Color colortoChange, float hueIncrease){
		float H, S, V;

		Color.RGBToHSV (colortoChange, out H, out S, out V);
		H = H + hueIncrease;
		return Color.HSVToRGB (H, S, V);
	}
}
