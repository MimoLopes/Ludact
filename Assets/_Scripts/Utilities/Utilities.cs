namespace Utilities
{
    public static class Calc
    {
        public static int[] FibonacciSequence(int numberOfSeries)
        {
            int[] fibonacciNumbers = new int[numberOfSeries];
            fibonacciNumbers[0] = 0;
            fibonacciNumbers[1] = 1;

            for (int i = 2; i < numberOfSeries; i++)
            {
                fibonacciNumbers[i] = fibonacciNumbers[i - 2] + fibonacciNumbers[i - 1];
            }

            return fibonacciNumbers;
        }
    }
}
