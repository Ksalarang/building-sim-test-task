using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildingSim.Bootstrap
{
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
