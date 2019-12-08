import Foundation

func processOps(inputs: [Int]) throws -> Int {
  var inputs = inputs
  var intOp = try String(contentsOfFile: "input.txt").split(separator: ",").map{ Int($0)! }

  var pointer = 0
  while (intOp[pointer] != 99) {
    let p1 = ((intOp[pointer] / 100) % 10) == 0 ? intOp[intOp[pointer + 1]] : intOp[pointer + 1]

    var p2 = intOp[pointer + 2]
    if ((intOp[pointer] / 1000) == 0 && p2 < intOp.count) {
      p2 = intOp[p2]
    }

    switch (intOp[pointer] % 100) {
      case 1:
        intOp[intOp[pointer + 3]] = p1 + p2
        pointer += 4
      case 2:
        intOp[intOp[pointer + 3]] = p1 * p2
        pointer += 4
      case 3:
        intOp[intOp[pointer + 1]] = inputs.removeFirst()
        pointer += 2
      case 4:
        //print("Output[\(pointer)]: \(p1)")
        pointer += 2
        return p1
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
        print("Unexpected Op: \(intOp[pointer])")
        break
    }
  }

  return 0
}

func MaxAmplifier(amplifiers: [Int], input: Int = 0) throws -> Int {
  var maxValue = 0

  for a in amplifiers {
    let nextInput = try processOps(inputs: [a, input])

    if (amplifiers.count == 1) { return nextInput }

    var remaining = amplifiers
    remaining.removeAll(where: { $0 == a })

    let ampValue = try MaxAmplifier(amplifiers: remaining, input: nextInput)

    maxValue = max(ampValue, maxValue)
  }
  return maxValue
}

let result = try MaxAmplifier(amplifiers: [0,1,2,3,4])

print("Highest signal: \(result)")
