import strutils as str

let windowSize = 1

var prev = 1000000
var window : seq[int]

proc sum(a: seq[int]) : int =
  for v in a:
    result += v

proc sonarSweep() : int =
  for line in "input.txt".lines:
    var value = str.parseInt(line)
    window.add(value)

    if window.len < windowSize:
      continue
    
    var sum = sum(window)
    result += (sum > prev).int
    prev = sum  

    window.delete(0)

echo sonarSweep()
