using System;
using System.Collections.Generic;
using UnityEngine;

namespace FebJam
{
    public class ResourcesView : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private ResourceView _resourceViewPrefab;

        private Dictionary<string, ResourceView> _resourcesView = new();

        public void Init()
        {
            ServiceLocator.GetService<ResourcesSet>().ResourceAdded += OnResourceAdded;
        }

        public void Dispose()
        {
            if (ServiceLocator.TryGetService(out ResourcesSet resourcesSet))
                resourcesSet.ResourceAdded += OnResourceAdded;
        }

        private void OnResourceAdded(ResourceData data, float value)
        {
            ResourceView resourceView;

            if (!_resourcesView.ContainsKey(data.Name))
            {
                resourceView = Instantiate(_resourceViewPrefab, _parent);
                _resourcesView.Add(data.Name, resourceView);
            }
            else
            {
                resourceView = _resourcesView[data.Name];
            }

            resourceView.Init(data.Sprite, data.Name, value);
        }
    }
}