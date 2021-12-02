import strutils as str

var horizontalPosition : int
var depth : int
var aim : int

proc divePart1() : int =
  for line in "input.txt".lines:
    var op = str.splitWhitespace(line)
    case op[0]
    of "forward":
      horizontalPosition += str.parseInt(op[1])
    of "up":
      depth -= str.parseInt(op[1])
    of "down":
      depth += str.parseInt(op[1])
  
  return horizontalPosition * depth

proc divePart2() : int =
  for line in "input.txt".lines:
    var op = str.splitWhitespace(line)
    case op[0]
    of "forward":
      horizontalPosition += str.parseInt(op[1])
      depth += str.parseInt(op[1]) * aim
    of "up":
      aim -= str.parseInt(op[1])
    of "down":
      aim += str.parseInt(op[1])
  
  return horizontalPosition * depth

echo divePart2()
