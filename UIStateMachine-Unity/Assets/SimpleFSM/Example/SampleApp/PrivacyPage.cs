using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SampleApp
{
    /// <summary>
    /// メインメニューシーン用
    /// </summary>
    public class PrivacyPage : IState
    {
        private Button _loginButton;

        public void BeginEnter()
        {
            SampleAppFSM.Instance.PrivacyPage.SetActive(true);
            //_loginButton = GameObject.Find("Button_Login").GetComponent<Button>();
            //_loginButton.onClick.AddListener(HandleLoginButton);
        }

        public void EndEnter()
        {
            SampleAppFSM.Instance.BackButton.onClick.AddListener(HandleBackButton);
        }

        public IEnumerable Execute()
        {
            while (true)
            {

                yield return null;
            }
        }

        public event EventHandler<StateBeginExitEventArgs> OnBeginExit;

        public void EndExit()
        {
            SampleAppFSM.Instance.PrivacyPage.SetActive(false);
            SampleAppFSM.Instance.BackButton.onClick.RemoveListener(HandleBackButton);
        }

        private void HandleLoginButton()
        {
            var nextState = new PlayState();
            var transition = new ScreenFadeTransition(0.1f);
            var eventArgs = new StateBeginExitEventArgs(nextState, transition);
            OnBeginExit(this, eventArgs);
        }

        private void HandleBackButton()
        {
            var nextState = new LoginPage();
            var transition = new ScreenFadeTransition(0f);
            var eventArgs = new StateBeginExitEventArgs(nextState, transition);
            OnBeginExit(this, eventArgs);
        }
    }
}