using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFSM
{
    public interface IState
    {
        // 新しいステートに切り替え時に呼ばれる
        void BeginEnter();

        // 新しいステートに入り終わった時に呼ばれる
        void EndEnter();

        // 毎フレーム呼ばれる
        IEnumerable Execute();

        // 次のステートに遷移を始める、全ての始まり
        event EventHandler<StateBeginExitEventArgs> OnBeginExit;

        // このステートから出るときに呼ばれる、この後は新しいステート
        void EndExit();
    }
}