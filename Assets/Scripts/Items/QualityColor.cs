using System;
using System.Collections.Generic;

public enum Quality { Common, Uncommon, Rare, Epic }

public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        { Quality.Common, "#FFF390" }, // yellow
        { Quality.Uncommon, "#9BE783" }, // green
        { Quality.Rare, "#FC9552" }, // orange
        { Quality.Epic, "#D82D2D" } // red
    };

    public static Dictionary<Quality, string> MyColors { get => colors; }
}
