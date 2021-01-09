using System.Linq;
using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric.Modules
{
    public class DashModule : Module
    {
        public override void Use(Airplane airplane)
        {
            var data = GetModulesData();

            var ray = new Ray(airplane.transform.position, airplane.transform.forward);
            var airplaneColliders = airplane.GetComponentsInChildren<Collider>();
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.65f, data.DashDistance)
                .Where(x => !airplaneColliders.Contains(x.collider))
                .ToArray();

            var distance = data.DashDistance;
            if (hits.Length > 0)
                distance = hits.Min(x => x.distance);

            var rb = airplane.GetComponent<Rigidbody>();
            rb.position += airplane.transform.forward * distance;
        }
    }
}