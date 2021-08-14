namespace HoweFramework.Fsm.Enums
{
    /// <summary>
    /// 状态机-运行状态定义
    /// </summary>
    public enum FsmStatus
    {
        /// <summary>
        /// 被销毁、未被初始化的状态机
        /// </summary>
        Disposed,

        /// <summary>
        /// 已初始化但未在运行中
        /// </summary>
        Idle,

        /// <summary>
        /// 运行中
        /// </summary>
        Running,
    }
}