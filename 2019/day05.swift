import Foundation

func processOps(input: Int) throws -> Int {
  var intOp = try String(contentsOfFile: "input.txt").split(separator: ",").map{ Int($0)! }

  var index = 0
  while (intOp[index] != 99) {
    if(intOp[index] % 100 == 1) {
      let p1 = ((intOp[index] / 100) % 10) == 0 ? intOp[intOp[index + 1]] : intOp[index + 1]
      let p2 = ((intOp[index] / 1000) % 10) == 0 ? intOp[intOp[index + 2]] : intOp[index + 2]
      intOp[intOp[index + 3]] = p1 + p2
      index += 4
    }
    else if (intOp[index] % 100 == 2) {
      let p1 = ((intOp[index] / 100) % 10) == 0 ? intOp[intOp[index + 1]] : intOp[index + 1]
      let p2 = ((intOp[index] / 1000) % 10) == 0 ? intOp[intOp[index + 2]] : intOp[index + 2]
      intOp[intOp[index + 3]] = p1 * p2
      index += 4
    }
    else if (intOp[index] % 100 == 3) {
      intOp[intOp[index + 1]] = input
      index += 2
    }
    else if (intOp[index] % 100 == 4) {
      let p1 = (intOp[index] / 100) == 0 ? intOp[intOp[index + 1]] : intOp[index + 1]
      print("Output[\(index)]: \(p1)")
      index += 2
    }
  }

  return intOp[0]
}

let _ = try processOps(input: 1)
