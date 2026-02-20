using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace FebJam
{
    /// <summary>
    /// Services that persist between game scenes.
    /// </summary>
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private PauseMenuController _pauseMenuControllerPrefab;
        [SerializeField]
        private DialogueManager _dialogueManagerPrefab;
        [SerializeField]
        private ResourcesDatabase _resourcesDatabase;
        [SerializeField]
        private GameObject _hudPrefab;
        [SerializeField]
        private float _timeBetweenRandomEventsMin;
        [SerializeField]
        private float _timeBetweenRandomEventsMax = 15f;
        private AchievementNotifier _achievementNotifier;
        private ResourcesView _resourcesView;
        private PauseMenuController _pauseMenuController;
        private GameObject _gameplayServices;
        private DialogueManager _dialogueManager;

        private static bool _isInstantiated;

        public static void Init()
        {
            _isInstantiated = false;
        }

        private void Awake()
        {
            if (_isInstantiated)
            {
                Debug.LogWarning("Duplicate Gameplay Entry Point");
                Destroy(gameObject);
                return;
            }

            _isInstantiated = true;

            _gameplayServices = new GameObject("GameplayServices");
            transform.parent = _gameplayServices.transform; // For dispose after gameplay exit
            DontDestroyOnLoad(_gameplayServices);
            InitServices();
        }

        public void InitServices()
        {
            ServiceLocator.AddService(new InputHandler());

            GameObject hud = Instantiate(_hudPrefab);
            hud.gameObject.transform.SetParent(_gameplayServices.transform);

            _achievementNotifier = hud.GetComponentInChildren<AchievementNotifier>();
            ServiceLocator.AddService(_achievementNotifier);

            _pauseMenuController = Instantiate(_pauseMenuControllerPrefab);
            _pauseMenuController.gameObject.transform.SetParent(_gameplayServices.transform);
            ServiceLocator.AddService(_pauseMenuController);

            ServiceLocator.AddService(_resourcesDatabase);
            ServiceLocator.AddService(new ResourcesSet());
            _resourcesView = hud.GetComponentInChildren<ResourcesView>();
            _resourcesView.Init();
            ServiceLocator.AddService(_resourcesView);

            ServiceLocator.AddService(new RandomEventsManager(this, _timeBetweenRandomEventsMin, _timeBetweenRandomEventsMax));

            _dialogueManager = Instantiate(_dialogueManagerPrefab, _gameplayServices.transform);
            ServiceLocator.AddService(_dialogueManager);
            _dialogueManager.gameObject.SetActive(false);
        }

        public void OnDestroy()
        {
            ServiceLocator.DeepReleaseService<AchievementNotifier>();
            ServiceLocator.DeepReleaseService<PauseMenuController>();
            ServiceLocator.DeepReleaseService<InputHandler>();
            ServiceLocator.DeepReleaseService<ResourcesView>();
            ServiceLocator.DeepReleaseService<ResourcesSet>();
            ServiceLocator.DeepReleaseService<ResourcesDatabase>();
            ServiceLocator.DeepReleaseService<RandomEventsManager>();
            ServiceLocator.DeepReleaseService<DialogueManager>();
            Destroy(_gameplayServices);
        }
    }
}