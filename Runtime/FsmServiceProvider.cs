using CatLib;
using CatLib.Container;
using HoweFramework.Fsm.Implements;

namespace HoweFramework.Fsm
{
    /// <summary>
    /// 有限状态机服务提供者
    /// </summary>
    public class FsmServiceProvider : ServiceProvider
    {
        public override void Register()
        {
            App.Singleton<IFsmService, FsmServiceImpl>();
        }
    }
}