package main

import (
  "bytes"
  "fmt"
  "io/ioutil"
)

func main() {
  polymer, _ := ioutil.ReadFile("input")
  
  // A
  //length := ReducePolymer(polymer)
  //fmt.Printf("Sequence Length: %d\n", length)
  
  minimal := len(polymer)
  for i := 0; i < 26; i++ {
    upper := []byte{ byte(i + 65) }
    filtered := bytes.Replace(polymer, upper, []byte{}, -1)
    lower := []byte{ byte(i + 97) }
    filtered = bytes.Replace(filtered, lower, []byte{}, -1)
    
    reduced := ReducePolymer(filtered)
    fmt.Printf("Length replacing %q and %q: %d\n", upper, lower, reduced)
    if (reduced < minimal) {
      minimal = reduced
    }
  }

  fmt.Printf("Minimal Length: %d\n", minimal)
}

func ReducePolymer(polymer []byte) int {
  for {
    r := FindReaction(polymer)

    if r == -1 { break }
    
    polymer = bytes.Replace(polymer, polymer[r-1:r+1], []byte{}, -1)
  }

  return len(polymer)
}

func FindReaction(polymer []byte) int {
  prev := byte(0)
  for i, b := range polymer {
    if prev + byte(32) == b || prev - byte(32) == b {
      return i
    }
    prev = b
  }

  return -1
}
