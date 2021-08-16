using System;
using HoweFramework.Base;

namespace HoweFramework.Fsm
{
    internal interface IFsmOperator : IUpdate, IDisposable
    {
        void Initialize(int id, IFsmState[] states);

        void Start(Type stateType);

        void ChangeState(Type stateType);

        void Stop();
    }
}