using System;
using System.Collections.Generic;
using HoweFramework.Fsm.Exceptions;
using HoweFramework.Variable;

namespace HoweFramework.Fsm.Implements
{
    internal class Fsm : IFsm, IFsmOperator
    {
        private readonly IVariableContainer<string> _container = new VariableContainer<string>();
        private readonly Dictionary<Type, IFsmState> _fsmStates = new Dictionary<Type, IFsmState>();
        private IFsmState _activeState;

        public bool IsRunning => _activeState != null;
        
        public bool IsState<T>() where T : class, IFsmState
        {
            if (!IsRunning)
                return false;

            return _activeState.GetType() == typeof(T);
        }

        public void Start<T>() where T : class, IFsmState
        {
            if (IsRunning)
                throw new FsmAlreadyRunningException();
            
            InnerStart<T>();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Update(float dt)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(params IFsmState[] states)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeState<T>() where T : class, IFsmState
        {
            
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

        private void InnerStart<T>() where T : class, IFsmState
        {
            if (!_fsmStates.TryGetValue(typeof(T), out var state))
                throw new FsmStateNotFoundException();
            
            
        }

        private void InnerStop()
        {
            
        }

        #endregion
    }
}