using System;

[Serializable]
public class GameData
{
    public int LevelNumber;
    public int MoneyValue;

    public GameData()
    {
        LevelNumber = 0;
        MoneyValue = 0;
    }
}
