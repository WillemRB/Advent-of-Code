package main

import (
  "fmt"
)

type FuelCell struct {
  x int
  y int
  power_level int
  total_power int
  grid_size int
}

const grid_size = 300
const serial_number = 1955

func main() {
  grid := [grid_size][grid_size]FuelCell{}

  result := FuelCell{ 0, 0, 0, 0, 0 }

  for row := grid_size - 1; row >= 0; row-- {
    for col := grid_size - 1; col >= 0; col-- {
      rack_id := row + 10
      power_level := ((rack_id * col) + serial_number) * rack_id
      power_level = (power_level % 1000) / 100
      power_level = power_level - 5

      fuelCell := FuelCell{ row, col, power_level, 0, 0 }
      grid[row][col] = fuelCell

      bound := grid_size - row
      if bound > grid_size - col {
        bound = grid_size - col
      }

      for s := 1; s < bound; s++ {
        temp_power := 0
        for r := 0; r < s; r++ {
          for c := 0; c < s; c++ {
            temp_power += grid[row+r][col+c].power_level
          }
        }

        if fuelCell.total_power < temp_power {
          fuelCell.total_power = temp_power
          fuelCell.grid_size = s
        }
      }

      if fuelCell.total_power > result.total_power {
        result = fuelCell
      }

      grid[row][col] = fuelCell
    }
  }

  fmt.Println("Max total power: ", result.total_power)
  fmt.Printf("Coordinate: %d,%d,%d\n", result.x, result.y, result.grid_size)
}
