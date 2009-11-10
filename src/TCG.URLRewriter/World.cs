using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using TCG.Utils;
using TCG.Data;
using TCG.Entity;
using TCG.Handlers;

namespace TCG.URLRewriter
{
    /// <summary>
    /// 网站世界状态类，监控网站生命体运行是否正常
    /// </summary>
    public class World
    {
        /// <summary>
        /// 世界是否被创造
        /// </summary>
        public static bool IsWorldCreated
        {
            get
            {
                return World._isworldcreated;
            }
            set
            {
                World._isworldcreated = value;
            }
        }
        private static bool _isworldcreated; //世界是否被创造

   
        public static ConfigService configService
        {
            get
            {
                return World._configservice;
            }
            set
            {
                World._configservice = value;
            }
        }
        private static ConfigService _configservice;


        public static HandlerService handlerService
        {
            get
            {
                return World._handlerservice;
            }
            set
            {
                World._handlerservice = value;
            }
        }
        private static HandlerService _handlerservice; 

        /// <summary>
        /// 是否正在创造世界
        /// </summary>
        public static bool IsCreateWorlding
        {
            get
            {
                return World._iscreateworlding;
            }
            set
            {
                World._iscreateworlding = value;
            }
        }
        private static bool _iscreateworlding; //是否正在创造世界

        /// <summary>
        /// 创造世界
        /// </summary>
        public static void CreateWorld()
        {
            //表示世界创造正在进行中...
            World.IsCreateWorlding = true;

            Dictionary<string, EntityBase> res = World.handlerService.resourcsService.resourcesHandlers.GetAllResurces();

            //完成世界创造
            World.IsWorldCreated = true;
        }
    }
}
