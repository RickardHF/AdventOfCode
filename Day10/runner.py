
is_test = False

file_name = "Day10/input.txt" if not is_test else "Day10/test.txt" 

scan = []

# Read from file
with open(file_name, "r") as f:
    scan = [[char for char in line.strip()] for line in f.readlines()]

print(scan)

# get start position

start_row = 0
startCol = 0

for x in range(len(scan)):
    if ('S' in scan[x]):
        start_row = x
        start_col = scan[x].index('S')
        break

print(start_row, start_col)

def check_legal(col, row, from_col, from_row):
    if col < 0 or col >= len(scan):
        return False
    if row < 0 or row >= len(scan[0]):
        return False
    char = scan[col][row]
 
    #if char == 'S':
    #    return True
    
    if char == '-':
        return from_col == col and from_row != row
    
    if char == '|':
        return from_col != col and from_row == row
    
    if char == 'F':
        return (from_col - col == 1 and from_row == row) or (from_col == col and from_row - row == 1)
    if char == '7':
        return (from_col - col == 1 and from_row == row) or (from_col == col and row - from_row == 1)
    if char == 'J':
        return (from_col - col == -1 and from_row == row) or (from_col == col and from_row - row == -1)
    if char == 'L':
        return (from_col - col == -1 and from_row == row) or (from_col == col and from_row - row == 1)
    
    return False

def get_next_nodes(col, row, from_col, from_row):
    options = [(col + 1, row), (col - 1, row), (col, row + 1), (col, row - 1)]
    return [option for option in options if check_legal(option[0], option[1], col, row) and option != (from_col, from_row)]


print(get_next_nodes(start_row, start_col, 0, 0))

def get_path(col, row, from_col, from_row, path):
    next_nodes = get_next_nodes(col, row, from_col, from_row)
    
    path.append((col, row))

    if (len(next_nodes) == 0):
        return path
    if len(next_nodes) == 1:
        return get_path(next_nodes[0][0], next_nodes[0][1], col, row, path)
    else:
        paths = []
        for next_node in next_nodes:
            paths.append(get_path(next_node[0], next_node[1], col, row, path.copy()))
        return paths

steps = 0
col_1 = start_col
row_1 = start_row
col_2 = start_col
row_2 = start_row

