using System;

public interface ILogger
{
    void LogError(string message);
    void LogEvent(string message);
}

public class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Ошибка]: {message}");
        Console.ResetColor();
    }

    public void LogEvent(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[Событие]: {message}");
        Console.ResetColor();
    }
}

public class Calculator
{
    private readonly ILogger logger;

    public Calculator(ILogger logger)
    {
        this.logger = logger;
    }

    public double AddNumbers(double num1, double num2)
    {
        try
        {
            logger.LogEvent("Выполняется сложение чисел...");

            double result = num1 + num2;
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError($"Ошибка при сложении чисел: {ex.Message}");
            throw;
        }
        finally
        {
            logger.LogEvent("Сложение чисел завершено.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Введите первое число:");
        double num1 = ReadNumberFromConsole();

        Console.Write("Введите второе число:");
        double num2 = ReadNumberFromConsole();

        ILogger logger = new ConsoleLogger();
        Calculator calculator = new Calculator(logger);

        try
        {
            double result = calculator.AddNumbers(num1, num2);
            Console.WriteLine($"Результат сложения: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    private static double ReadNumberFromConsole()
    {
        double number;
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите число:");
        }
        return number;
    }
}