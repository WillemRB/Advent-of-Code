import Foundation

let start = 172930
let end = 683082

var count = 0

for password in start...end {
  var str = String(password)
  var increases = true, double = false

  var first: Character, second = str.removeFirst()
  while(increases) {
    first = second
    if (str.isEmpty) {
      break
    }
    second = str.removeFirst()
    increases = first <= second
    double = double || (first == second)
  }

  if (increases && double) {
    count += 1
  }
}

print("Possible passcodes: \(count)")
