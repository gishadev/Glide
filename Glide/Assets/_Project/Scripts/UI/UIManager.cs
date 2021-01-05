using UnityEngine;

namespace Gisha.Glide.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject levelsMenu = default;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                levelsMenu.SetActive(!levelsMenu.activeSelf);
        }
    }
}