# Sort stack such that the smallest item is on the top. You can use additional stack.
# Time: O(n^2)
# Space: O(N)

def sort(stack):
    r = []
    while len(stack) > 0:
        temp = stack.pop()
        while len(r) > 0 and r[-1] > temp:
            stack.append(r.pop())
        r.append(temp)
    while len(r) > 0:
        stack.append(r.pop())

# [2, 4, 7, 3, 1] []
# [2, 4, 7, 3] [1]
# [2, 4, 7] [1, 3]
# [2, 4] [1, 3, 7]
# [2, 7] [1, 3, 4]
# [2] [1, 3, 4, 7]
# [7, 4, 3] [1, 2]
# [7, 4] [1, 2, 3]
# [7] [1, 2, 3, 4]
# [] [1, 2, 3, 4, 7]
# [7, 4, 3, 2, 1] []

stack = [2,4,7,3,1]
sort(stack)
print(stack)