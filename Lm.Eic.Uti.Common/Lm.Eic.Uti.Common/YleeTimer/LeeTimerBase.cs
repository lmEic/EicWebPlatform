using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Lm.Eic.Uti.Common.YleeTimer
{
   /// <summary>
   /// 计时器基类
   /// </summary>
   public abstract class LeeTimerBase
    {
       protected Timer leeTimer;
       /// <summary>
       /// 计时器监控句柄
       /// </summary>
       protected abstract void TimerWatcherHandler();
       /// <summary>
       /// 初始化计时器
       /// </summary>
       /// <param name="interval"></param>
       /// <param name="enabled"></param>
       protected void InitTimer(int interval,bool enabled=false)
       {
           this.leeTimer = new Timer() { AutoReset = true, Interval = interval, Enabled = enabled };
           this.leeTimer.Elapsed += (s, e) =>
           {
               leeTimer.Stop();
               TimerWatcherHandler();
               leeTimer.Start();
           };
       }
       /// <summary>
       /// 启动计时器
       /// </summary>
       public virtual void Start()
       {
           this.leeTimer.Enabled = true;
           this.leeTimer.Start();
       }
       /// <summary>
       /// 停止计时器
       /// </summary>
       public virtual void Stop()
       {
           this.leeTimer.Enabled = false;
           this.leeTimer.Stop();
       }
    }
}
