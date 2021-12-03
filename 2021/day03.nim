from strutils import fromBin, splitWhitespace
from sequtils import filter

var diagnostics : seq[string]

proc loadDiagnostics() : seq[string] =
  let entireFile = readFile("input.txt")
  result = splitWhitespace(entireFile)

proc processSeq(diagnostics : seq[string]) : seq[int] =
  for line in diagnostics:
    if result.len == 0:
      result = newSeq[int](line.len)
    
    for idx, c in line:
      result[idx] += (c == '1').int

proc getGamma(binary : seq[int]) : int =
  var gamma : string
  for i in binary:
    gamma.add(if i * 2 > diagnostics.len: '1' else: '0')
  result = fromBin[int](gamma)

proc getEpsilon(binary : seq[int]) : int =
  var epsilon : string
  for i in binary:
    epsilon.add(if i * 2 > diagnostics.len: '0' else: '1')
  result = fromBin[int](epsilon)

proc getOxygenGeneratorRating() : int =
  diagnostics = loadDiagnostics()
  let bits = diagnostics[0].len

  for idx in 0..<bits:
    var s = processSeq(diagnostics)
    diagnostics = filter(diagnostics, proc(x: string): bool = x[idx] == (if s[idx] * 2 >= diagnostics.len: '1' else: '0'))
    if diagnostics.len == 1:
      break

  result = fromBin[int](diagnostics[0])

proc getCO2ScrubberRating() : int =
  diagnostics = loadDiagnostics()
  let bits = diagnostics[0].len

  for idx in 0..<bits:
    var s = processSeq(diagnostics)
    diagnostics = filter(diagnostics, proc(x: string): bool = x[idx] == (if s[idx] * 2 >= diagnostics.len: '0' else: '1'))
    if diagnostics.len == 1:
      break

  result = fromBin[int](diagnostics[0])

# Part A
diagnostics = loadDiagnostics()
var bin = processSeq(diagnostics)
echo getGamma(bin) * getEpsilon(bin)

# Part B
echo getOxygenGeneratorRating() * getCO2ScrubberRating()
