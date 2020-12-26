using Gisha.Glide.Game;
using UnityEngine;

namespace Gisha.Glide.Objects
{
    public class Teleporter : TriggerableObject
    {
        public override void OnTriggerSignal(Collider other)
        {
            Debug.Log("<color=green>Airplane was teleported!</color>");
            GameManager.LoadNextLevel();
        }
    }
}