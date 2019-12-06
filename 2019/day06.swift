import Foundation

var orbits = 0

func OrbitCountChecksum(object:String, level:Int = 0) {
  orbits += level
  for orbit in (map[object] ?? [])! {
    OrbitCountChecksum(object: orbit, level: level + 1)
  }
}

var map = try String(contentsOfFile: "input.txt")
  .split(separator: "\n")
  .map{ $0.split(separator: ")") }
  .reduce(into: [:]) { orb, map in
    orb[String(map.first!), default: []].append(String(map.last!)) }

OrbitCountChecksum(object: "COM")
print("Orbits: \(orbits)")
