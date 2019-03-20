package main

import (
  "fmt"
  "io/ioutil"
  "strconv"
  "strings"
  "sort"
  "time"
)

type data_entry struct {
  timestamp time.Time
  data string
}

type guardData []data_entry

func (g guardData) Len() int {
  return len(g)
}

func (p guardData) Less(i, j int) bool {
  return p[i].timestamp.Before(p[j].timestamp)
}

func (p guardData) Swap(i, j int) {
  p[i], p[j] = p[j], p[i]
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  guard_data := make([]data_entry, 0)
  
  dtLayout := "2006-01-02 15:04"

  for _, line := range lines {
    if len(line) == 0 { break }

    datetime := line[1:17]
    data := line[19:]

    t, _ := time.Parse(dtLayout, datetime)

    guard_data = append(guard_data, data_entry{ t, data })
  }
  
  sort.Sort(guardData(guard_data))

  log := make(map[int][60]int)

  current_guard, asleep := -1, 0

  for _, gd := range guard_data {
    if strings.HasPrefix(gd.data, "Guard") {
      current_guard, _ = strconv.Atoi(strings.Split(gd.data, " ")[1][1:])
      continue
    }
    if strings.HasPrefix(gd.data, "falls") {
      asleep = gd.timestamp.Minute()
      continue
    }
    if strings.HasPrefix(gd.data, "wakes") {
      temp := log[current_guard]
      for m := asleep; m < gd.timestamp.Minute(); m++ {
        temp[m]++
      }
      log[current_guard] = temp
    }
  }

  // A
  guard_A, total_A, minute_A := 0, 0, 0
  // B
  guard_B, minute_B, minute_value_B := 0, 0, 0

  for key, value := range log {
    total, minute, minute_value := 0, 0, 0

    for m_i, m_v := range value {
      total += m_v

      if m_v > minute_value {
        minute_value = m_v
        minute = m_i
      }

      if m_v > minute_value_B {
        guard_B = key
        minute_value_B = m_v
        minute_B = m_i
      }
    }

    if total > total_A {
      guard_A = key
      total_A = total
      minute_A = minute
    }
  }

  fmt.Printf("\nGuard A: #%d @ %d Minutes (%d)", guard_A, minute_A, guard_A * minute_A)
  fmt.Printf("\nGuard B: #%d @ %d Minutes (%d)", guard_B, minute_B, guard_B * minute_B)
}
