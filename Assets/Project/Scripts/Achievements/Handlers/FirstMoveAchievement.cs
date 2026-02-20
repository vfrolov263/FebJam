using UnityEngine;

namespace FebJam
{
    public class FirstMoveAchievement : AchievementHandler
    {
        private void OnEnable()
        {
            ServiceLocator.GetService<InputHandler>().Moved += OnMoved;
        }

        private void OnDisable()
        {
            if (ServiceLocator.TryGetService<InputHandler>(out var input))
                input.Moved -= OnMoved;
        }

        private void OnMoved(Vector2 _)
        {
            Achieve();
        }
    }
}
