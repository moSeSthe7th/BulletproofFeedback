using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataScript 
{

    public static Texture ballTexture;
    public static bool isBallTextureChanged;
    public static bool isAdsPurchased;
    public static int isGameModeEndless; // 0 for level 1 for endless

    public static int pointHolder;
    public static bool isSessionEnded;
    //-------------For advertisement rewards------
    public static bool isSecondChanceButtonPushedForLevel;
    public static bool isPassedAtLeastOneLevel;
    //------------END--------
    public static int ThemeIndex;

    // Theme template :: new Theme("",new Color32(0x,0x,0x,0xFF),new Color32(0x,0x,0x,0xFF),new Color32(0x,0x,0x,0xFF),new Color32(0x,0x,0x,0xFF),new Color32(0x,0x,0x,0xFF),Resources.Load("Original",typeof(Material)) as Material),
    public static List<Theme> Themes = new List<Theme> {
        new Theme("Original", new Color32(0xBB,0x5F,0x5D,0xFF),new Color32(0x00,0x6A,0x64,0xFF),new Color32(0xF3,0xA5,0xA4,0xFF),new Color32(0x64,0x74,0x78,0xFF),new Color32(0xB0,0xCC,0xC3,0xFF),Resources.Load("Original",typeof(Material)) as Material), //Ana renkler
        new Theme("Zambak", new Color32(0x7D,0x5D,0x26,0xFF), new Color32(0x4F,0x57,0x7B,0xFF),new Color32(0xCC,0x98,0x3D,0xFF),new Color32(0x5D,0x67,0x90,0xFF),new Color32(0xB0,0xCC,0xC3,0xFF),Resources.Load("Original",typeof(Material)) as Material), //lila - turuncu -- tasarım 5
        new Theme("Lakers",new Color32(0x8E,0x67,0x00,0xFF),new Color32(0x4F,0x16,0x43,0xFF),new Color32(0xFF,0xD3,0x5F,0xFF),new Color32(0x65,0x1C,0x56,0xFF),new Color32(0xC3,0x94,0x9C,0xFF),Resources.Load("Lakers",typeof(Material)) as Material), // sarı mor -- tasarım 1
        new Theme("Süreyya",new Color32(0x19,0x73,0x57,0xFF),new Color32(0x4F,0x16,0x43,0xFF),new Color32(0x34,0x84,0x6B,0xFF),new Color32(0x65,0x1C,0x56,0xFF),new Color32(0xBB,0xE4,0xC3,0xFF),Resources.Load("Green",typeof(Material)) as Material),//mor koyu yeşil -- tasarım 3
        new Theme("SeksilikAbidesi",new Color32(0x6F,0x00,0x00,0xFF),new Color32(0x2E,0x2E,0x2E,0xFF),new Color32(0xFF,0x00,0x00,0xFF),new Color32(0x5B,0x5A,0x5A,0xFF),new Color32(0xBB,0xBA,0xB9,0xFF),Resources.Load("Gray",typeof(Material)) as Material),//siyah kırmızı -- tasarım 4 -- seks
        new Theme("Tontik",new Color32(0x26,0x7B,0x7B,0xFF),new Color32(0x82,0x41,0x3F,0xFF),new Color32(0x3D,0xB7,0xB7,0xFF),new Color32(0xA4,0x53,0x51,0xFF),new Color32(0xBB,0xE4,0xC3,0xFF),Resources.Load("Green",typeof(Material)) as Material), // yavru ağzı yeşil -- tasarım 6
        new Theme("SolgunGeceler",new Color32(0x9F,0x62,0x5F,0xFF),new Color32(0x2F,0x4B,0x59,0xFF),new Color32(0xFF,0xAC,0xA8,0xFF),new Color32(0x34,0x53,0x63,0xFF),new Color32(0xEA,0xC7,0xBC,0xFF),Resources.Load("Kiremit",typeof(Material)) as Material), // mavi pembe -- tasarım 7 
        new Theme("SabahGüneşi",new Color32(0xA6,0x4F,0x2E,0xFF),new Color32(0x14,0x30,0x18,0xFF),new Color32(0xCA,0x5F,0x38,0xFF),new Color32(0x18,0x3A,0x1D,0xFF),new Color32(0xEA,0xC7,0xBC,0xFF),Resources.Load("Kiremit",typeof(Material)) as Material), // yeşil koyu turuncu -- tasarım 8
        new Theme("KahveKeyfi",new Color32(0x25,0x6A,0x6A,0xFF),new Color32(0x57,0x47,0x36,0xFF),new Color32(0x31,0x89,0x89,0xFF),new Color32(0x65,0x50,0x3D,0xFF),new Color32(0xBD,0xBB,0xBB,0xFF),Resources.Load("Gray",typeof(Material)) as Material), // kahve mavi -- tasarım 9
        new Theme("FloryaKolleji",new Color32(0x8E,0x6B,0x3D,0xFF),new Color32(0x27,0x49,0x5B,0xFF),new Color32(0xF3,0xCB,0x5B,0xFF),new Color32(0x45,0x56,0x60,0xFF),new Color32(0x8E,0xA2,0x9C,0xFF),Resources.Load("ShopEnteringSceneSkybox",typeof(Material)) as Material),// mavi açık turuncu -- tasarım 2
        new Theme("RedDead",new Color32(0x43,0x43,0x43,0xFF),new Color32(0x60,0x00,0x00,0xFF),new Color32(0x82,0x82,0x82,0xFF),new Color32(0x4F,0x00,0x00,0xFF),new Color32(0xBB,0xBA,0xB9,0xFF),Resources.Load("Gray",typeof(Material)) as Material), // kırmızı siyah -- tasarım 10, seksiest tersi
        new Theme("BlueDead",new Color32(0x43,0x43,0x43,0xFF),new Color32(0x45,0x7B,0x7D,0xFF),new Color32(0x82,0x82,0x82,0xFF),new Color32(0x3E,0x6F,0x71,0xFF),new Color32(0xBB,0xBA,0xB9,0xFF),Resources.Load("Gray",typeof(Material)) as Material) // mavi gri -- tasarım 11
    };
}

/*HER TASARIM İÇİN ARKADAKİ SPARKLELAR HEXAGON RENGİ VE STEP RENGİ OLARAK AYARLANCAK!!!
 
TASARIM 1: (sarı-mor)
Hexogen: 4F1643
Ray 2: 651C56
Blok: 8E6700
Hexo iç: FFD35F
Arkaplan: sarimor
Fog: FFD35F

TASARIM 2: (mavi-acık turuncu)

Hexogen: 27495B
Ray 2: 455660
Blok: 8E6B3D
Hexo iç: B9905A -- yenisi -- F3CB5B
Arkaplan: bacgg3
Fog: B4CDC8

TASARIM 3: (mor-koyu yeşil)

Hexogen: 4F1643
Ray 2: 651C56
Blok: 197357
Hexo iç: 34846B
Arkaplan: yesil
Fog: BBE4C3

TASARIM 4: (siyah-kırmızı)

Hexogen: 2E2E2E
Ray 2: 5B5A5A
Blok: 6F0000
Hexo iç: FF0000
Arkaplan: gri
Fog: BBBAB9

TASARIM 5: (lila-turuncu)
Hexogen: 4F577B
Ray 2: 5D6790
Blok: 7D5D26
Hexo iç: CC983D
Arkaplan: bacgg6
Fog: B0CCC3

TASARIM 6: (yavru ağzı-pembe)
Hexogen: 82413F
Ray 2: A45351
Blok: 267B7B
Hexo iç: 3DB7B7
Arkaplan: yesil
Fog: BBE4C3

TASARIM 7: (
Hexogen: 2F4B59
Ray 2: 345363
Blok: 9F625F
Hexo iç: C37571
Arkaplan: kiremit2
Fog: EAC7BC

TASARIM 8: (yeşil-koyu turuncu)
Hexogen: 143018
Ray 2: 183A1D
Blok: A64F2E
Hexo iç: CA5F38
Arkaplan: kiremit2
Fog: EAC7BC

TASARIM 9: (kahve-mavi)
Hexogen: 574736
Ray 2: 63503D
Blok: 256A6A
Hexo iç: 318989
Arkaplan: gri
Fog: BDBBBB
TASARIM 10: (kırmızı-gri)
Hexogen: 600000
Ray 2: 4F0000
Blok: 434343
Hexo iç: 828282
Arkaplan: gri
Fog: BBBAB9

TASARIM 10: (mavi-gri)
Hexogen: 457B7D
Ray 2: 3E6F71
Blok: 434343
Hexo iç: 828282
Arkaplan: gri
Fog: BBBAB9
*/

//006A64



//public static Color levelModeBlockColor;
//public static Color levelModeHexogenColor;


//UI da updateda olabilir sürekli bakılır ya da değişebileceği yerlerden sonra bakılır, samplescene da girince bakılır playerprefste de tutulabilir
//public static int currentLevel;
//level bitince güncellenir eğer gecilen level = max levelsa ve startingscene nin startında alınır. playerprefste de tutulabilir
//public static int maxLevel;