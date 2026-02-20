using UnityEngine;

namespace FebJam
{
    public class AchievementNotifier : MonoBehaviour
    {
        [SerializeField]
        Transform _parent;
        [SerializeField]
        AchievementView _achievementViewPrefab;
        [SerializeField]
        private float _showTime;
        private AchievementView _achievementView;

        public void Awake()
        {
            ServiceLocator.GetService<AchievementsDatabase>().Achieved += OnAchieved;
        }

        private void OnAchieved(AchievementData achievement)
        {
            if (_achievementView != null)
            {
                DestroyImmediate(_achievementView);
            }

            _achievementView = Instantiate(_achievementViewPrefab, _parent);
            _achievementView.Init(achievement.Sprite, achievement.Name, achievement.Description);
            Destroy(_achievementView.gameObject, _showTime);
        }

        private void OnDestroy()
        {
            ServiceLocator.GetService<AchievementsDatabase>().Achieved -= OnAchieved;
        }
    }
}