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
        /// 状态机当前运行状态
        /// </summary>
        FsmStatus status { get; }

        /// <summary>
        /// 状态机当前是否是指定状态
        /// </summary>
        bool IsState<T>() where T : class, IFsmState;
    }
}