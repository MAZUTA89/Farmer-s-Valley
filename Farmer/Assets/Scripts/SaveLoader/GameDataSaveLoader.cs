using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Assets.Scripts;
using Newtonsoft.Json.Linq;
using AScripts.SaveLoader;

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
                JsonConvert.DeserializeObject<GameDataState>(json,
                new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new ItemPlacementDataConverter() }
                });
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

        public class ItemPlacementDataConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PlacementItemData);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject item = JObject.Load(reader);
                var typeName = item["ItemTypeName"].ToString();
                switch (typeName)
                {
                    case nameof(ChestData):
                        return item.ToObject<ChestData>(serializer);
                    case nameof(SandData):
                        return item.ToObject<SandData>(serializer);
                    default:
                        return item.ToObject<PlacementItemData>(serializer);
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
