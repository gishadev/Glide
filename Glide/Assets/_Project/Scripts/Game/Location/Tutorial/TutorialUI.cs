using Gisha.Glide.Game.AirplaneGeneric;
using TMPro;
using UnityEngine;

namespace Gisha.Glide.Game.Location.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float maxDistForTextAppear = default;

        TMP_Text[] _allText;
        Transform _airplane;

        private void Awake()
        {
            _allText = GetComponentsInChildren<TMP_Text>();
            _airplane = AirplaneSpawner.Instance.Airplane.transform;
        }

        private void Update()
        {
            foreach (var t in _allText)
            {
                var dist = (t.transform.position - _airplane.position).magnitude;
                var opacity = Mathf.Max((maxDistForTextAppear - dist) / 200f, 0);
                t.color = new Color(t.color.r, t.color.g, t.color.b, opacity);
            }
        }
    }
}