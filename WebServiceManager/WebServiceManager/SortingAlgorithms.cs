using System;

namespace WebServiceManagementSystem
{
    public static class SortingAlgorithms
    {
        public static void BubbleSort<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (comparison(array[j], array[j + 1]) > 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public static void QuickSort<T>(T[] array, int low, int high, Comparison<T> comparison)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high, comparison);
                QuickSort(array, low, pi - 1, comparison);
                QuickSort(array, pi + 1, high, comparison);
            }
        }

        private static int Partition<T>(T[] array, int low, int high, Comparison<T> comparison)
        {
            T pivot = array[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (comparison(array[j], pivot) < 0)
                {
                    i++;
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            T temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            return i + 1;
        }

        public static void MergeSort<T>(T[] array, Comparison<T> comparison)
        {
            if (array.Length <= 1)
                return;

            T[] left = new T[array.Length / 2];
            T[] right = new T[array.Length - left.Length];

            Array.Copy(array, 0, left, 0, left.Length);
            Array.Copy(array, left.Length, right, 0, right.Length);

            MergeSort(left, comparison);
            MergeSort(right, comparison);
            Merge(array, left, right, comparison);
        }

        private static void Merge<T>(T[] array, T[] left, T[] right, Comparison<T> comparison)
        {
            int leftIndex = 0, rightIndex = 0, targetIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    array[targetIndex++] = left[leftIndex++];
                }
                else
                {
                    array[targetIndex++] = right[rightIndex++];
                }
            }

            while (leftIndex < left.Length)
            {
                array[targetIndex++] = left[leftIndex++];
            }

            while (rightIndex < right.Length)
            {
                array[targetIndex++] = right[rightIndex++];
            }
        }
    }
}