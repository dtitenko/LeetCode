from collections import defaultdict

# This class represents a directed graph using adjacency list representation


class Graph:

    def __init__(self, vertices):
        self.V = vertices
        self.graph = defaultdict(list)

    def addEdge(self, u, v):
        self.graph[u].append(v)

    # Use DFS to check path between s and d
    def isReachable(self, s, d):
        # Mark all the vertices as not visited
        visited = [False]*(self.V)

        # Create a stack for DFS
        stack = [s]

        while stack:

            # Dequeue a vertex from queue
            n = stack.pop()
            # Check a vertex is visited
            if visited[n]:
                continue
            # mark vertex as visited
            visited[n] = True

            # If destination node reached return True
            if n == d:
                 return True

            # Add adjacent vertices to the stack
            for i in self.graph[n]:
                stack.append(i)
         # If DFS is complete without visited d
        return False

g = Graph(4)
g.addEdge(0, 1)
g.addEdge(0, 2)
g.addEdge(1, 2)
g.addEdge(2, 0)
g.addEdge(2, 3)
g.addEdge(3, 3)

u = 1; v = 3

if g.isReachable(u, v):
    print("There is a path from %d to %d" % (u,v))
else :
    print("There is no path from %d to %d" % (u,v))

u = 3; v = 1
if g.isReachable(u, v) :
    print("There is a path from %d to %d" % (u,v))
else :
    print("There is no path from %d to %d" % (u,v))
