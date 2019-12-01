import Foundation

let contents = (try String(contentsOfFile: "input.txt")).split(separator: "\n")

let input = contents.map { Int($0) }

let result = input.reduce(0) { $0 + (($1! / 3) - 2) }

print(result)