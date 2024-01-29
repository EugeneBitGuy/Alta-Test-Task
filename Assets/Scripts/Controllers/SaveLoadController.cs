using Newtonsoft.Json;
using UnityEngine;

public sealed class SaveLoadController : MonoSingletone<SaveLoadController>
{
    private const string KEY = "SaveData";

    private bool _shouldSave;

    public SaveLoadData Data { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public void Save()
    {
        if(Data == null)
            return;

        var jsonStringToSave = JsonConvert.SerializeObject(Data);
        PlayerPrefs.SetString(KEY, jsonStringToSave);
    }

    private void Load()
    {
        string savedJsonString = PlayerPrefs.GetString(KEY, string.Empty);

        if (string.IsNullOrEmpty(savedJsonString))
        {
            Data = new SaveLoadData();
        }
        else
        {
            Data = JsonConvert.DeserializeObject<SaveLoadData>(savedJsonString);
        }
    }
}
