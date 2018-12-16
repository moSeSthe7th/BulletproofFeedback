using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme {
    public string Name { get; set; }

    public Color32 BlockColor { get; set; }
    public Color32 HexagonColor { get; set; }
    public Color32 HexagonLight { get; set; }
    public Color32 BrighterRail { get; set; }
    public Color32 fogColor { get; set; }
    public Material BackGround { get; set; }

    public Theme(string name,Color32 block,Color32 hexagon,Color32 hexagonLight,Color32 rail,Color32 fog, Material skybox)
    {
        Name = name;
        BlockColor = block;
        HexagonColor = hexagon;
        HexagonLight = hexagonLight;
        BrighterRail = rail;
        fogColor = fog;
        BackGround = skybox;
    }
}
