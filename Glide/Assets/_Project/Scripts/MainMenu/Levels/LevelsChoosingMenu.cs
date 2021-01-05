using Gisha.Glide.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelsChoosingMenu : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform worldsParent = default;
        [SerializeField] private WorldUI[] worldsUI = default;

        private void Start()
        {
            UpdateLevelsData();
            SetLevelsUIData();
        }

        private void OnValidate()
        {
            for (int i = 0; i < worldsUI.Length; i++)
                worldsParent.GetChild(i).name = worldsUI[i].name;
        }

        private void SetLevelsUIData()
        {
            List<LevelCoords> allCoords = new List<LevelCoords>();

            for (int i = 0; i < worldsUI.Length; i++)
                for (int j = 0; j < worldsUI[i].levelsUI.Length; j++)
                {
                    var coords = new LevelCoords(0, i, j);
                    allCoords.Add(coords);

                    LevelUI level = worldsUI[i].levelsUI[j];
                    level.Coords = coords;
                }

            SceneLoader.UpdateLevelsCoords(allCoords);
        }

        [ContextMenu("Update Levels Data")]
        private void UpdateLevelsData()
        {
            worldsUI = new WorldUI[worldsParent.childCount];
            for (int i = 0; i < worldsUI.Length; i++)
            {
                var worldTrans = worldsParent.GetChild(i);

                var levelsUI = worldTrans.GetComponentsInChildren<LevelUI>(true);
                worldsUI[i] = new WorldUI(worldTrans.name, levelsUI);
            }
        }
    }

    [System.Serializable]
    public class WorldUI
    {
        public string name;
        public LevelUI[] levelsUI;

        public WorldUI(string name, LevelUI[] levelsUI)
        {
            this.name = name;
            this.levelsUI = levelsUI;
        }
    }
}