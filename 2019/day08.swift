import Foundation

var image = try String(contentsOfFile: "input.txt")

let width = 25
let height = 6
let imageSize = width * height

var zeroes = imageSize
var result = 0

while (!image.isEmpty) {
  let layer = image.prefix(imageSize)
  image = String(image.suffix(image.count - imageSize))

  var hashmap: [Character: Int] = [:]
  String(layer).forEach {
    hashmap[$0] = (hashmap[$0] ?? 0)! + 1
  }

  if (hashmap["0"] ?? 0 < zeroes) {
    zeroes = hashmap["0"]!
    result = (hashmap["1"]!) * (hashmap["2"]!)
  }
}

print("Result: \(result)")
