using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dislands
{
    public class ChangeScene : MonoBehaviour
    {
        public void ChangeToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}