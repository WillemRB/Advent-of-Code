package main

import (
  "bufio"
  "fmt"
  "io/ioutil"
  "os"
  "strings"
)

func main() {
  //PartA()
  PartB()
}

func PartA() {
  file, _ := os.Open("input")
  twos := 0
  threes := 0
  defer file.Close()

  scanner := bufio.NewScanner(file)
  for scanner.Scan() {
    hash := make(map[byte]int)
    
    for _, b := range scanner.Bytes() {
      hash[b] += 1
    }

    two := false
    three := false

    for _, count := range hash {
      if count == 2 && !two {
        two = true
        twos += 1
      }

      if count == 3 && !three {
        three = true
        threes += 1
      }

      if two && three {
        break
      }
    }
  }

  fmt.Printf("Twos: %d, Threes: %d, Checksum: %d", twos, threes, twos * threes)
}

func PartB() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  for idx1 := 0; idx1 < len(lines) - 2; idx1++ {
    source := []rune(lines[idx1])
    for idx2 := idx1 + 1; idx2 < len(lines) - 1; idx2++ {
      diff := []int {}
      other := []rune(lines[idx2])

      for i := range source {
        if source[i] != other[i] {
          diff = append(diff, i)
        }
      }

      if len(diff) == 1 {
        fmt.Printf("%s%s", lines[idx1][:diff[0]], lines[idx1][(diff[0]) + 1:])
      }
    }
  }
}
