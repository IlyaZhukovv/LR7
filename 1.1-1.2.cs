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
                        DepthFirstSearch(startVertexIndex, adjacencyMatrix, visited);
                    }
                }
            }
        }
        //Метод GenerateAdjacencyMatrix генерирует случайную матрицу смежности для графа.
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
        //Данный код реализует алгоритм обхода графа в глубину
        static void DepthFirstSearch(int startVertexIndex, int[,] adjacencyMatrix, bool[] visited)
        {
            //Вначале функция выводит на экран информацию о посещении стартовой вершины.
            Console.WriteLine("Посещена вершина №" + (startVertexIndex + 1));

            //Затем она помечает данную вершину как посещенную, устанавливая соответствующий элемент в массиве visited в значение true.
            visited[startVertexIndex] = true;

            //Затем происходит цикл, который перебирает все вершины, смежные со стартовой вершиной.
            //Если в матрице смежности значение равно 1 (т.е. вершины связаны) и данная вершина еще не была посещена (значение в массиве visited равно false),
            //то вызывается рекурсивно функция DepthFirstSearch для данной вершины.
            for (int i = 0; i < adjacencyMatrix.GetLength(1); i++)
            {
                if (adjacencyMatrix[startVertexIndex,i] == 1 && !visited[i])
                {
                    DepthFirstSearch(i, adjacencyMatrix, visited);
                }
            }
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
