namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机接口
    /// </summary>
    public interface IFsm
    {
        /// <summary>
        /// 切换状态
        /// </summary>
        void SwitchState<T>() where T : class, IFsmState;

        /// <summary>
        /// 设置状态机的数据 以键值对的形式存在该状态机内
        /// </summary>
        void SetData<T>(string key);

        /// <summary>
        /// 获取该状态机内的数据 若该键值无法索引到指定类型的对象，则返回默认值
        /// </summary>
        T GetData<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 移除该状态机内的指定数据，并返回该数据，若该键值无法索引到指定类型的对象，则返回默认值
        /// </summary>
        T RemoveData<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 移除该状态机内的指定数据(不对值类型进行校验)
        /// </summary>
        void RemoveData(string key);

        /// <summary>
        /// 移除该状态机内的所有数据
        /// </summary>
        void RemoveAllData();
    }
}