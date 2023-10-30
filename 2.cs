using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7laba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер матрицы: ");
            int size = Convert.ToInt32(Console.ReadLine());

            int[,] adjacencyMatrix = GenerateAdjacencyMatrix(size);

            Console.WriteLine("Матрица смежности для графа G1:");
            PrintMatrix(adjacencyMatrix);

            //создается массив visited, который будет использоваться для отслеживания посещенных вершин
            bool[] visited = new bool[size];

            Console.WriteLine("Обход в глубину:");

            //происходит обход графа в глубину. Для каждой вершины графа, если она еще не посещена, проверяется, является ли она изолированной (не имеет ребер с другими вершинами).
            //Если вершина изолированная, вызывается метод DepthFirstSearchIsolatedVertex для ее обхода. В противном случае вызывается метод DepthFirstSearchNonRecursive для обхода графа.
            for (int startVertexIndex = 0; startVertexIndex < size; startVertexIndex++)
            {
                if (!visited[startVertexIndex])
                {
                    if (IsIsolatedVertex(startVertexIndex, adjacencyMatrix))
                    {
                        Console.WriteLine("Вершина №" + (startVertexIndex + 1) + " не имеет ребро с другими вершинами");
                        DepthFirstSearchIsolatedVertex(startVertexIndex);
                    }
                    else
                    {
                        DepthFirstSearchNonRecursive(startVertexIndex, adjacencyMatrix, visited);
                    }
                }
            }
        }
        //Метод DepthFirstSearchNonRecursive осуществляет обход графа в глубину с использованием стека.
        private static void DepthFirstSearchNonRecursive(int startVertexIndex, int[,] adjacencyMatrix, bool[] visited)
        {
            //Начиная с заданной вершины, он помещает ее в стек
            Stack<int> stack = new Stack<int>();
            stack.Push(startVertexIndex);

            //затем до тех пор, пока стек не пуст, извлекает текущую вершину из стека. 
            while (stack.Count > 0)
            {
                int currentVertexIndex = stack.Pop();
                //Если эта вершина еще не посещена, она отмечается как посещенная и выводится на экран. Затем происходит проверка всех смежных вершин текущей вершины и,
                //если они еще не посещены, они добавляются в стек.
                if (!visited[currentVertexIndex])
                {
                    Console.WriteLine("Посещена вершина №" + (currentVertexIndex + 1));
                    visited[currentVertexIndex] = true;

                    for (int i = 0; i < adjacencyMatrix.GetLength(1); i++)
                    {
                        if (adjacencyMatrix[currentVertexIndex, i] == 1 && !visited[i])
                        {
                            stack.Push(i);
                        }
                    }
                }
            }
        }
        //Метод GenerateAdjacencyMatrix генерирует случайную матрицу смежности для графа
        private static int[,] GenerateAdjacencyMatrix(int size)
        {
            Random r = new Random();

            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        //для каждой пары вершин (i, j) генерируется случайное число 0 или 1, которое указывает наличие или отсутствие ребра между вершинами.
                        matrix[i, j] = r.Next(2);
                        matrix[j, i] = matrix[i, j];
                    }
                }
            }
            return matrix;
        }
        //Метод PrintMatrix выводит матрицу смежности на экран.
        static void PrintMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //Метод DepthFirstSearchIsolatedVertex выводит сообщение о посещении изолированной вершины.
        static void DepthFirstSearchIsolatedVertex(int vertex)
        {
            Console.WriteLine("Посещена изолированная вершина №" + (vertex + 1));
        }

        //Метод IsIsolatedVertex проверяет, является ли заданная вершина изолированной, путем проверки наличия ребер с другими вершинами в матрице смежности.
        static bool IsIsolatedVertex(int vertex, int[,] adjacencyMatrix)
        {
            for (int i = 0; i < adjacencyMatrix.GetLength(1); i++)
            {
                if (adjacencyMatrix[vertex, i] == 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
