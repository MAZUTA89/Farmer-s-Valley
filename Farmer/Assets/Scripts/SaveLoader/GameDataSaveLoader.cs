using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Scripts.SaveLoader
{
    public class GameDataSaveLoader
    {
        const string c_levelsNamesKey = "LevelNamesKeys";
        public GameDataSaveLoader() { }

        public void SaveGameState(GameDataState gameDataState)
        {
            string json = JsonConvert.SerializeObject(gameDataState);
            PlayerPrefs.SetString(gameDataState.GameDataStateName,
                json);
        }
        
        public GameDataState LoadGameState(string keyName)
        {
            string json = PlayerPrefs.GetString(keyName);
            GameDataState gameDataState =
                JsonConvert.DeserializeObject<GameDataState>(json);
            return gameDataState;
        } 
        public void SaveWorldNamesJson(List<string> worldNames)
        {
            string json = JsonConvert.SerializeObject(worldNames);
            PlayerPrefs.SetString(c_levelsNamesKey, json);
        }
        public List<string> LoadWorldNamesJson()
        {
            string json = PlayerPrefs.GetString(c_levelsNamesKey);
            var names =
                JsonConvert.DeserializeObject<List<string>>(json);
            return names;
        }
    }
}
