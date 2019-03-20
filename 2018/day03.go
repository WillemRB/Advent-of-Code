package main

import (
  "fmt"
  "io/ioutil"
  "strings"
  "regexp"
  "strconv"
)

type Claim struct {
  id string
  x1 int
  x2 int
  y1 int
  y2 int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  overlap := 0
  fabric := [1000][1000]int{}
  re := regexp.MustCompile("^(#\\d+) @ (\\d+),(\\d+): (\\d+)x(\\d+)$")

  claims := make([]Claim, 0)
  
  for _, line := range lines {
    if line == "" { break }

    m := re.FindAllStringSubmatch(line, -1)[0]

    x1, _ := strconv.Atoi(m[2])
    x2, _ := strconv.Atoi(m[4])
    x2 += x1

    y1, _ := strconv.Atoi(m[3])
    y2, _ := strconv.Atoi(m[5])
    y2 += y1

    claims = append(claims, Claim{ m[1], x1, x2, y1, y2 })
  }


  // A
  for _, claim := range claims {
    for x := claim.x1; x < claim.x2; x++ {
      for y := claim.y1; y < claim.y2; y++ {
        fabric[x][y]++

        if (fabric[x][y] == 2) {
          overlap++
        }
      }
    }
  }

  fmt.Printf("Overlap: %d\n", overlap)

  // B
  for _, claim := range claims {
    overlapping := false

    for x := claim.x1; x < claim.x2; x++ {
      for y := claim.y1; y < claim.y2; y++ {
        if (fabric[x][y] > 1) {
          overlapping = true
        }
      }
    }

    if !overlapping {
      fmt.Println("Non-overlapping Claim:", claim)
    }
  }
}
