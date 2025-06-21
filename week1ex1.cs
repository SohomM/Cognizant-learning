using System;

namespace SingletonPatternExample
{
    // Singleton Logger class
    public class Logger
    {
        // Private static instance (eager initialization)
        private static readonly Logger instance = new Logger();

        // Private constructor prevents external instantiation
        private Logger()
        {
            Console.WriteLine("Logger instance created.");
        }

        // Public static method to access the singleton instance
        public static Logger GetInstance()
        {
            return instance;
        }

        // Logging method
        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }
    }

    // Program class to test the Singleton
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger1 = Logger.GetInstance();
            logger1.Log("This is the first log message.");

            Logger logger2 = Logger.GetInstance();
            logger2.Log("This is the second log message.");

            if (ReferenceEquals(logger1, logger2))
            {
                Console.WriteLine("Both logger instances are the same (singleton confirmed).");
            }
            else
            {
                Console.WriteLine("Logger instances are different (singleton violated).");
            }
        }
    }
}

