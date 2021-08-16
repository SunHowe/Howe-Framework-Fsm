using System;
using System.Collections.Generic;
using HoweFramework.Base;
using HoweFramework.Fsm.Enums;
using HoweFramework.Pool;
using HoweFramework.Utilities;

namespace HoweFramework.Fsm.Implements
{
    /// <summary>
    /// 有限状态机服务实现类
    /// </summary>
    public class FsmServiceImpl : IFsmService, IUpdate
    {
        private readonly Dictionary<int, Fsm> _fsmDict = new Dictionary<int, Fsm>();
        private readonly IdentifierGenerator _generator = new IdentifierGenerator();
        private readonly List<Fsm> _updates = new List<Fsm>();
        private readonly List<Fsm> _destroys = new List<Fsm>();

        public void Initialize()
        {
            _generator.Reset();
        }

        public void Update(float dt)
        {
            for (var index = 0; index < _updates.Count; index++)
            {
                var fsm = _updates[index];
                if (fsm.status != FsmStatus.Running)
                {
                    _updates.RemoveAt(index--);
                    continue;
                }

                fsm.Update(dt);
            }
        }

        public void Dispose()
        {
            _generator.Dispose();

            foreach (var fsm in _fsmDict.Values)
                fsm.Dispose();

            _fsmDict.Clear();
        }

        public IFsm Get(int id)
        {
            return GetFsm(id);
        }

        public IFsm Spawn(IFsmState[] states)
        {
            var fsm = PoolService.Acquire<Fsm>();

            fsm.Initialize(_generator.Spawn(), states);
            _fsmDict.Add(fsm.id, fsm);

            return fsm;
        }

        public void Destroy(IFsm fsm)
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return;

            fsmOperator.Dispose();
            _destroys.Add(fsmOperator);
            _fsmDict.Remove(fsm.id);
        }

        public void Destroy(int id)
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return;

            fsmOperator.Dispose();
            _destroys.Add(fsmOperator);
            _fsmDict.Remove(id);
        }

        public void Start(IFsm fsm, Type stateType)
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return;

            fsmOperator.Start(stateType);
            _updates.Add(fsmOperator);
        }

        public void Start(int id, Type stateType)
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return;

            fsmOperator.Start(stateType);
            _updates.Add(fsmOperator);
        }

        public IUpdate StartManualDrive(IFsm fsm, Type stateType)
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return null;

            fsmOperator.Start(stateType);

            return fsmOperator;
        }

        public IUpdate StartManualDrive(int id, Type stateType)
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return null;

            fsmOperator.Start(stateType);

            return fsmOperator;
        }

        public void Stop(IFsm fsm)
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return;

            fsmOperator.Stop();
        }

        public void Stop(int id)
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return;

            fsmOperator.Stop();
        }

        private Fsm GetFsm(int id)
        {
            return _fsmDict.TryGetValue(id, out var fsm) ? fsm : null;
        }

        private Fsm GetFsm(IFsm fsm)
        {
            if (fsm == null)
                return null;

            var fsmInDict = GetFsm(fsm.id);
            return fsmInDict == fsm ? fsmInDict : null;
        }
    }
}