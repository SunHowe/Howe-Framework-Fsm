using HoweFramework.Variable;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机接口
    /// </summary>
    public interface IFsm : IVariableContainer<string>
    {
        /// <summary>
        /// 状态机是否在运行中
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 状态机当前是否是指定状态
        /// </summary>
        bool IsState<T>() where T : class, IFsmState;
    }
}