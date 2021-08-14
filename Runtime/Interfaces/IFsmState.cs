using System;
using HoweFramework.Base;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机-状态接口
    /// </summary>
    public interface IFsmState : IInitialize<IFsm>, IDisposable
    {
        void Enter(IFsm fsm);

        void Leave(IFsm fsm);

        void Update(IFsm fsm, float dt);
    }
}