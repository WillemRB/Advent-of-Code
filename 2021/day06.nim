from strutils import split, parseInt
from sequtils import foldl

let entireFile = readFile("input.txt")
var split = split(entireFile, ',')

var lanternfish = newSeq[int](9)

for l in split:
  lanternfish[parseInt(l)] += 1

for day in 1..257:
  let addLantern = lanternfish[7]
  lanternfish[7] = lanternfish[8]
  lanternfish[8] = lanternfish[day mod 7]

  lanternfish[day mod 7] += addLantern

echo foldl(lanternfish[0..6], a + b, 0)
