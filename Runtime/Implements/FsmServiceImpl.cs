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
            _updates.Clear();
            _updates.AddRange(_fsmDict.Values);

            foreach (var fsm in _updates)
            {
                if (fsm.status != FsmStatus.Running)
                    continue;

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
            ;

            fsmOperator.Dispose();
            _destroys.Add(fsmOperator);
            _fsmDict.Remove(id);
        }

        public void Start<T>(IFsm fsm) where T : class, IFsmState
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return;

            fsmOperator.Start<T>();
        }

        public void Start<T>(int id) where T : class, IFsmState
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return;

            fsmOperator.Start<T>();
            ;
        }

        public IUpdate StartManualDrive<T>(IFsm fsm) where T : class, IFsmState
        {
            var fsmOperator = GetFsm(fsm);
            if (fsmOperator == null)
                return null;

            fsmOperator.Start<T>();

            return fsmOperator;
        }

        public IUpdate StartManualDrive<T>(int id) where T : class, IFsmState
        {
            var fsmOperator = GetFsm(id);
            if (fsmOperator == null)
                return null;

            fsmOperator.Start<T>();

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