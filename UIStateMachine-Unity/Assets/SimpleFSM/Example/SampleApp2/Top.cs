using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFSM;
using System;

public class Top : IState
{
    public void GoNextState()
    {
        var nextState = new Home();
        var transition = new PolygonDestructionTransition(3.0f);
        var eventArgs = new StateBeginExitEventArgs(nextState, transition);
        OnBeginExit(this, eventArgs);
    }

    #region IState
    public event EventHandler<StateBeginExitEventArgs> OnBeginExit;

    public void BeginEnter()
    {
        SampleApp2.Instance.SplachView.SetActive(false);
        SampleApp2.Instance.TopView.SetActive(true);
    }

    public void EndEnter()
    {
    }

    public void EndExit()
    {
        SampleApp2.Instance.TopView.SetActive(false);
    }

    public IEnumerable Execute()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoNextState();
            }

            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Ended)
                {
                    GoNextState();
                }
            }
            yield return null;
        }
    }
    #endregion IState
}
