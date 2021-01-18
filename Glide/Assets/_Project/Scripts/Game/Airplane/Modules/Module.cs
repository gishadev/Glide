using Gisha.Glide.Game.Core;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    #region Module
    public abstract class Module
    {
        public virtual void Use(Airplane airplane)
        {
            ScoreProcessor.AddScore(10);
        }

        public ModulesData GetModulesData() => PathBuilder.GetModulesData();

        public ModuleUIData GetModuleUIData() => PathBuilder.GetModuleUIData(this.GetType().Name);
    }
    #endregion
    //---------------------------------------------------------------------------------------------------
    #region DashModule: Dash on short distance
    public class DashModule : Module
    {
        public override void Use(Airplane airplane)
        {
            base.Use(airplane);

            var data = GetModulesData();

            var ray = new Ray(airplane.transform.position, airplane.transform.forward);
            var airplaneColliders = airplane.GetComponentsInChildren<Collider>();
            RaycastHit[] hits = Physics.SphereCastAll(ray, 1.5f, data.DashDistance)
                .Where(x => !airplaneColliders.Contains(x.collider))
                .ToArray();

            var distance = data.DashDistance;
            if (hits.Length > 0)
                distance = hits.Min(x => x.distance);

            airplane.StartCoroutine(Dash(airplane, data, distance));
        }

        private IEnumerator Dash(Airplane airplane, ModulesData data, float distance)
        {
            airplane.EnginePushing = false;
            var rb = airplane.GetComponent<Rigidbody>();
            var path = 0f;
            while (path < distance)
            {
                var step = data.DashSpeed * Time.fixedDeltaTime;
                path += step;
                rb.MovePosition(rb.position + airplane.transform.forward * step);

                yield return null;
            }
            airplane.EnginePushing = true;
        }
    }
    #endregion
}