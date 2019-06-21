using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public class StateMachine : IStateMachine
    {
        private IState state;
        private IState nextState;
        private IStateTransition transition;

        public StateMachine(IState initialState)
        {
            State = initialState;
            state.EndEnter();
        }

        // 実行可能なIEnumerableを返す
        public IEnumerable Execute()
        {
            // ずっとぶん回す
            while (true)
            {
                for(var e = state.Execute().GetEnumerator(); transition == null && e.MoveNext();)
                {
                    yield return e.Current;
                }

                while(transition == null)
                {
                    yield return null;
                }

                state.OnBeginExit -= HandleStateBeginExit;

                if(nextState == null)
                {
                    break;
                }

                // いまのステートが終わる時の演出
                foreach(var e in transition.Exit())
                {
                    yield return e;
                }
                state.EndExit();

                // ステートの切り替え
                // BeginEnter()呼び出し
                State = nextState;
                nextState = null;

                // 新しいステートが始まるときの演出
                foreach(var e in transition.Enter())
                {
                    yield return e;
                }
                state.EndEnter();
                transition = null;
            }
        }

        private IState State
        {
            set
            {
                state = value;
                state.OnBeginExit += HandleStateBeginExit;
                state.BeginEnter();
            }
        }

        private void HandleStateBeginExit(object sender, StateBeginExitEventArgs e)
        {
            nextState = e.NextState;
            transition = e.Transition;
        }
    }
}