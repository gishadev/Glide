using Gisha.Glide.Game.AirplaneGeneric.Modules;
using UnityEngine;

namespace Gisha.Glide.Game.Core
{
    public static class PathBuilder
    {
        // Levels.
        public const string MainRelativePath = "_Project/Scenes/Main";
        public const string GalaxiesRelativePath = "_Project/Scenes/Galaxies";
        public const string LevelsMapRelativePath = "ScriptableObjects/LevelsMap";

        // Modules.
        public const string ModulesDataRelativePath = "ScriptableObjects/ModulesData";
        public const string ModulesUIDataRelativePath = "ScriptableObjects/ModulesUI";

        public static LevelsMap GetLevelsMapAsset() => Resources.Load(LevelsMapRelativePath, typeof(LevelsMap)) as LevelsMap;
        public static ModulesData GetModulesData() => Resources.Load(ModulesDataRelativePath, typeof(ModulesData)) as ModulesData;
        public static ModuleUIData GetModuleUIData(string typeName) => Resources.Load($"{ModulesUIDataRelativePath}/{typeName}", typeof(ModuleUIData)) as ModuleUIData;

        public static string GetScenePathFromCoords(LevelCoords coords)
        {
            var map = GetLevelsMapAsset();

            return $"{GalaxiesRelativePath}" +
                $"/{map.galaxies[coords.GalaxyID].galaxyName}" +
                $"/{map.galaxies[coords.GalaxyID].worldNames[coords.WorldID]}" +
                $"/Level {coords.LevelID + 1}";
        }

        public static string GetScenePathFromNames(string galaxyName, string worldName, int levelIndex)
            => $"{GalaxiesRelativePath}/{galaxyName}/{worldName}/Level {levelIndex + 1}";

        public static string GetPathToMainScene(string sceneName)
            => $"{MainRelativePath}/{sceneName}";
    }
}