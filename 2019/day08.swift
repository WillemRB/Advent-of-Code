import Foundation

var image = try String(contentsOfFile: "input.txt")

let width = 25
let height = 6
let imageSize = width * height

var zeroes = imageSize
var result = 0

var message = ""

while (!image.isEmpty) {
  let layer = image.prefix(imageSize)
  image = String(image.suffix(image.count - imageSize))

  // A
  // var hashmap: [Character: Int] = [:]
  // String(layer).forEach {
  //   hashmap[$0] = (hashmap[$0] ?? 0)! + 1
  // }
  // print(layer)

  // if (hashmap["0"] ?? 0 < zeroes) {
  //   print(hashmap["0"]!)
  //   zeroes = hashmap["0"]!
  //   result = (hashmap["1"]!) * (hashmap["2"]!)
  // }

  if (message.isEmpty) {
    message = String(layer)
  }
  else {
    var decoded = ""
    for c in layer {
      let char = message.removeFirst()
      decoded.append(char == "2" ? c : char)
    }
    message = decoded
  }
}

//print("Result: \(message)") // A

while (!message.isEmpty) {
  print(message.prefix(width).map { $0 == "0" ? "\u{20}" : "\u{2588}" }.joined(separator: ""))
  message = String(message.suffix(message.count - width))
}
