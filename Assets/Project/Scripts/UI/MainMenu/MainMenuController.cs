using UnityEngine;
using UnityEngine.SceneManagement;

namespace FebJam
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel, _settingsPanel, _achievmentsPanel, _backBtn;

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
            Bootstrapper.SetStage(GameExecutionStage.Gameplay);
        }

        public void OpenMain()
        {
            _mainPanel.SetActive(true);
            _achievmentsPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _backBtn.SetActive(false);
        }

        public void OpenAchievments()
        {
            _mainPanel.SetActive(false);
            _achievmentsPanel.SetActive(true);
            _backBtn.SetActive(true);
        }

        public void OpenSettings()
        {
            _mainPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            _backBtn.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
