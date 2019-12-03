import Foundation

func CrossDistance(w1l: (Int, Int), w1nl: (Int, Int), w2l: (Int, Int), w2nl: (Int, Int)) -> Int {
  if (min(w1l.0, w1nl.0) < min(w2l.0, w2nl.0) && max(w1l.0, w1nl.0) > max(w2l.0, w2nl.0)) {
    if (min(w1l.1, w1nl.1) > min(w2l.1, w2nl.1) && max(w1l.1, w1nl.1) < max(w2l.1, w2nl.1)) {
      return abs(w1l.1) + abs(w2l.0)
    }
  }

  return Int.max
}

let wires = try String(contentsOfFile: "input.txt").split(separator: "\n")

let wire1 = wires[0].split(separator: ",").map { ( $0.first!, Int($0.dropFirst(1))! ) }
let wire2 = wires[1].split(separator: ",").map { ( $0.first!, Int($0.dropFirst(1))! ) }

var wire1Location = (x: 0, y: 0)
var minimalDistance = Int.max

for (dir1, dist1) in wire1 {
  var wire1NextLocation = wire1Location
  if (dir1 == "U" || dir1 == "D") {
    wire1NextLocation.y += dir1 == "D" ? -1 * dist1 : dist1
  }
  else { // "R", "L"
    wire1NextLocation.x += dir1 == "L" ? -1 * dist1 : dist1
  }

  var wire2Location = (x: 0, y: 0)
  for (dir2, dist2) in wire2 {
    var wire2NextLocation = wire2Location
    if (dir2 == "U" || dir2 == "D") {
      wire2NextLocation.y += dir2 == "D" ? -1 * dist2 : dist2
      if (dir1 == "R" || dir1 == "L") {
        minimalDistance = min(CrossDistance(w1l: wire1Location, w1nl: wire1NextLocation, w2l: wire2Location, w2nl: wire2NextLocation), minimalDistance)
      }
    }
    else { // "R", "L"
      wire2NextLocation.x += dir2 == "L" ? -1 * dist2 : dist2
      if (dir1 == "U" || dir1 == "D") {
        minimalDistance = min(CrossDistance(w1l: wire2Location, w1nl: wire2NextLocation, w2l: wire1Location, w2nl: wire1NextLocation), minimalDistance)
      }
    }
    wire2Location = wire2NextLocation
  }

  wire1Location = wire1NextLocation
}

print("Minimal Distance: \(minimalDistance)")
