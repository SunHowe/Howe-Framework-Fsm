using System;
using System.Collections.Generic;
using HoweFramework.Fsm.Enums;
using HoweFramework.Fsm.Exceptions;
using HoweFramework.Pool;
using HoweFramework.Variable;

namespace HoweFramework.Fsm.Implements
{
    /// <summary>
    /// 优先状态机实现类
    /// </summary>
    internal class Fsm : IFsm, IFsmOperator, IReset
    {
        private readonly IVariableContainer<string> _container = new VariableContainer<string>();
        private readonly Dictionary<Type, IFsmState> _fsmStates = new Dictionary<Type, IFsmState>();

        public int id { get; private set; }
        public FsmStatus status { get; private set; } = FsmStatus.Disposed;
        public IFsmState activeState { get; private set; }


        public void Initialize(int id, IFsmState[] states)
        {
            if (status != FsmStatus.Disposed)
                throw new FsmAlreadyInitializedException();

            if (states.Length == 0)
                throw new FsmStatesEmptyException();

            status = FsmStatus.Idle;
            this.id = id;
            foreach (var state in states)
            {
                _fsmStates.Add(state.GetType(), state);
                state.Initialize(this);
            }
        }

        public void Dispose()
        {
            switch (status)
            {
                case FsmStatus.Disposed:
                    throw new FsmAlreadyDisposedException();
                case FsmStatus.Running:
                    InnerStop();
                    break;
            }

            foreach (var state in _fsmStates.Values)
                state.Dispose();

            _fsmStates.Clear();

            status = FsmStatus.Disposed;
        }

        public void Reset()
        {
            if (status != FsmStatus.Disposed)
                Dispose();

            id = -1;
        }

        public bool IsState(Type stateType)
        {
            if (status != FsmStatus.Running)
                return false;

            return activeState.GetType() == stateType;
        }

        public bool HasState(Type stateType)
        {
            return _fsmStates.ContainsKey(stateType);
        }

        public void Start(Type stateType)
        {
            if (status == FsmStatus.Running)
                throw new FsmAlreadyRunningException();

            if (!_fsmStates.TryGetValue(stateType, out var state))
                throw new FsmStateNotFoundException();

            status = FsmStatus.Running;
            InnerStart(state);
        }

        public void ChangeState(Type stateType)
        {
            if (status != FsmStatus.Running)
                throw new FsmNotRunningException();

            if (!_fsmStates.TryGetValue(stateType, out var state))
                throw new FsmStateNotFoundException();
            
            if (state == activeState)
                return;

            InnerStop();
            InnerStart(state);
        }

        public void Stop()
        {
            if (status != FsmStatus.Running)
                throw new FsmNotRunningException();

            InnerStop();
            status = FsmStatus.Idle;
        }

        public void Update(float dt)
        {
            if (status != FsmStatus.Running)
                throw new FsmNotRunningException();

            activeState.Update(this, dt);
        }

        #region [Variable Container]

        public void SetData<TValue>(string key, TValue data)
        {
            _container.SetData(key, data);
        }

        public TValue GetData<TValue>(string key, TValue defaultValue = default)
        {
            return _container.GetData(key, defaultValue);
        }

        public TValue RemoveData<TValue>(string key, TValue defaultValue = default)
        {
            return _container.RemoveData(key, defaultValue);
        }

        public void RemoveData(string key)
        {
            _container.RemoveData(key);
        }

        public void RemoveAllData()
        {
            _container.RemoveAllData();
        }

        #endregion

        #region [Implements]

        private void InnerStart(IFsmState state)
        {
            activeState = state;
            activeState.Enter(this);
        }

        private void InnerStop()
        {
            activeState.Leave(this);
            activeState = null;
        }

        #endregion
    }
}