using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using System;
using UnityEngine.UI;

namespace SampleApp
{
    /// <summary>
    /// メインメニューシーン用
    /// </summary>
    public class LoginPage : IState
    {
        private Button _loginButton;

        public void BeginEnter()
        {
            SampleAppFSM.Instance.LoginPage.SetActive(true);
            SampleAppFSM.Instance.BackButton.gameObject.SetActive(false);
            _loginButton = GameObject.Find("Button_Login").GetComponent<Button>();
            _loginButton.onClick.AddListener(HandleLoginButton);
        }

        public void EndEnter()
        {

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
            SampleAppFSM.Instance.LoginPage.SetActive(false);
            SampleAppFSM.Instance.BackButton.gameObject.SetActive(true);
            _loginButton.onClick.RemoveListener(HandleLoginButton);
        }

        private void HandleLoginButton()
        {
            var nextState = new PrivacyPage();
            var transition = new ScreenFadeTransition(0f);
            var eventArgs = new StateBeginExitEventArgs(nextState, transition);
            OnBeginExit(this, eventArgs);
        }
    }
}