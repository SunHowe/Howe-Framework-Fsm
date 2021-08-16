namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机拓展类
    /// </summary>
    public static class FsmExtensions
    {
        /// <summary>
        /// 状态机当前是否是指定状态
        /// </summary>
        public static bool IsState<T>(this IFsm fsm) where T : class, IFsmState
        {
            return fsm.IsState(typeof(T));
        }

        /// <summary>
        /// 状态机内是否包含指定状态
        /// </summary>
        public static bool HasState<T>(this IFsm fsm) where T : class, IFsmState
        {
            return fsm.HasState(typeof(T));
        }
    }
}