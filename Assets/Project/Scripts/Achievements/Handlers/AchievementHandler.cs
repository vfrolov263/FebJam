using UnityEngine;

namespace FebJam
{
    public class AchievementHandler : MonoBehaviour
    {
        [SerializeField]
        private AchievementData _achievementData;

        protected void Achieve()
        {
            ServiceLocator.GetService<AchievementsDatabase>().Add(_achievementData);
            Destroy(gameObject);
        }
    }
}
