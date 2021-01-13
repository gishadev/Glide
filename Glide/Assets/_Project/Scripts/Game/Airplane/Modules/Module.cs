using Gisha.Glide.Game.Core;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public abstract class Module
    {
        public virtual void Use(Airplane airplane)
        {
            ScoreProcessor.AddScore(10);
        }

        public ModulesData GetModulesData() => PathBuilder.GetModulesData();

        public ModuleUIData GetModuleUIData() => PathBuilder.GetModuleUIData(this.GetType().Name);
    }
}