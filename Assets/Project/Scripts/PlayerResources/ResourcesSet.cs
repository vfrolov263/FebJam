using System;
using System.Collections.Generic;
using UnityEngine;

namespace FebJam
{
    public class ResourcesSet
    {
        private ResourcesDatabase _resourcesDatabase;
        private Dictionary<string, float> _resources = new();

        public event Action<ResourceData, float> ResourceAdded;

        public ResourcesSet()
        {
            _resourcesDatabase = ServiceLocator.GetService<ResourcesDatabase>();
        }

        public void AddResource(string name, float amount)
        {
            var resourceData = _resourcesDatabase.GetResourceData(name);

            if (resourceData == null)
                return;

            if (!_resources.ContainsKey(name))
            {
                _resources.Add(name, new());
            }

            _resources[name] += amount;
            _resources[name] = Mathf.Clamp(_resources[name], resourceData.Min, resourceData.Max);
            ResourceAdded?.Invoke(resourceData, _resources[name]);
        }
    }
}