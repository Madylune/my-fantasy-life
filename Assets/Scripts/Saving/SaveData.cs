using System;

public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public SaveData()
    {

    }
}

[Serializable]
public class PlayerData
{
    public int MyLevel { get; set; }

    public PlayerData(int level)
    {
        this.MyLevel = level;
    }
}
