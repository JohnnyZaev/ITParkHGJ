using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services
{
    public class SceneLoader : ServiceBase<SceneLoader>
    {
        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private Image progressBar;
        
        public enum SceneNames
        {
            Bootstrap,
            MainMenu,
            Game
        }

        protected override void DoAwake()
        {
            loadingCanvas.SetActive(true);
            LoadScene(SceneNames.MainMenu);
        }

        private void OnDisable()
        {
            loadingCanvas.SetActive(false);
        }

        public async void LoadScene(SceneNames s)
        {
            loadingCanvas.SetActive(true);
            var scene = SceneManager.LoadSceneAsync(s.GetHashCode());
            scene.allowSceneActivation = false;

            do
            {
                await Task.Delay(100);
                progressBar.fillAmount = scene.progress;
            } while (scene.progress < 0.9f);

            await Task.Delay(1000);
            
            scene.allowSceneActivation = true;
            progressBar.fillAmount = 0;
            loadingCanvas.SetActive(false);
        }
    }
}
