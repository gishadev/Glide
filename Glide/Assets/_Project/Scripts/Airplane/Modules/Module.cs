using Gisha.Glide.Game;

namespace Gisha.Glide.AirplaneGeneric.Modules
{
    public abstract class Module
    {
        public abstract void Use(Airplane airplane);
        public ModulesData GetModulesData() => PathBuilder.GetModulesData();
    }
}