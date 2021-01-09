using Gisha.Glide.AirplaneGeneric.Modules;
using UnityEditor;

namespace Gisha.Glide.Game
{
    public static class PathBuilder
    {
        // Levels.
        public const string MainRelativePath = "_Project/Scenes/Main";
        public const string GalaxiesRelativePath = "_Project/Scenes/Galaxies";
        public const string LevelsMapRelativePath = "Assets/_Project/ScriptableObjects/LevelsMap.asset";

        // Modules.
        public const string ModulesDataRelativePath = "Assets/_Project/ScriptableObjects/ModulesData.asset";

        public static LevelsMap GetLevelsMapAsset() => AssetDatabase.LoadAssetAtPath(LevelsMapRelativePath, typeof(LevelsMap)) as LevelsMap;
        public static ModulesData GetModulesData() => AssetDatabase.LoadAssetAtPath(ModulesDataRelativePath, typeof(ModulesData)) as ModulesData;

        public static string GetScenePathFromCoords(LevelCoords coords)
        {
            var map = GetLevelsMapAsset();

            return $"{GalaxiesRelativePath}/" +
                $"{map.galaxies[coords.GalaxyID].galaxyName}/" +
                $"{map.galaxies[coords.GalaxyID].worldNames[coords.WorldID]}/" +
                $"Level {coords.LevelID + 1}";
        }

        public static string GetScenePathFromNames(string galaxyName, string worldName, int levelIndex) 
            => $"{GalaxiesRelativePath}/{galaxyName}/{worldName}/Level {levelIndex + 1}";

        public static string GetPathToMainScene(string sceneName) 
            => $"{MainRelativePath}/{sceneName}";
    }
}