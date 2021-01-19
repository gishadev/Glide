using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public static class SaveSystem
    {
        private static string LevelsDataPath = Application.persistentDataPath + "/levels.data";

        #region LevelsData
        public static void SaveLevelsData(LevelsData levelsData)
        {
            var dictionary = new Dictionary<int[], LevelData>();
            var levelCoords = levelsData.allLevels.Keys.ToArray();
            var levelStates = levelsData.allLevels.Values.Select(x => x.LevelState).ToArray();
            var levelScores = levelsData.allLevels.Values.Select(x => x.BestScore).ToArray();

            for (int i = 0; i < levelsData.allLevels.Count; i++)
            {
                int[] coords = {
                    levelCoords[i].GalaxyID,
                    levelCoords[i].WorldID,
                    levelCoords[i].LevelID};

                int levelState = (int)levelStates[i];
                int bestScore = levelScores[i];

                LevelData level = new LevelData(levelState, bestScore);
                dictionary.Add(coords, level);
            }

            var data = DictionarySerializer<int[], LevelData>.Save(dictionary);
            File.WriteAllBytes(LevelsDataPath, data);
        }

        public static LevelsData LoadLevelsData()
        {
            if (File.Exists(LevelsDataPath))
            {
                var bytes = File.ReadAllBytes(LevelsDataPath);

                var dictionary = DictionarySerializer<int[], LevelData>.Load(bytes);
                var keys = dictionary.Keys.ToArray();
                var values = dictionary.Values.ToArray();

                var allLevels = new Dictionary<LevelCoords, LevelData>();
                for (int i = 0; i < dictionary.Count; i++)
                {
                    var coords = new LevelCoords(keys[i][0], keys[i][1], keys[i][2]);
                    var level = values[i];

                    allLevels.Add(coords, level);
                }

                var data = new LevelsData(allLevels);
                return data;
            }
            else
            {
                Debug.LogError("File not found in " + LevelsDataPath);
                return null;
            }
        }
        #endregion
    }

    public static class DictionarySerializer<TKey, TValue>
    {
        public static byte[] Save(Dictionary<TKey, TValue> dictionary)
        {
            var binFormatter = new BinaryFormatter();
            using (var mStream = new MemoryStream())
            {
                binFormatter.Serialize(mStream, dictionary);
                return mStream.ToArray();
            }
        }

        public static Dictionary<TKey, TValue> Load(byte[] bArray)
        {
            using (var mStream = new MemoryStream())
            {
                var binFormatter = new BinaryFormatter();

                mStream.Write(bArray, 0, bArray.Length);
                mStream.Position = 0;

                return binFormatter.Deserialize(mStream) as Dictionary<TKey, TValue>;
            }
        }
    }
}