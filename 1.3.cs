using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._3
{
    class Graph
    {
        List<List<int>> adjacencyList;
        //Конструктор класса принимает размер графа и инициализирует пустой список adjacencyList, который будет хранить смежные вершины для каждой вершины графа.
        public Graph(int size)
        {
            adjacencyList = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                adjacencyList.Add(new List<int>());
            }
        }
        //Метод AddEdge принимает две вершины - from и to, и добавляет их в список смежности друг друга.
        //Это позволяет установить связь между вершинами графа.
        public void AddEdge(int from, int to)
        {
            adjacencyList[from].Add(to);
            adjacencyList[to].Add(from);
        }
        //Метод GetNeighbors принимает вершину графа и возвращает список ее смежных вершин.
        public List<int> GetNeighbors(int vertex)
        {
            return adjacencyList[vertex];
        }
        //Метод PrintGraph выводит на экран информацию о графе. Он перебирает все вершины графа и для каждой вершины выводит ее номер
        //, а затем перебирает все смежные вершины и выводит их номера.
        public void PrintGraph()
        {
            for (int i = 0; i < adjacencyList.Count; i++)
            {
                Console.Write($"Вершина {i + 1}: ");
                foreach (var vertex in adjacencyList[i])
                {
                    Console.Write($"{vertex + 1} ");
                }
                Console.WriteLine();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер графа:");
            int size = Convert.ToInt32(Console.ReadLine());

            Graph graph = GenerateAdjacencyList(size);

            Console.WriteLine("Граф смежности для графа G1:");
            graph.PrintGraph();

            //массив visited, который используется для отслеживания посещенных вершин. 
            bool[] visited = new bool[size];


            //цикл, который проходит по всем вершинам графа. 
            for (int startVertexIndex = 0; startVertexIndex < size; startVertexIndex++)
            {
                //Если текущая вершина не была посещена, то выполняется обход в глубину с использованием функции DepthFirstSearch
                if (!visited[startVertexIndex])
                {
                    Console.WriteLine("Обход в глубину, начиная с вершины " + (startVertexIndex + 1) + ":");
                    DepthFirstSearch(startVertexIndex, graph, visited);
                }
            }
        }
        //Функция GenerateAdjacencyList генерирует случайный граф заданного размера size в виде списка смежности.
        private static Graph GenerateAdjacencyList(int size)
        {
            Random r = new Random();
            Graph graph = new Graph(size);

            //двойной цикл, который перебирает все возможные комбинации вершин графа.
            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    //Если сгенерированное число равно 1, то вызывается метод AddEdge объекта graph для добавления ребра между вершинами i и j.
                    if (r.Next(2) == 1)
                    {
                        graph.AddEdge(i, j);
                    }
                }
            }
            //После завершения циклов возвращается объект graph, содержащий случайно сгенерированный граф в виде списка смежности.
            return graph;
        }
        //Функция DepthFirstSearch осуществляет поиск в глубину, начиная с заданной стартовой вершины startVertexIndex.
        //Входные параметры функции - граф graph и массив visited, отслеживающий посещенные вершины.
        static void DepthFirstSearch(int startVertexIndex, Graph graph, bool[] visited)
        {
            //Алгоритм использует стек для хранения вершин, которые нужно посетить.
            Stack<int> stack = new Stack<int>();

            //Начальная вершина помещается в стек
            stack.Push(startVertexIndex);

            visited[startVertexIndex] = true;

            //а затем пока стек не пустой, извлекается текущая вершина из стека. 
            while (stack.Count > 0)
            {
                int currentVertexIndex = stack.Pop();
                //Затем выводится информация о посещенной вершине (в данном случае просто ее номер)
                Console.WriteLine("Посещена вершина №" + (currentVertexIndex + 1));
                //и для каждого соседа текущей вершины, который еще не был посещен, он добавляется в стек и отмечается как посещенный.
                List<int> neighbors = graph.GetNeighbors(currentVertexIndex);

                foreach(int neighbor in neighbors)
                {
                    if (!visited[neighbor])
                    {
                        stack.Push(neighbor);
                        visited[neighbor] = true;
                    }
                }
            }
        }
    }
}
