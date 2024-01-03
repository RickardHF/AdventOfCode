

is_test = False

file_name = "Day9/input.txt" if not is_test else "Day9/test.txt"

sequences = []

# Read from file
with open(file_name, "r") as f:
    lines = f.readlines()
    
    for line in lines:
        sequences.append([int(num) for num in line.strip().split(" ")])

pyramids = []

for sequence in sequences:
    all_zero = False
    pyramid = [sequence]
    while not all_zero:
        new_line = []
        for i in range(len(pyramid[-1]) - 1):
            new_line.append(pyramid[-1][i + 1] - pyramid[-1][i])
        pyramid.append(new_line)
        all_zero = all([num == 0 for num in pyramid[-1]])
    pyramids.append(pyramid)

next_numbers = []
left_numbers = []

for pyramid in pyramids:
    next_number = 0
    left_number = 0
    for i in range(len(pyramid) - 1, 0, -1):
        next_number += pyramid[i - 1][-1]
        left_number = pyramid[i - 1][0] - left_number
    next_numbers.append(next_number)
    left_numbers.append(left_number)
    count = 0
    for line in pyramid:
        offset = ["-" for i in range(count)]
        print(f"{'-'.join(offset)}{line}")
print(sum(next_numbers))
print(sum(left_numbers))
# 1st too high 1984151189
# 2nd too low   709973419