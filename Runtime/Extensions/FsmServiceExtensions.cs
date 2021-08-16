using HoweFramework.Base;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机服务拓展类
    /// </summary>
    public static class FsmServiceExtensions
    {
        /// <summary>
        /// 启动指定状态机(由状态机服务驱动状态机的帧更新)
        /// </summary>
        public static void Start<T>(this IFsmService fsmService, IFsm fsm) where T : class, IFsmState
        {
            fsmService.Start(fsm, typeof(T));
        }

        /// <summary>
        /// 启动指定状态机(由状态机服务驱动状态机的帧更新)
        /// </summary>
        public static void Start<T>(this IFsmService fsmService, int id) where T : class, IFsmState
        {
            fsmService.Start(id, typeof(T));
        }

        /// <summary>
        /// 启动指定状态机(自行驱动状态机的帧更新)
        /// </summary>
        public static IUpdate StartManualDrive<T>(this IFsmService fsmService, IFsm fsm) where T : class, IFsmState
        {
            return fsmService.StartManualDrive(fsm, typeof(T));
        }

        /// <summary>
        /// 启动指定状态机(自行驱动状态机的帧更新)
        /// </summary>
        public static IUpdate StartManualDrive<T>(this IFsmService fsmService, int id) where T : class, IFsmState
        {
            return fsmService.StartManualDrive(id, typeof(T));
        }
    }
}