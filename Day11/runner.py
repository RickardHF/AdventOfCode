
is_test = True

file_name = "Day11/input.txt" if not is_test else "Day11/test.txt" 

scan = []

empty_distance = 100

# Read from file
with open(file_name, "r") as f:
    scan = [[char for char in line.strip()] for line in f.readlines()]

# Expand empty rows and columns

for line in scan:
    print(line)

empty_rows = [i for i in range(len(scan)) if all([char == "." for char in scan[i]])]

empty_cols = [i for i in range(len(scan[0])) if all([scan[j][i] == "." for j in range(len(scan))])]

print(empty_rows)
print(empty_cols)

# Find galaxies 
galaxies = []
for i in range(len(scan)):
    for j in range(len(scan[0])):
        if scan[i][j] == "#":
            galaxies.append((i, j))

shortest_paths = []

def largest_shortest(x, y):
    if (x > y):
        return (x, y)
    else:
        return (y, x)

def between(x, y, z):
    return x < z and y > z 

def manhattan_distance(point1, point2):
    (largest_row, shortest_row) = largest_shortest(point1[0], point2[0])
    (largest_col, shortest_col) = largest_shortest(point1[1], point2[1])

    fitting_empty_cols = [col for col in empty_cols if between(shortest_col, largest_col, col)]
    fitting_empty_rows = [row for row in empty_rows if between(shortest_row, largest_row, row)]
    
    return (largest_col + (len(fitting_empty_cols) * empty_distance) - shortest_col) + (largest_row + (len(fitting_empty_rows) * empty_distance) - shortest_row)

for i in range(len(galaxies) - 1):
    for j in range(i + 1, len(galaxies)):
        print(f"From: {i + 1}, to: {j + 1}")
        distance = manhattan_distance(galaxies[i], galaxies[j])
        print(distance)
        shortest_paths.append(distance)
print(shortest_paths)
print(len(shortest_paths))
print(sum(shortest_paths))

print("empty rows: ", empty_rows)
print("empty cols: ", empty_cols)