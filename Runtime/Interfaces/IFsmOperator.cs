using System;
using HoweFramework.Base;

namespace HoweFramework.Fsm
{
    internal interface IFsmOperator : IUpdate, IDisposable
    {
        void Initialize(int id, IFsmState[] states);

        void Start<T>() where T : class, IFsmState;

        void ChangeState<T>() where T : class, IFsmState;

        void Stop();
    }
}