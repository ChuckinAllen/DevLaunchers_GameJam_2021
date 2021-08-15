using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        //[SerializeField] int sceneLoadTime = 2;
        [SerializeField] int loadMenu = 1;
        [SerializeField] int deadLoadingTime = 5;
        //[SerializeField] Portal portal;

        public void LoadNextScean()
        {
            StartCoroutine(GetNextScene());
        }

        public void LoadDeadScean()
        {
            StartCoroutine(Dead());
        }

        public void LaodCurrentScene()
        {
            LoadCurrentScean();
        }

        public void Quit()
        {
            Application.Quit();
        }

        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(loadMenu);
        }

        public IEnumerator LoadMenu()
        {
            DontDestroyOnLoad(gameObject);
            Pause();
            //portal.StartLevelTransition();
            SceneManager.LoadSceneAsync(0);
            yield return Pause();
            Destroy(gameObject);
        }

        public IEnumerator LoadCurrentScean()
        {
            Pause();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            return Pause();
        }

        public IEnumerator Dead()
        {
            yield return new WaitForSeconds(deadLoadingTime);
            SceneManager.LoadSceneAsync(0);
            yield return Pause();
        }

        private IEnumerator GetNextScene()
        {
            DontDestroyOnLoad(gameObject);
            Pause();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            yield return Pause();
            Destroy(gameObject);
        }
    }
}