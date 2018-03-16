using System;
using System.IO;

namespace ConsoleEssentials
{
    /// <summary>
    /// Logging for console and text files.
    /// </summary>
    public static class Log
    {
        #region Properties

        private static bool m_LogToConsole = true;
        private static bool m_LogToFile = true;
        private static readonly string m_DefaultLogPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Log");
        private static string m_LogFileName = string.Empty;
        private static string m_LogPath = m_DefaultLogPath;

        /// <summary>
        /// Set the log to text file path to a different one than the one that gets created next to the executeable.
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_LogPath))
                    m_LogPath = m_DefaultLogPath;
                return m_LogPath;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    m_LogPath = value;
            }
        }

        /// <summary>
        /// Turn logging to text files on/off
        /// </summary>
        /// <param name="value"></param>
        public static void SetLogToFile(bool value)
        {
            m_LogToFile = value;
        }

        /// <summary>
        /// Turn logging to the console on/off
        /// </summary>
        /// <param name="value"></param>
        public static void SetLogToConsole(bool value)
        {
            m_LogToConsole = value;
        }

        private static string m_LogDateTime;
        /// <summary>
        /// Gets the date time of the log in a file valid format.
        /// </summary>
        public static string LogDateTime
        {
            get
            {
                if (!string.IsNullOrEmpty(m_LogDateTime))
                    return m_LogDateTime;
                DateTime now = DateTime.Now;
                m_LogDateTime = $"{now.Year}{now.Month.ToString().PadLeft(2, '0')}{now.Day.ToString().PadLeft(2, '0')}_{now.Hour.ToString().PadLeft(2, '0')}{now.Minute.ToString().PadLeft(2, '0')}{now.Second.ToString().PadLeft(2, '0')}";
                return m_LogDateTime;
            }
        }

        #endregion

        private static void WriteLog(string message, string type)
        {
            string logMessage = $"{DateTime.Now} - {type} - {message}";
            if (m_LogToConsole)
                Console.WriteLine(logMessage);

            if (!m_LogToFile)
                return;

            if (!Directory.Exists(m_LogPath))
                Directory.CreateDirectory(m_LogPath);
            if (string.IsNullOrEmpty(m_LogFileName))
            {
                m_LogFileName = $"Log{LogDateTime}.txt";
            }
            using (StreamWriter sw = File.AppendText(Path.Combine(m_LogPath, m_LogFileName)))
            {
                sw.WriteLine(logMessage);
            }
        }

        #region Information

        /// <summary>
        /// Log an information message (INFO)
        /// </summary>
        /// <param name="message"></param>
        public static void Information(string message)
        {
            WriteLog(message, "INFO ");
        }

        /// <summary>
        /// Log an exception as an information message (INFO)
        /// </summary>
        /// <param name="ex"></param>
        public static void Information(Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Information($"{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Information($"{ex.Message}{ex.StackTrace}");
#endif
        }

        /// <summary>
        /// Log both a message and an exception as an information message (INFO)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Information(string message, Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Information($"{message}{Environment.NewLine}{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Information($"{message}{Environment.NewLine}{ex.Message}{ex.StackTrace}");
#endif
        }

        #endregion

        #region Warning

        /// <summary>
        /// Log a warning message (WARN)
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLog(message, "WARN ");
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Log an exception as a warning message (WARN)
        /// </summary>
        /// <param name="ex"></param>
        public static void Warning(Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Warning($"{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Warning($"{ex.Message}{ex.StackTrace}{Environment.NewLine}");
#endif
        }

        /// <summary>
        /// Log both a message and an exception as a warning message (WARN)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Warning(string message, Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Warning($"{message}{Environment.NewLine}{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Warning($"{message}{Environment.NewLine}{ex.Message}{ex.StackTrace}");
#endif
        }

        #endregion

        #region Error

        /// <summary>
        /// Log an error message (ERROR)
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLog(message, "ERROR");
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Log an exception as an error message (ERROR)
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Error($"{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Error($"{ex.Message}{ex.StackTrace}");
#endif
        }

        /// <summary>
        /// Log both a message and an exception as an error message (ERROR)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Error(string message, Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Error($"{message}{Environment.NewLine}{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Error($"{message}{Environment.NewLine}{ex.Message}{ex.StackTrace}");
#endif
        }

        #endregion

        #region Critical

        /// <summary>
        /// Log a critical message (CRIT)
        /// </summary>
        /// <param name="message"></param>
        public static void Critical(string message)
        {
            ConsoleColor defaultForegroundColor = Console.ForegroundColor;
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            WriteLog(message, "CRIT ");
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }

        /// <summary>
        /// Log an exception as a critical message (CRIT)
        /// </summary>
        /// <param name="ex"></param>
        public static void Critical(Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Critical($"{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Critical($"{ex.Message}{ex.StackTrace}");
#endif
        }

        /// <summary>
        /// Log both a message and an exception as a critical message (CRIT)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Critical(string message, Exception ex)
        {
#if NET4_5 || NET4_5_1 || NET4_5_2 || NET4_6 || NET4_6_1
            Critical($"{message}{Environment.NewLine}{ex.Message}[{ex.HResult}]{ex.StackTrace}");
#else
            Critical($"{message}{Environment.NewLine}{ex.Message}{ex.StackTrace}");
#endif
        }

        #endregion
    }
}
