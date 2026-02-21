using UnityEngine;
using UnityEngine.SceneManagement;

namespace FebJam
{
    public class LoadNextScene : MonoBehaviour
    {
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}