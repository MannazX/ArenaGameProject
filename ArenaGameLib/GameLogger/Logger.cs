using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameLogger
{
	public class Logger
	{
		public TraceSource TraceSource { get; set; }
		private static Logger _instanceLogger = new Logger();

		private Logger()
		{

		}

		public static Logger InitLogger()
		{
			return _instanceLogger;
		}

		public void StartLogger()
		{
			TraceSource = new TraceSource("Game Log", SourceLevels.All);
			TraceSource.Switch = new SourceSwitch("Log", SourceLevels.All.ToString());
			TraceSource.Listeners.Add(new ConsoleTraceListener());
			TraceListener textListener = new TextWriterTraceListener("gameLog.txt");
			textListener.Filter = new EventTypeFilter(SourceLevels.Critical);
			TraceSource.Listeners.Add(textListener);
			TraceSource.Listeners.Add(new XmlWriterTraceListener("gameLog.xml"));
		}

		public void AddNewListener(TraceListener listener)
		{
			TraceSource.Listeners.Add(listener);
		}

		public void WriteInfo(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Information, 1, logEntry);
			TraceSource.Flush();
		}

		public void LogWarning(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Warning, 2, logEntry);
			TraceSource.Flush();
		}

		public void LogError(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Error, 3, logEntry);
			TraceSource.Flush();
		}

		public void LogCritical(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Critical, 4, logEntry);
			TraceSource.Flush();
		}

		public void CloseLogger()
		{
			TraceSource.Close();
		}
	}
}
