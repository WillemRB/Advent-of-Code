import Foundation

var orbits = 0

func OrbitCountChecksum(object:String, level:Int = 0) {
  orbits += level
  for orbit in (map[object] ?? [])! {
    OrbitCountChecksum(object: orbit, level: level + 1)
  }
}

func GetPathToCom(from:String) -> [String] {
  var path = [from]

  while(path.first != "COM") {
    let kv = map.first(where: { $0.value.contains(path.first!) })
    path.insert(kv?.key ?? "", at: 0)
  }

  return path
}

var map = try String(contentsOfFile: "input.txt")
  .split(separator: "\n")
  .map{ $0.split(separator: ")") }
  .reduce(into: [:]) { orb, map in
    orb[String(map.first!), default: []].append(String(map.last!)) }

//OrbitCountChecksum(object: "COM")
//print("Orbits: \(orbits)")

var you = GetPathToCom(from: "YOU")
var san = GetPathToCom(from: "SAN")

while (you.first == san.first) {
  you.removeFirst()
  san.removeFirst()
}

let transfers = (you.count + san.count) - 2
print("Orbital transfers required: \(transfers)")
