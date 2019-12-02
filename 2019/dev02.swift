import Foundation

var intOp = try String(contentsOfFile: "input.txt").split(separator: ",").map{ Int($0)! }

intOp[1] = 12
intOp[2] = 2

var index = 0
while (intOp[index] != 99) {
  if(intOp[index] == 1) {
    intOp[intOp[index + 3]] = intOp[intOp[index + 1]] + intOp[intOp[index + 2]]
  }
  else {
    intOp[intOp[index + 3]] = intOp[intOp[index + 1]] * intOp[intOp[index + 2]]
  }

  index += 4
}

print("Result: \(intOp[0])")
