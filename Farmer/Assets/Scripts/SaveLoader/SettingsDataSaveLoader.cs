using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

namespace Scripts.SaveLoader
{
    public class SettingsDataSaveLoader
    {
        public void Save(SettingsData data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(GameConfiguration.SaveSettingsKeyName, json);
        }
        public SettingsData Load()
        {
            var json = PlayerPrefs.GetString(GameConfiguration.SaveSettingsKeyName);
            return JsonConvert.DeserializeObject<SettingsData>(json);
        }
        /// <summary>
        /// возвращает true, если нет сохранения
        /// </summary>
        /// <returns></returns>
        public bool IsDefault()
        {
            return !PlayerPrefs.HasKey(GameConfiguration.SaveSettingsKeyName);
        }
    }
}
