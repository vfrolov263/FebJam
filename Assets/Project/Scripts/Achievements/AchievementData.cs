using UnityEngine;

namespace FebJam
{
    [CreateAssetMenu(fileName = "AchievementData", menuName = "Scriptable Objects/AchievementData")]
    public class AchievementData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
    }
}