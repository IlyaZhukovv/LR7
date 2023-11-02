using System;
using System.Collections.Generic;

class Graph
{
    private List<List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new List<List<int>>();
    }

    public void AddEdge(int from, int to)
    {
        while (adjacencyList.Count <= from || adjacencyList.Count <= to)
        {
            adjacencyList.Add(new List<int>());
        }
        adjacencyList[from].Add(to);
        adjacencyList[to].Add(from);
    }

    public List<int> GetNeighbors(int vertex)
    {
        return adjacencyList[vertex];
    }

    //Вывод графа на экран
    public void PrintGraph()
    {
        for (int i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write("Вершина " + (i + 1) + ": ");
            foreach (int neighbor in adjacencyList[i])
            {
                Console.Write((neighbor + 1) + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите размер графа: ");
        int size = int.Parse(Console.ReadLine());

        Graph graph = GenerateAdjacencyList(size);

        Console.WriteLine("Список смежности для графа:");
        graph.PrintGraph();

        bool[] visited = new bool[size];

        for (int startVertexIndex = 0; startVertexIndex < size; startVertexIndex++)
        {
            if (!visited[startVertexIndex])
            {
                Console.WriteLine("Обход в глубину, начиная с вершины " + (startVertexIndex + 1) + ":");
                DepthFirstSearch(startVertexIndex, graph, visited);
            }
        }
    }

    static Graph GenerateAdjacencyList(int size)
    {
        Random r = new Random();
        Graph graph = new Graph();

        for (int i = 0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                int randomEdge = r.Next(2);
                if (randomEdge == 1)
                {
                    graph.AddEdge(i, j);
                }
            }
        }

        return graph;
    }

    //алгоритм обхода графа в глубину 
    static void DepthFirstSearch(int startVertexIndex, Graph graph, bool[] visited)
    {
        //Вначале функция помечает текущую вершину как посещенную
        visited[startVertexIndex] = true;

        //Затем она получает список смежных вершин для текущей вершины с помощью метода GetNeighbors
        List<int> neighbors = graph.GetNeighbors(startVertexIndex);

        //Затем функция проходит по всем смежным вершинам и для каждой смежной вершины проверяет, была ли она уже посещена.
        //Если смежная вершина не была посещена, то функция рекурсивно вызывает себя для этой смежной вершины, передавая ее в качестве новой стартовой вершины.
        foreach (int neighbor in neighbors)
        {
            if (!visited[neighbor])
            {
                //При каждом рекурсивном вызове функция выводит на экран ребро, которое соединяет текущую вершину и смежную вершину.
                Console.WriteLine((startVertexIndex + 1) + " -> " + (neighbor + 1));
                DepthFirstSearch(neighbor, graph, visited);
            }
        }
    }
}
