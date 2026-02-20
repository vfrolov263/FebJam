using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FebJam
{
    [CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Scriptable Objects/ResourcesDatabase")]
    public class ResourcesDatabase : ScriptableObject
    {
        [SerializeField]
        private List<ResourceData> _resourcesData;

        public ResourceData GetResourceData(string name)
        {
            return _resourcesData.First(x => x.Name == name);
        }
    }
}