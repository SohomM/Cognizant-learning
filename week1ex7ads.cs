using System;

namespace FinancialForecasting
{
    class Program
    {
        // Step 2 & 3: Recursive method to calculate future value
        static double PredictFutureValue(int year, double initialAmount, double growthRate)
        {
            if (year == 0)
                return initialAmount;
            return PredictFutureValue(year - 1, initialAmount, growthRate) * (1 + growthRate);
        }

        // Optional: Optimized version using memoization (Step 4)
        static double PredictFutureValueMemo(int year, double initialAmount, double growthRate, double[] memo)
        {
            if (year == 0)
                return initialAmount;
            if (memo[year] != 0)
                return memo[year];

            memo[year] = PredictFutureValueMemo(year - 1, initialAmount, growthRate, memo) * (1 + growthRate);
            return memo[year];
        }

        static void Main(string[] args)
        {
            double initialAmount = 1000;
            double annualGrowthRate = 0.05; // 5%
            int years = 10;

            Console.WriteLine("Recursive Forecast:");
            for (int i = 0; i <= years; i++)
            {
                double value = PredictFutureValue(i, initialAmount, annualGrowthRate);
                Console.WriteLine($"Year {i}: {value:F2}");
            }

            Console.WriteLine("\nOptimized Recursive Forecast with Memoization:");
            double[] memo = new double[years + 1];
            for (int i = 0; i <= years; i++)
            {
                double value = PredictFutureValueMemo(i, initialAmount, annualGrowthRate, memo);
                Console.WriteLine($"Year {i}: {value:F2}");
            }
        }
    }
}

