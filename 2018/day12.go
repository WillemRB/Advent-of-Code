package main

import (
  "fmt"
  "math"
  "io/ioutil"
  "strings"
)

type Note struct {
  plant int
  mask int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  plants := make(map[int]int)
  notes := make([]Note, 0)

  for l, line := range lines {
    if l == 0 {
      plants = FillPlants(plants, line)
    }

    if l >= 2 {
      n := strings.Split(line, " ")
    
      p := 0
      if n[2] == "#" {
        p = 1
      }

      notes = append(notes, Note{ p, MaskToInt(n[0]) })
    }
  }

  // The plants stabilize after a number of generations
  plants = ComputeGeneration(plants, notes, 1000)

  sum := 0
  plant_count := 0
  for i, p := range plants {
    sum += i * p
    plant_count += p
  }

  fmt.Println("Sum of pots with plants: ", sum)
  fmt.Println("Plants: ", plant_count)
  big_sum := (float64(plant_count) * (float64(50000000000) - float64(1000))) + float64(sum)
  fmt.Println("After 50.000.000.000 generations:", big_sum)
}

func ComputeGeneration(plants map[int]int, notes []Note, generations int) map[int]int {
  for gen := 1; gen <= generations; gen++ {
    next_gen := make(map[int]int)
    for i := -4; i < len(plants) + 4; i++ {
      m := GetMask(plants, i)
      for idx, n := range notes {
        if m == n.mask {
          next_gen[i] = n.plant
          break
        } else if idx == len(notes) - 1 {
          next_gen[i] = 0
          break
        }
      }
    }
    plants = next_gen
  }

  return plants
}

func MaskToInt(mask string) int {
  result := 0
  for i, c := range mask {
    if c == '#' {
      result += Pow(2, i)
    }
  }
  return result
}

func FillPlants(plants map[int]int, line string) map[int]int {
  initial := strings.Split(line, " ")[2]
  for i, c := range initial {
    if c == '#' {
      plants[i] = 1
    } else {
      plants[i] = 0
    }
  }
  return plants
}

func GetMask(plants map[int]int, index int) int {
  mask := 0
  for i := 0; i <= 4 ; i++ {
    p, ok := plants[(index - 2) + i]
    if ok {
      mask += Pow(2, i) * p
    }
  }
  return mask
}

func Pow(a, b int) int {
  pow := math.Pow(float64(a), float64(b))
  return int(pow)
}
