public static class Dijkstra
{
    public static void DijkstraWithAdjacencyMatrix(int[][] graph, int sourceVertex, int destinationVertex)
    {
        Console.WriteLine($"Finding the shortest path from {sourceVertex} to {destinationVertex}\n");

        var vertexCount = graph.Length;
        var visitedVertex = new bool[vertexCount];
        var distance = new int[vertexCount];
        var lastUpdatedBy = new int?[vertexCount];

        for (int vertex = 0; vertex < vertexCount; vertex++)
        {
            visitedVertex[vertex] = false;
            distance[vertex] = vertex != sourceVertex ? int.MaxValue : 0;
            lastUpdatedBy[vertex] = null;
        }

        for (int vertex = 0; vertex < vertexCount; vertex++)//O(V)
        {
            var minDistanceVertex = FindMinDistanceVertex(distance, visitedVertex);

            visitedVertex[minDistanceVertex] = true;

            // Update the distance between neighbouring vertex and source vertex
            for (int neighborVertex = 0; neighborVertex < vertexCount; neighborVertex++)//O(V)
            {
                var newDistanceLength = distance[minDistanceVertex] + graph[minDistanceVertex][neighborVertex];
                var vertexNotVisited = !visitedVertex[neighborVertex];
                var hasEdgeWithNeighbor = graph[minDistanceVertex][neighborVertex] != 0;
                var newDistanceLengthIsLessThanTheCurrentDistance = newDistanceLength < distance[neighborVertex];

                if (vertexNotVisited && hasEdgeWithNeighbor && newDistanceLengthIsLessThanTheCurrentDistance)
                {
                    distance[neighborVertex] = newDistanceLength;
                    lastUpdatedBy[neighborVertex] = minDistanceVertex;
                }
            }
        }

        for (int destVertex = 0; destVertex < distance.Length; destVertex++)
            Console.WriteLine($"Distance from {sourceVertex} to {destVertex} is {distance[destVertex]}");

        Console.WriteLine();
        for (int vertex = 0; vertex < lastUpdatedBy.Length; vertex++)
        {
            var vertextLastUpdateBy = lastUpdatedBy[vertex] is null ? "Source Vertex" : lastUpdatedBy[vertex].ToString();
            Console.WriteLine($"Vertex: {vertex} - Last Updating By Vertex: {vertextLastUpdateBy}");
        }

        Console.WriteLine("\nPrinting the path");
        Console.Write(destinationVertex);

        while (destinationVertex != sourceVertex)
        {
            Console.Write(" -> ");
            destinationVertex = lastUpdatedBy[destinationVertex].GetValueOrDefault();
            Console.Write($"{destinationVertex}");
        }
    }

    // Finding the minimum distance vertex
    //To find the vertex with Min Distance from source vertex would be better use a Min Heap Data structure and extract the min node from the distance heap data structure
    private static int FindMinDistanceVertex(int[] distance, bool[] visitedVertex)
    {
        var currentMinDistance = int.MaxValue;
        var minDistanceVertex = -1;

        for (int vertex = 0; vertex < distance.Length; vertex++)
        {
            var vertexNotVisited = !visitedVertex[vertex];
            var currentVertexDistance = distance[vertex];

            if (vertexNotVisited && currentVertexDistance < currentMinDistance)
            {
                currentMinDistance = currentVertexDistance;
                minDistanceVertex = vertex;
            }
        }

        return minDistanceVertex;
    }
}