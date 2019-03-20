package main

import (
  "fmt"
  "io/ioutil"
  "regexp"
  "strconv"
  "strings"
)

type Light struct {
  pos_x int
  pos_y int
  vel_x int
  vel_y int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  re := regexp.MustCompile("^position=<\\s*(-?\\d+),\\s*(-?\\d+)> velocity=<\\s*(-?\\d+),\\s*(-?\\d+)>$")
  lights := make([]Light, 0)
  
  for _, line := range lines {
    m := re.FindAllStringSubmatch(line, -1)[0]
    
    pos_x, _ := strconv.Atoi(m[1])
    pos_y, _ := strconv.Atoi(m[2])
    vel_x, _ := strconv.Atoi(m[3])
    vel_y, _ := strconv.Atoi(m[4])

    lights = append(lights, Light{ pos_x, pos_y, vel_x, vel_y })
  }

  count := 0
  for {
    left_top_x, left_top_y, right_bot_x, right_bot_y := Boundary(lights)

    height := left_top_y - right_bot_y
    if height < 0 {
      height = -1 * height
    }

    width := right_bot_x - left_top_x
    if width < 0 {
      width = -1 * width
    }

    if height <= 10 {
      sky := make([][]string, height + 1)
      for i, _ := range sky {
        sky[i] = strings.Split(strings.Repeat(".", width + 1), "")
      }

      for _, light := range lights {
        sky[light.pos_y - right_bot_y][light.pos_x - left_top_x] = "#"
      }

      fmt.Printf("After %d seconds:\n", count)
      for _, row := range sky {
        fmt.Println(row)
      }
      break
    }

    count++
    lights = Step(lights)
  }
}

func Boundary(lights []Light) (int, int, int, int) {
  left_top_x := lights[0].pos_x
  left_top_y := lights[0].pos_y
  right_bot_x := lights[0].pos_x
  right_bot_y := lights[0].pos_y

  for _, light := range lights {
    if light.pos_x < left_top_x {
      left_top_x = light.pos_x
    }
    if light.pos_x > right_bot_x {
      right_bot_x = light.pos_x
    }
    if light.pos_y < right_bot_y {
      right_bot_y = light.pos_y
    }
    if light.pos_y > left_top_y {
      left_top_y = light.pos_y
    }
  }

  return left_top_x, left_top_y, right_bot_x, right_bot_y
}

func Step(lights []Light) []Light {
  for i, light := range lights {
    light.pos_x += light.vel_x
    light.pos_y += light.vel_y
    lights[i] = light
  }
  return lights
}
