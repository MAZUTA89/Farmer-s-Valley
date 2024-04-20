using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Assets.Scripts;

namespace Scripts.SaveLoader
{
    public class GameDataSaveLoader
    {
        
        public GameDataSaveLoader() { }

        public void SaveGameState(GameDataState gameDataState)
        {
            string json = JsonConvert.SerializeObject(gameDataState, Formatting.Indented);
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
            PlayerPrefs.SetString(GameConfiguration.SaveLevelNamesKeyName, json);
        }
        public List<string> LoadWorldNamesJson()
        {
            string json = 
                PlayerPrefs.GetString(GameConfiguration.SaveLevelNamesKeyName);
            var names =
                JsonConvert.DeserializeObject<List<string>>(json);
            return names;
        }
    }
}
