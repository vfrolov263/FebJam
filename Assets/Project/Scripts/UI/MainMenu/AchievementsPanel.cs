using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FebJam
{
    public class AchievementsPanel : MonoBehaviour
    {
        [SerializeField]
        private AchievementView _achievementViewPrefab;

        private void Awake()
        {
            foreach (AchievementData achievement in ServiceLocator.GetService<AchievementsDatabase>())
            {
                CreateAchievementView(achievement);
            }
        }

        private void CreateAchievementView(AchievementData achievementData)
        {
            var achievmentView = Instantiate(_achievementViewPrefab);
            achievmentView.Init(achievementData.Sprite, achievementData.Name, achievementData.Description);
            achievmentView.transform.SetParent(transform);
        }
    }
}