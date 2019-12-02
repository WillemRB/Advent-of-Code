import Foundation

func processOps(noun: Int, verb: Int) throws -> Int {
  var intOp = try String(contentsOfFile: "input.txt").split(separator: ",").map{ Int($0)! }

  intOp[1] = noun
  intOp[2] = verb

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

  return intOp[0]
}

let result = try processOps(noun: 12, verb: 2)
print("Result A: \(result)")

for noun in 0...99 {
  for verb in 0...99 {
    let res = try processOps(noun: noun, verb: verb)
    
    if (res == 19690720) {
      print("Result B: \(100 * noun + verb)")
    }
  }
}
