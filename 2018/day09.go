package main

import (
  "fmt"
  "container/list"
)

func main() {
  // A
  a := MarbleMania(477, 70851)
  fmt.Println("High score: ", a)

  // B
  b := MarbleMania(477, 7085100)
  fmt.Println("High score: ", b)
  
}

func MarbleMania(players, marbles int) int {
  scores := make([]int, players)

  circle := list.New()
  circle.PushFront(0)
  current := circle.Front()
  current_player := 0

  for marble := 1; marble <= marbles; marble++ {
    if marble % 23 == 0 {
      score := 0
      circle, current, score = Score(circle, current)
      scores[current_player] += marble + score
    } else {
      current = current.Next()
      if current == nil {
        current = circle.Front()
      }
      current = circle.InsertAfter(marble, current)
    }

    current_player++
    current_player = current_player % players
  }

  high_score := 0
  for _, score := range scores {
    if score > high_score {
      high_score = score
    }
  }  
  return high_score
}

func Score(circle *list.List, current *list.Element) (*list.List, *list.Element, int) {
  to_remove := current
  for i := 0; i < 7; i++ {
    to_remove = to_remove.Prev()
    if to_remove == nil {
      to_remove = circle.Back()
    }
  }
  
  score := to_remove.Value.(int)
  current = to_remove.Next()
  circle.Remove(to_remove)

  return circle, current, score
}
