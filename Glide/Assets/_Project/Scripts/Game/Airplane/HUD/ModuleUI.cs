using UnityEngine;
using UnityEngine.UI;

namespace Gisha.Glide.Game.AirplaneGeneric.HUD
{
    public class ModuleUI : MonoBehaviour
    {
        Image _image;
        
        public void Initialize(Sprite sprite)
        {
            _image = GetComponent<Image>();
            _image.sprite = sprite;
        }
    }
}