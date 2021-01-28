using System.Linq;
using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public static class CoordsManager
    {
        public static LevelCoords CurrentCoords { get; private set; }

        public static void SetCoords(LevelCoords coords) => CurrentCoords = coords;

        public static LevelCoords GetNextCoords()
        {
            var data = SaveSystem.LoadLevelsData();

            var maxLevelsInWorld = data.allLevels
                .Where(x => x.Key.WorldID == CurrentCoords.WorldID)
                .Max(x => x.Key.LevelID);
            var maxWorldsInGalaxy = data.allLevels
                .Where(x => x.Key.GalaxyID == CurrentCoords.GalaxyID)
                .Max(x => x.Key.WorldID);

            var newLevelID = CurrentCoords.LevelID + 1;
            var newWorldID = CurrentCoords.WorldID + 1;

            if (newLevelID > maxLevelsInWorld && newWorldID > maxWorldsInGalaxy)
            {
                Debug.Log("<color=blue>Player is on last coords.</color>");
                return CurrentCoords;
            }

            var levelID = newLevelID <= maxLevelsInWorld ? newLevelID : 0;
            var worldID = newLevelID > maxLevelsInWorld && newWorldID <= maxWorldsInGalaxy ? newWorldID : CurrentCoords.WorldID;

            return new LevelCoords(CurrentCoords.GalaxyID, worldID, levelID);
        }

        public static LevelCoords MoveNext()
        {
            CurrentCoords = GetNextCoords();
            return CurrentCoords;
        }
    }

    public struct LevelCoords
    {
        public int GalaxyID { private set; get; }
        public int WorldID { private set; get; }
        public int LevelID { private set; get; }

        public string DebugText => $"[{GalaxyID},{WorldID},{LevelID}]";

        public LevelCoords(int galaxyID, int worldID, int levelID)
        {
            GalaxyID = galaxyID;
            WorldID = worldID;
            LevelID = levelID;
        }
    }
}