using Gisha.Glide.Game.Core;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public abstract class Module
    {
        public abstract void Use(Airplane airplane);
        public ModulesData GetModulesData() => PathBuilder.GetModulesData();

        public ModuleUIData GetModuleUIData() => PathBuilder.GetModuleUIData(this.GetType().Name);
    }
}