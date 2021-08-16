using System;
using HoweFramework.Base;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机服务
    /// </summary>
    public interface IFsmService : IService
    {
        /// <summary>
        /// 获取指定状态机实例
        /// </summary>
        IFsm Get(int id);

        /// <summary>
        /// 创建状态机
        /// </summary>
        IFsm Spawn(IFsmState[] states);

        /// <summary>
        /// 销毁指定状态机
        /// </summary>
        void Destroy(IFsm fsm);

        /// <summary>
        /// 销毁指定状态机
        /// </summary>
        void Destroy(int id);

        /// <summary>
        /// 启动指定状态机(由状态机服务驱动状态机的帧更新)
        /// </summary>
        void Start(IFsm fsm, Type stateType);

        /// <summary>
        /// 启动指定状态机(由状态机服务驱动状态机的帧更新)
        /// </summary>
        void Start(int id, Type stateType);

        /// <summary>
        /// 启动指定状态机(自行驱动状态机的帧更新)
        /// </summary>
        IUpdate StartManualDrive(IFsm fsm, Type stateType);

        /// <summary>
        /// 启动指定状态机(自行驱动状态机的帧更新)
        /// </summary>
        IUpdate StartManualDrive(int id, Type stateType);

        /// <summary>
        /// 停止指定状态机
        /// </summary>
        void Stop(IFsm fsm);

        /// <summary>
        /// 停止指定状态机
        /// </summary>
        void Stop(int id);
    }
}