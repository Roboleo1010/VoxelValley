using log4net.Appender;
using log4net.Core;
using UnityEngine;

public class UnityAppender : AppenderSkeleton
{
    protected override void Append(LoggingEvent loggingEvent)
    {
        if (loggingEvent.Level == Level.Debug || loggingEvent.Level == Level.Info)
            Debug.Log(loggingEvent.RenderedMessage);
        else if (loggingEvent.Level == Level.Warn)
            Debug.LogWarning(loggingEvent.RenderedMessage);
        else if (loggingEvent.Level == Level.Error || loggingEvent.Level == Level.Fatal)
            Debug.LogError(loggingEvent.RenderedMessage);
        else
            Debug.Log(loggingEvent.RenderedMessage);
    }
}
