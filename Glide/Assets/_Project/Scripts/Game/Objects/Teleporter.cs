using Gisha.Glide.Game.Core;
using UnityEngine;

namespace Gisha.Glide.Game.Objects
{
    public class Teleporter : TriggerableObject
    {
        public override void OnTriggerSignal(Collider other) => GameManager.OnPassLevel();
    }
}