using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGameLib.GameLogger
{
	/// <summary>
	/// Singleton class for logging creature actions - Dealing damage, taking damage and looting items.
	/// </summary>
	public class Logger
	{
		public TraceSource TraceSource { get; private set; }
		private static Logger _instanceLogger = new Logger();

		private Logger()
		{

		}

		/// <summary>
		/// Method for instanciating the logger.
		/// </summary>
		/// <returns>Type: Logger - Instance of Logger</returns>
		public static Logger InitLogger()
		{
			return _instanceLogger;
		}

		/// <summary>
		/// Method for starting the logger.
		/// </summary>
		public void StartLogger()
		{
			TraceSource = new TraceSource("GameLog", SourceLevels.All);
			TraceSource.Switch = new SourceSwitch("Log", SourceLevels.All.ToString());
			TraceSource.Listeners.Add(new ConsoleTraceListener());
			TraceListener textListener = new TextWriterTraceListener("gameLog.txt");
			textListener.Filter = new EventTypeFilter(SourceLevels.Critical);
			TraceSource.Listeners.Add(textListener);
			TraceSource.Listeners.Add(new XmlWriterTraceListener("gameLog.xml"));
		}

		/// <summary>
		/// Method for adding new trace listeners to the Trace Source.
		/// </summary>
		/// <param name="listener">Type: TraceListener - New trace listener to add to the trace source</param>
		public void AddNewListener(TraceListener listener)
		{
			TraceSource.Listeners.Add(listener);
		}

		/// <summary>
		/// Method for writing information to the log.
		/// </summary>
		/// <param name="logEntry">Type: string - Information to write to the log</param>
		public void WriteInfo(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Information, 1, logEntry);
			TraceSource.Flush();
		}

		/// <summary>
		/// Method for writing warnings to the log.
		/// </summary>
		/// <param name="logEntry">Type: string - Warning to write to the log</param>
		public void LogWarning(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Warning, 2, logEntry);
			TraceSource.Flush();
		}

		/// <summary>
		/// Method for writing errors to the log.
		/// </summary>
		/// <param name="logEntry">Type: string - Error to write to the log</param>
		public void LogError(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Error, 3, logEntry);
			TraceSource.Flush();
		}

		/// <summary>
		/// Method for writing critical to the log.
		/// </summary>
		/// <param name="logEntry">Type: string - Critical to write to the log</param>
		public void LogCritical(string logEntry)
		{
			TraceSource.TraceEvent(TraceEventType.Critical, 4, logEntry);
			TraceSource.Flush();
		}

		/// <summary>
		/// Method for closing the logger
		/// </summary>
		public void CloseLogger()
		{
			TraceSource.Close();
		}
	}
}
