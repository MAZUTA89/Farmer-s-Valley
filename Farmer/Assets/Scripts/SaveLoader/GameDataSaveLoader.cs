using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Assets.Scripts;
using Newtonsoft.Json.Linq;
using AScripts.SaveLoader;
using Scripts.InventoryCode;

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
                    Converters = new List<JsonConverter> {
                        new ItemPlacementDataConverter(),
                        new InventoryItemsDataConverter(),
                    }
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
                    case nameof(TreeData):
                        return item.ToObject<TreeData>(serializer);
                    case nameof(SeedData):
                        return item.ToObject<SeedData>(serializer);
                    default:
                        return item.ToObject<PlacementItemData>(serializer);
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        public class InventoryItemsDataConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(InventoryItemData);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var item = JObject.Load(reader);
                var inventoryItemTypeName = item["InventoryItemTypeName"].ToString();
                switch (inventoryItemTypeName)
                {
                    case nameof(HoeItemData):
                        return item.ToObject<HoeItemData>(serializer);
                    case nameof(BasketItemData):
                        return item.ToObject<BasketItemData>(serializer);
                    case nameof(ProductItemData):
                        return item.ToObject<ProductItemData>(serializer);
                    case nameof(WateringItemData):
                        return item.ToObject<WateringItemData>(serializer);
                    case nameof(SeedBagItemData):
                        return item.ToObject<SeedBagItemData>(serializer);
                    default:
                        throw new Exception("Error with json converter");
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
