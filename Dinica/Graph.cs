using System;
using System.Collections.Generic;

namespace Dinica
{
    public class Graph
    {
        public readonly int _countVertex;
        private int[] _level;
        private List<Edge>[] _edges;

        public Graph(int count_vertex)
        {
            _countVertex = count_vertex;
            _edges = new List<Edge>[_countVertex];
            for (int i = 0; i < _countVertex; i++)
                _edges[i] = new List<Edge>();

            _level = new int[_countVertex];
        }
        public void AddEdge(int from, int to, int Capacity)
        {
            _edges[from].Add(new Edge(to, 0, Capacity, _edges[to].Count));
            _edges[to].Add(new Edge(from, 0, 0, _edges[from].Count));
        }
        private bool BFS(int startPoint, int endPoint)
        {
            for (int j = 0; j < _countVertex; j++)
                _level[j] = -1;
            _level[startPoint] = 0;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startPoint);
            while (queue.Count != 0)
            {
                int from = queue.Dequeue();
                for (int to = 0; to < _edges[from].Count; to++)
                {
                    Edge edge = _edges[from][to];
                    if (_level[edge._to] < 0 && edge._flow < edge._capacity)
                    {
                        _level[edge._to] = _level[from] + 1;
                        queue.Enqueue(edge._to);
                    }
                }
            }
            return _level[endPoint] < 0 ? false : true;
        }
        private long DFS(int from, long Flow, int endPoint, int[] ptr)
        {
            if (from == endPoint)
                return Flow;
            while (ptr[from] < _edges[from].Count)
            {
                Edge edge = _edges[from][ptr[from]];
                if (_level[edge._to] == _level[from] + 1 && edge._flow < edge._capacity)
                {
                    long current_flow = Math.Min(Flow, edge._capacity - edge._flow);
                    long temp_flow = DFS(edge._to, current_flow, endPoint, ptr);
                    if (temp_flow > 0)
                    {
                        edge._flow += temp_flow;
                        _edges[edge._to][edge._reverse]._flow -= temp_flow;
                        return temp_flow;
                    }
                }
                ptr[from]++;
            }
            return 0;
        }
        public long Dinica(int startPoint, int endPoint)
        {
            if (startPoint == endPoint)
                return -1;
            long maxFlow = 0;
            while (BFS(startPoint, endPoint))
            {
                int[] ptr = new int[_countVertex + 1];
                while (true)
                {
                    long Flow = DFS(startPoint, long.MaxValue, endPoint, ptr);
                    if (Flow == 0)
                        break;
                    maxFlow += Flow;
                }
            }
            return maxFlow;
        }
    }
}
