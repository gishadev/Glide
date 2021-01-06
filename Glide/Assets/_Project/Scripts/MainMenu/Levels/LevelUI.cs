using Gisha.Glide.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gisha.Glide.MainMenu.Levels
{
    public class LevelUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public LevelCoords Coords { get; set; }

        Image _image;

        public void ChangeColor(Color color)
        {
            if (_image == null)
                _image = GetComponent<Image>();

            _image.color = color;
        }

        #region OnPointer
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"{Coords.GalaxyID},{Coords.WorldID},{Coords.LevelID}");
            SceneLoader.LoadLevel(Coords);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.25f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }
        #endregion
    }
}