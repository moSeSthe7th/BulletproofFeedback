using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataScript {

	public static Texture ballTexture;
	public static bool isBallTextureChanged;
    public static bool isAdsPurchased;
	//UI da updateda olabilir sürekli bakılır ya da değişebileceği yerlerden sonra bakılır, samplescene da girince bakılır playerprefste de tutulabilir
	//public static int currentLevel;
	//level bitince güncellenir eğer gecilen level = max levelsa ve startingscene nin startında alınır. playerprefste de tutulabilir
	//public static int maxLevel;
	public static int isGameModeEndless; // 0 for level 1 for endless
	public static Color levelModeBlockColor; 
	public static Color levelModeHexogenColor;
    public static int pointHolder;
    public static bool isSessionEnded;

}


//006A64