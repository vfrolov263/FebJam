using System;
using UnityEngine;

namespace FebJam
{
    [Serializable]
    public class ResourceData
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public float Min { get; private set; }
        [field: SerializeField]
        public float Max { get; private set; }
    }
}