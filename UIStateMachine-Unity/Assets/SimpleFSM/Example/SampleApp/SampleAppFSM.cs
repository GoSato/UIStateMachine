using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using Utility;
using UnityEngine.UI;

/// <summary>
/// 初期化用
/// </summary>
namespace SampleApp
{
    public class SampleAppFSM : SingletonMonoBehaviour<SampleAppFSM>
    {
        [SerializeField]
        private GameObject _loginPage;
        public GameObject LoginPage { get { return _loginPage; } }

        [SerializeField]
        private GameObject _privacyPage;
        public GameObject PrivacyPage { get { return _privacyPage; } }

        [SerializeField]
        private Button _backButton;
        public Button BackButton { get { return _backButton; } }

        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeMethodLoad()
        {
            Screen.SetResolution(1920, 1080, false);

        }

        private void Start()
        {
            var mainMenuState = new LoginPage();
            var stateMachine = new StateMachine(mainMenuState);
            StartCoroutine(stateMachine.Execute().GetEnumerator());
        }
    }
}