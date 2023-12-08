import math
from functools import reduce

is_test = False

file_name = "Day8/input.txt" if not is_test else "Day8/test.txt"

nodes = {}

current_node = "AAA"

# Read from file
with open(file_name, "r") as f:
    lines = f.readlines()
    directions = lines[0].strip()

    # Skip the first line
    for line in lines[2:]:
        key = line.split("=")[0].strip()
        values = line.split("=")[1].strip()[1:-1]
        value_list = [value.strip() for value in values.split(",")]
        nodes[key] = value_list

# Part 1

index = 0
steps = 0

def get_new_current_node(node, direction):
    if(direction == "L"):
        return nodes[node][0]
    else:
        return nodes[node][1]

while current_node != "ZZZ":
    print("Going from ", current_node, " to ", get_new_current_node(current_node, directions[index]))
    direction = directions[index]
    current_node = get_new_current_node(current_node, direction)
    index += 1
    index %= len(directions)
    steps += 1

print("Part 1: ", steps)

# Part 2

start_nodes = [node for node in nodes.keys() if node.endswith("A")]
start_node_steps = []

for node in start_nodes:
    steps = 0
    index = 0
    current_node = node
    
    while not current_node.endswith("Z"):
        direction = directions[index]
        current_node = get_new_current_node(current_node, direction)
        index += 1
        index %= len(directions)
        steps += 1
    
    start_node_steps.append(steps)

# Find lowest common multiple
def lcm(a, b):
    return abs(a*b) // math.gcd(a, b)

def lcm_list(l):
    return reduce(lcm, l)

print("Part 2: ", lcm_list(start_node_steps))