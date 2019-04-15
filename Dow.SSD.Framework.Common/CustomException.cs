using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Dow.SSD.Framework.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        public string CustomMessage { get; set; }

        public string EventID { get; set; }

        public CustomException()
            : base()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            CustomMessage = info.GetString("CustomMessage");
            EventID = info.GetString("EventID");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            //
            info.AddValue("CustomMessage", CustomMessage);
            info.AddValue("EventID", EventID);
        }
    }
}