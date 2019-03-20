package main

import (
  "bufio"
  "fmt"
  "os"
  "strconv"
  "strings"
)

func main() {
  frequency := 0
  duplicateFound := false
  hist := map[int]bool { frequency: true }
  
  for {
    file, _ := os.Open("input")
    defer file.Close()

    scanner := bufio.NewScanner(file)
    for scanner.Scan() {
      change, _ := strconv.Atoi(strings.Replace(scanner.Text(), "+", "", -1))
      frequency += change
      
      if hist[frequency] {
        duplicateFound = true
        break
      }
      
      hist[frequency] = true
    }

    if duplicateFound { break }
  }

  fmt.Printf("Frequency: %d", frequency)
}
