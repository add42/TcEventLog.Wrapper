namespace TcEventLog.Wrapper
{
    public class TcEventLogWrapper : IDisposable
    {
        TCEVENTLOGGERLib.TcEventLog? _TcLogger = null;

        public event Action<LogEvent>? OnNewLogEvent;

        public void ConnectLocalPC()
        {
            _TcLogger = new TCEVENTLOGGERLib.TcEventLog();
            _TcLogger.OnNewEvent += M_logger_OnNewEvent;
            _TcLogger.OnConfirmEvent += M_logger_OnConfirmEvent;
            _TcLogger.OnResetEvent += M_logger_OnResetEvent;

            foreach (TCEVENTLOGGERLib.TcEvent evt in _TcLogger.EnumActiveEventsEx())
            {
                RaiseLogEvent(evt);
            }
        }

        public void Dispose()
        {
            if (_TcLogger != null)
            {
                _TcLogger.OnNewEvent -= M_logger_OnNewEvent;
                _TcLogger.OnConfirmEvent -= M_logger_OnConfirmEvent;
                _TcLogger.OnResetEvent -= M_logger_OnResetEvent;
                _TcLogger = null;
            }
        }

        void RaiseLogEvent(object evt)
        {
            var tce = evt as TCEVENTLOGGERLib.ITcEvent;
            if (tce != null)
            {
                OnNewLogEvent?.Invoke(new LogEvent(tce));
            }
        }

        private void M_logger_OnResetEvent(object evtObj)
        {
            RaiseLogEvent(evtObj);
        }

        private void M_logger_OnConfirmEvent(object evtObj)
        {
            RaiseLogEvent(evtObj);
        }
        private void M_logger_OnNewEvent(object evtObj)
        {
            RaiseLogEvent(evtObj);
        }
    }
}