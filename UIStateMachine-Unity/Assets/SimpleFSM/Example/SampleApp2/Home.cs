using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using System;

public class Home : IState
{
    private void GoNextState()
    {
        var nextState = new Top();
        var transition = new ScreenFadeTransition(1.0f);
        var eventArgs = new StateBeginExitEventArgs(nextState, transition);
        OnBeginExit(this, eventArgs);
    }

    #region IState
    public event EventHandler<StateBeginExitEventArgs> OnBeginExit;

    public void BeginEnter()
    {
        SampleApp2.Instance.HomeView.SetActive(true);
    }

    public void EndEnter()
    {

    }

    public void EndExit()
    {
        SampleApp2.Instance.HomeView.SetActive(false);
    }

    public IEnumerable Execute()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GoNextState();
            }
            yield return null;
        }
    }
    #endregion IState
}
