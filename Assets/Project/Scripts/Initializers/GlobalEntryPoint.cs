using BigProject.Managers;
using System;
using UnityEngine;

namespace FebJam
{
    /// <summary>
    /// Global services and settings.
    /// </summary>
    public class GlobalEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private MusicManager _musicManagerPrefab;
        [SerializeField]
        private SoundsManager _soundsManagerPrefab;

        private GameObject _globalServices;
        private MusicManager _musicManager;
        private SoundsManager _soundsManager;

        private static bool _isInstantiated;

        public static void Init()
        {
            _isInstantiated = false;
        }

        private void Awake()
        {
            if (_isInstantiated)
            {
                Debug.LogWarning(String.Format("Duplicate Global Entry Point"));
                Destroy(gameObject);
                return;
            }
            
            _isInstantiated = true;

            _globalServices = new GameObject("GlobalServices");
            DontDestroyOnLoad(_globalServices);
            InitServices();
        }

        private void InitServices()
        {
            ServiceLocator.AddService(new AchievementsDatabase());

            _musicManager = Instantiate(_musicManagerPrefab);
            _soundsManager = Instantiate(_soundsManagerPrefab);
            _musicManager.transform.SetParent(_globalServices.transform);
            _soundsManager.transform.SetParent(_globalServices.transform);
            ServiceLocator.AddService(_musicManager);
            ServiceLocator.AddService(_soundsManager);
        }
    }
}