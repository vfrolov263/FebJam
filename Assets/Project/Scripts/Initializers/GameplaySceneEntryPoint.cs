using UnityEngine;
using UnityEngine.Events;

namespace FebJam
{
    /// <summary>
    /// Scene dependencies.
    /// </summary>
    public class GameplaySceneEntryPoint : MonoBehaviour
    {
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

        }
    }
}