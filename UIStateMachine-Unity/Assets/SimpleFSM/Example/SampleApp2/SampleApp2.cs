using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using SimpleFSM.Utility;

public class SampleApp2 : SingletonMonoBehaviour<SampleApp2>
{
    [SerializeField]
    private GameObject _splashView;
    public GameObject SplachView { get { return _splashView; } }

    [SerializeField]
    private GameObject _TopView;
    public GameObject TopView { get { return _TopView; } }

    [SerializeField]
    private GameObject _homeView;
    public GameObject HomeView { get { return _homeView; } }

    private void Start()
    {
        var startState = new Home();
        var stateMachine = new StateMachine(startState);
        StartCoroutine(stateMachine.Execute().GetEnumerator());
    }
}
