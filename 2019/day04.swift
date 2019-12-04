import Foundation

let start = 172930
let end = 683082

var countA = 0, countB = 0

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
    countA += 1
    
    var hashmap: [Character: Int] = [:]
    String(password).forEach {
      hashmap[$0] = (hashmap[$0] ?? 0)! + 1
    }
    countB += hashmap.values.contains(2) ? 1 : 0
  }
}

print("Possible passcodes A: \(countA)")
print("Possible passcodes B: \(countB)")
