using Gisha.Glide.Game.AirplaneGeneric;
using TMPro;
using UnityEngine;

namespace Gisha.Glide.Game.Location.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text[] allText = default;

        [Header("General")]
        [SerializeField] private float maxDistForTextAppear = default;

        Transform airplane;

        private void Start()
        {
            airplane = AirplaneSpawner.Instance.Airplane.transform;
        }

        private void Update()
        {
            foreach (var t in allText)
            {
                var dist = (t.transform.position - airplane.position).magnitude;
                var opacity = Mathf.Max((maxDistForTextAppear - dist) / 200f, 0);
                t.color = new Color(t.color.r, t.color.g, t.color.b, opacity);
            }
        }

        [ContextMenu("Get Text Children")]
        private void GetTextChildren() => allText = GetComponentsInChildren<TMP_Text>();
    }
}