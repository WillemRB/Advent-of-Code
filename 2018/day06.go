package main

import (
  "fmt"
  "io/ioutil"
  "math"
  "strings"
  "strconv"
)

type Point struct {
  x float64
  y float64
  area int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  points := make([]Point, 0)

  for _, line := range lines {
    split := strings.Split(string(line), ", ")

    x, _ := strconv.ParseFloat(split[0], 64)
    y, _ := strconv.ParseFloat(split[1], 64)
    points = append(points, Point { x, y, 0 })
  }

  const grid_size = 360
  const max_distance_to_all = float64(10000)

  grid := [grid_size][grid_size]int{}
  edge_points := make(map[int]struct{})
  area_size := 0

  for i := 0; i < grid_size; i++ {
    for j := 0; j < grid_size; j++ {
      closest := 0
      distance := float64(1000000)
      sum_distance := float64(0)
      for idx, p := range points {
        dist := math.Abs(p.x - float64(i)) + math.Abs(p.y - float64(j))
        sum_distance += dist

        if dist < distance {
          grid[i][j] = idx
          closest = idx
          distance = dist
        } else if dist == distance {
          grid[i][j] = -1
          closest = -1
        }
      }

      if closest >= 0 {
        if i == 0 || j == 0 || j == grid_size - 1 || i == grid_size -1 {
          edge_points[grid[i][j]] = struct{}{}
        }
        points[closest].area++
      }

      if sum_distance < max_distance_to_all {
        area_size++
      }
    }
  }

  max_area := 0

  for idx, p := range points {
    _, edge_point := edge_points[idx]
    if !edge_point && p.area > max_area {
      max_area = p.area
    }
  }

  fmt.Printf("Largest non infinite area: %d\n", max_area)
  fmt.Printf("Size of region: %d\n", area_size)
}
