namespace Dinica
{
    public class Edge
    {
        public int _to, _capacity, _reverse;
        public long _flow;
        public Edge(int to, long flow, int capacity, int reverse_edge)
        {
            _to = to;
            _flow = flow;
            _capacity = capacity;
            _reverse = reverse_edge;
        }
    }
}
