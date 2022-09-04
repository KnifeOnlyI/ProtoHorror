using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Prefabs.Player.Scripts;
using UnityEngine;

namespace Utils
{
    public static class SaveUtils
    {
        private static readonly BinaryFormatter BinaryFormatter = new BinaryFormatter();
        private static readonly string SavePath = $"{Application.persistentDataPath}/save.dat";

        public static void SavePlayer(Player player)
        {
            Debug.Log("Save in progress...");
            var stream = new FileStream(SavePath, FileMode.Create);
            var data = new PlayerData(player);

            BinaryFormatter.Serialize(stream, data);

            stream.Close();
            Debug.Log("Save completed succesfully");
        }

        public static PlayerData LoadPlayer()
        {
            Debug.Log("Load in progress...");
            PlayerData data = null;

            if (File.Exists(SavePath))
            {
                var stream = new FileStream(SavePath, FileMode.Open);

                data = BinaryFormatter.Deserialize(stream) as PlayerData;
                Debug.Log("Load succesfully game data");
            }
            else
            {
                Debug.LogError("Cannot load save");
            }

            return data;
        }
    }
}