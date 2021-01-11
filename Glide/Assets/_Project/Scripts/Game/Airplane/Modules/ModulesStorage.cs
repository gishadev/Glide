using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace Gisha.Glide.Game.AirplaneGeneric.Modules
{
    public static class ModulesStorage
    {
        static Dictionary<string, Module> _modules = new Dictionary<string, Module>();
        static bool _isInitialized = false;

        private static void Initialize()
        {
            _modules.Clear();
            var allModuleTypes = Assembly.GetAssembly(typeof(Module)).GetTypes()
                .Where(t => typeof(Module).IsAssignableFrom(t) && t.IsAbstract == false);

            foreach(var moduleType in allModuleTypes)
            {
                Module module = Activator.CreateInstance(moduleType) as Module;
                _modules.Add(moduleType.Name, module);
            }

            _isInitialized = true;
        }

        public static Module GetModuleFromTypeName(string typeName)
        {
            if (!_isInitialized)
                Initialize();

            var module = _modules[typeName];
            if (module == null)
                Debug.LogError($"Module with type name: {typeName} not found.");

            return module;
        }
    }
}