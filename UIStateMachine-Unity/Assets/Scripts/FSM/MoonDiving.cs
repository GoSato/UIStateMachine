using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;

/// <summary>
/// 初期化用
/// </summary>
namespace MoonVR
{
    public class MoonDiving : MonoBehaviour
    {
        private void Start()
        {
            var mainMenuState = new MainMenuState();
            var stateMachine = new StateMachine(mainMenuState);
            StartCoroutine(stateMachine.Execute().GetEnumerator());
        }
    }
}