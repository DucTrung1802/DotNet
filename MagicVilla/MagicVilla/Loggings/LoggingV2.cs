namespace MagicVilla.Loggings
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - " + message);
            }
            else if (type == "warning")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("WARNING - " + message);

            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(message);
            }
        }
    }
}
