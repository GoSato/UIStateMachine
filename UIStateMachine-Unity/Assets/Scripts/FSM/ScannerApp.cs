using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using Utility;
using UnityEngine.UI;

/// <summary>
/// 初期化用
/// </summary>
namespace STYLY.ScannerApp
{
    public class ScannerApp : SingletonMonoBehaviour<ScannerApp>
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

        private void Start()
        {
            var mainMenuState = new LoginPage();
            var stateMachine = new StateMachine(mainMenuState);
            StartCoroutine(stateMachine.Execute().GetEnumerator());
        }
    }
}