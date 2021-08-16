using System;
using HoweFramework.Fsm.Enums;
using HoweFramework.Variable;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机接口
    /// </summary>
    public interface IFsm : IVariableContainer<string>
    {
        /// <summary>
        /// 状态机的实例id(唯一id)
        /// </summary>
        int id { get; }
        
        /// <summary>
        /// 状态机运行情况
        /// </summary>
        FsmStatus status { get; }
        
        /// <summary>
        /// 状态机当前的状态
        /// </summary>
        IFsmState activeState { get; }

        /// <summary>
        /// 状态机当前是否是指定状态
        /// </summary>
        bool IsState(Type stateType);

        /// <summary>
        /// 状态机内是否包含指定状态
        /// </summary>
        bool HasState(Type stateType);
    }
}