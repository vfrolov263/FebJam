using UnityEngine;
using UnityEngine.Events;

namespace FebJam
{
    /// <summary>
    /// Scene dependencies.
    /// </summary>
    public class GameplaySceneEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharacterDialogue _dialogue;
        [SerializeField]
        private GameplayManager _gameplayManager;
        [SerializeField]
        private Papers _papers;
        [SerializeField]
        private ImagesData _imagesData;

        [SerializeField, Tooltip("Actions to execute for early initialize.")]
        private UnityEvent _initActions;

        private void Awake()
        {
#if UNITY_EDITOR
            if (Bootstrapper.Stage != GameExecutionStage.Gameplay)
            {
                Bootstrapper.SetStage(GameExecutionStage.Gameplay);
            }
#endif

            InitScene();
            _initActions?.Invoke();
        }

        private void InitScene()
        {
            _gameplayManager.Init(_dialogue, _papers, _imagesData, ServiceLocator.GetService<AchievementsDatabase>());
        }
    }
}