using System;

[Serializable]
public class SaveLoadData
{
    public GameData GameData;
    public SettingsData SettingsData;

    public SaveLoadData()
    {
        GameData = new GameData();
        SettingsData = new SettingsData();
    }
}
