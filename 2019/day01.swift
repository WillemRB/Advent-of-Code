import Foundation

func determineFuel(value: Int) -> Int {
  let fuel = value / 3 - 2

  return fuel > 0 ? fuel + determineFuel(value: fuel) : 0
}

let input = (try String(contentsOfFile: "input.txt")).split(separator: "\n").map { Int($0) }

let fuelA = input.reduce(0) { $0 + ($1! / 3) - 2 }
let fuelB = input.reduce(0) { $0 + determineFuel(value: $1!) }

print("Part One: \(fuelA)")
print("Part Two: \(fuelB)")