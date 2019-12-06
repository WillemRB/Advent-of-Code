import Foundation

func processOps(input: Int) throws {
  var intOp = try String(contentsOfFile: "input.txt").split(separator: ",").map{ Int($0)! }

  var pointer = 0
  while (intOp[pointer] != 99) {
    let p1 = ((intOp[pointer] / 100) % 10) == 0 ? intOp[intOp[pointer + 1]] : intOp[pointer + 1]
    
    var p2 = 0
    if (intOp[pointer] % 100 != 3 && intOp[pointer] % 100 != 4) {
      p2 = (intOp[pointer] / 1000) == 0 ? intOp[intOp[pointer + 2]] : intOp[pointer + 2]
    }

    switch (intOp[pointer] % 100) {
      case 1:
        intOp[intOp[pointer + 3]] = p1 + p2
        pointer += 4
      case 2:
        intOp[intOp[pointer + 3]] = p1 * p2
        pointer += 4
      case 3:
        intOp[intOp[pointer + 1]] = input
        pointer += 2
      case 4:
        print("Output[\(pointer)]: \(p1)")
        pointer += 2
      case 5:
        pointer = p1 != 0 ? p2 : pointer + 3
      case 6:
        pointer = p1 == 0 ? p2 : pointer + 3
      case 7:
        intOp[intOp[pointer + 3]] = p1 < p2 ? 1 : 0
        pointer += 4
      case 8:
        intOp[intOp[pointer + 3]] = p1 == p2 ? 1 : 0
        pointer += 4
      default:
        break
    }
  }
}

try processOps(input: 1) // A
try processOps(input: 5) // B
