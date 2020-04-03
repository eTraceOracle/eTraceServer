using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common.Log
{
    public class CustomLayout : log4net.Layout.PatternLayout
    {
        public CustomLayout()
        {
            this.AddConverter("SentOn", typeof(SentOnPatternConverter));
            this.AddConverter("IP", typeof(IPPatternConverter));
            this.AddConverter("Controller", typeof(ControllerPatternConverter));
            this.AddConverter("Action", typeof(ActionPatternConverter));
            this.AddConverter("HostName", typeof(HostNamePatternConverter));
            this.AddConverter("Comment", typeof(CommentPatternConverter));
            this.AddConverter("Milliseconds", typeof(MillisecondsPatternConverter));
        }
        internal sealed class CommentPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.Comment);
            }
        }
        internal sealed class HostNamePatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.HostName);
            }
        }
        internal sealed class ActionPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.Action);
            }
        }
        internal sealed class ControllerPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.Controller);
            }
        }
        internal sealed class IPPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.IP);
            }
        }
        internal sealed class SentOnPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.SentOn);
            }
        }
        internal sealed class MillisecondsPatternConverter : PatternLayoutConverter
        {
            override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
            {
                LogHelper.SQLLOGModel logMessage = loggingEvent.MessageObject as LogHelper.SQLLOGModel;

                if (logMessage != null)
                    writer.Write(logMessage.Milliseconds);
            }
        }
    }
}
