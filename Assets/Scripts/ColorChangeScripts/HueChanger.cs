using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueChanger {


	public static Color hueChanger(Color colortoChange, float hueIncrease){
		float H, S, V;

		Color.RGBToHSV (colortoChange, out H, out S, out V);
		/*if (H + hueIncrease > 0.65 && H > hueIncrease)
			H = H - hueIncrease;
		else if (H + hueIncrease > 0.65 && H < hueIncrease)
			H = hueIncrease - H;
		else*/
		H = H + hueIncrease;
		H = H % 0.65f;
		//Debug.Log ("Hue = " + H);
		return Color.HSVToRGB (H, S, V);
	}
}


//güzel duran renkler 
//006A64 	orjinal renk

//DB6F66	turuncu
//DB8D66	turuncu
//3E6380	mavi
//5B0007	koyu kırmızı
//522A2D	koyu kırmızı
//A4373F	kırmızı
//482B3E *	koyu mormumsu
//6A95C8	acık mavi
//254060	koyu mavi
//1F626A	turkuaz
//378C6E	yeşil
//49633F	asker yeşili