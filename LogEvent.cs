using TCEVENTLOGGERLib;

namespace TcEventLog.Wrapper
{
    public class LogEvent
    {
        /// <summary>
        /// 8 = Paramterfehler, 7 = Error, 6 = Warning, 5 = Instruktion, 4 = Information, 3 = Hinweis, 2 = Nachricht, 1 = Wartung, 0 = Unbekannt
        /// </summary>
        public enum EventClasses
        {
            Unbekannt = 0,
            Wartung = 1,
            Nachricht = 2,
            Hinweis = 3,
            Information = 4,
            Instruktion = 5,
            Warning = 6,
            Error = 7,
            Parameterfehler = 8
        }

        public int SrcId { get; }
        public int EventId { get; }
        public EventClasses EventClass { get; }
        public int MsConfirmed { get; }
        public int MsReset { get; }
        public int MustConfirm { get; }

        ITcEvent? _TtcEvent = null;

        internal LogEvent(ITcEvent tcEvent)
        {
            _TtcEvent = tcEvent;
            SrcId = tcEvent.SrcId;
            EventId = tcEvent.Id;
            EventClass = (EventClasses)tcEvent.Class;
            MsConfirmed = tcEvent.MsConfirmed;
            MsReset = tcEvent.MsReset;
            MustConfirm = tcEvent.MustConfirm;
        }

        public string? GetSourceName(int langId)
        {
            try
            {
                return _TtcEvent?.SourceName[langId];
            }
            catch
            {
                return string.Empty;
            }
        }

        public string? GetMsgstring(int langId)
        {
            try
            {
                return _TtcEvent?.GetMsgString(langId);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}