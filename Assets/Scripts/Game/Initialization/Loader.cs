using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Initialization
{
    public class Loader : MonoBehaviour
    {
        static Loader s_Instance;

        public static Loader Instance
        {
            get => s_Instance;
        }

        [SerializeField] string m_MainSceneName;

        void Awake()
        {
            s_Instance = this;
        }

        public void LoadMainScene()
        {
            SceneManager.LoadSceneAsync(m_MainSceneName, LoadSceneMode.Single);
        }
    }
}
