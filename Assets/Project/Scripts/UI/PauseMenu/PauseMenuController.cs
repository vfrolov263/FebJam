using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FebJam
{
    public class PauseMenuController : MonoBehaviour
    {
        private GameObject _pausePanel;

        private void Awake()
        {
            _pausePanel = transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            ServiceLocator.GetService<InputHandler>().Canceled += SwitchPause;
        }

        private void OnDisable()
        {
            if (ServiceLocator.TryGetService<InputHandler>(out var input))
                input.Canceled -= SwitchPause;
        }

        public void SwitchPause()
        {
            _pausePanel.SetActive(!_pausePanel.activeSelf);
            Time.timeScale = 1f - Time.timeScale;
        }

        public void ExitToMenu()
        {
            SwitchPause();
            Bootstrapper.SetStage(GameExecutionStage.Launch);
            SceneManager.LoadScene(0);
        }
    }
}