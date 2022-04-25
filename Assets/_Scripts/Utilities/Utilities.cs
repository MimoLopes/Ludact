using UnityEngine;

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

        public static float RandomAngle()
        {
            return Random.Range(-Mathf.PI, Mathf.PI);
        }

        public static Vector3 RandomPosition(float maxdistance)
        {
            float x, y, z;

            x = Random.Range(-maxdistance, maxdistance);
            y = Random.Range(-maxdistance, maxdistance);
            z = Random.Range(0, maxdistance*2);

            return new Vector3(x, y, z);
        }
    }
}
