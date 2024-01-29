using System;

[Serializable]
public class SettingsData
{
    public bool SoundEnabled;
    public bool VibrationEnabled;

    public SettingsData()
    {
        SoundEnabled = true;
        VibrationEnabled = false;
    }
}
