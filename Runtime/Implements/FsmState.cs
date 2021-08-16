using System;
using HoweFramework.Fsm.Exceptions;

namespace HoweFramework.Fsm.Implements
{
    /// <summary>
    /// 有限状态机-状态基类
    /// </summary>
    public abstract class FsmState : IFsmState
    {
        private IFsm _fsm;
        private IFsmOperator _fsmOperator;

        protected bool isActive { get; private set; }

        public void Initialize(IFsm fsm)
        {
            _fsm = fsm;
            _fsmOperator = (IFsmOperator) fsm;
            OnInitialize(fsm);
        }

        public void Dispose()
        {
            OnDispose(_fsm);
            _fsm = null;
            _fsmOperator = null;
        }

        public void Enter(IFsm fsm)
        {
            isActive = true;
            OnEnter(fsm);
        }

        public void Leave(IFsm fsm)
        {
            isActive = false;
            OnLeave(fsm);
        }

        public void Update(IFsm fsm, float dt)
        {
            OnUpdate(fsm, dt);
        }

        /// <summary>
        /// 令所在的状态机切换到指定状态
        /// </summary>
        protected void ChangeState(Type stateType)
        {
            if (!isActive)
                throw new FsmStateIsNotActiveException();

            _fsmOperator.ChangeState(stateType);
        }

        /// <summary>
        /// 令所在的状态机切换到指定状态
        /// </summary>
        protected void ChangeState<T>() where T : class, IFsmState
        {
            ChangeState(typeof(T));
        }

        protected virtual void OnInitialize(IFsm fsm)
        {
        }

        protected virtual void OnDispose(IFsm fsm)
        {
        }

        protected virtual void OnEnter(IFsm fsm)
        {
        }

        protected virtual void OnLeave(IFsm fsm)
        {
        }

        protected virtual void OnUpdate(IFsm fsm, float dt)
        {
        }
    }
}