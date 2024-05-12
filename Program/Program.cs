using System;
using System.IO;
using Dinica;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            var input = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int count_vertex = int.Parse(input[0]);
            int count_edges = int.Parse(input[1]);
            Graph graph = new Graph(count_vertex);
            for (int i = 0; i < count_edges; i++)
            {
                input = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                graph.AddEdge(int.Parse(input[0]) - 1, int.Parse(input[1]) - 1, int.Parse(input[2]));
            }
            int from = 0;
            int to = count_vertex - 1;
            Console.WriteLine($"Максимальный поток из ({from+1}) в ({to+1}): {graph.Dinica(from, to)}");
        }
    }
}
