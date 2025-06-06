using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    [CreateAssetMenu]
    public class SceneChanger : ScriptableObject
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
