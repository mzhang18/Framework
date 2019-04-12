using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Dow.SSD.Framework.Common
{
    /// <summary>
    /// Exception Handling interface class
    /// </summary>
    public static partial class ExceptionManager
    {
        #region Private Fields

        //private const string CustomStr = "EventID: {0}; CustomMessage: {1}";
        //private static Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionManager manager;

        #endregion

        #region Constructors
        static ExceptionManager()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            //IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            //LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            //ExceptionPolicyFactory factory = new ExceptionPolicyFactory(configurationSource);
            //Logger.SetLogWriter(logWriterFactory.Create());
            //manager = factory.CreateManager();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="customMessage"></param>
        /// <param name="policy"></param>
        public static void HandleException(Exception ex, string customMessage = "", string policy = "DefaultPolicy")
        {
            //Log Error into Database
            LogUntity.LogError(ex, customMessage);

            #region Log Error into Server Event Log
            /*
            string eventId = Guid.NewGuid().ToString();
            if (ex is CustomException)
            {
                var customEx = ex as CustomException;
                //
                customEx.CustomMessage = customMessage;
                if (string.IsNullOrEmpty(customEx.EventID))
                {
                    customEx.EventID = eventId;
                }
                else
                {
                    eventId = customEx.EventID;
                }
            }
            else
            {
                ex.HelpLink = string.Format(CustomStr, eventId, customMessage);
            }
            //
            Exception newException;
            bool rethrow = manager.HandleException(ex, policy, out newException);
            if (rethrow && null != newException && newException is CustomException)
            {
                (newException as CustomException).CustomMessage = customMessage;
                (newException as CustomException).EventID = eventId;
                //
                throw newException;
            }
            */
            #endregion
        }

        #endregion
    }
}