package main

import (
  "fmt"
  "io/ioutil"
  "strings"
  "sort"
)

type Node struct {
  id string
  before []string
  required []string
  finished bool
  time_to_finish int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), "\n")

  nodes := ConvertToNodes(lines)

  // A
  //PartA(nodes)
  
  // B
  PartB(nodes)
}

func PartA(nodes map[string]Node) {
  result := make([]string, 0)
  queue := GetStartNodes(nodes)

  for len(queue) > 0 {
    for idx, val := range queue {
      current_node := nodes[val]

      if current_node.finished {
        queue = Append(queue[:idx], queue[idx+1:])
        break
      }

      if RequirementsMet(current_node, nodes) {
        current_node.finished = true
        result = append(result, current_node.id)
        nodes[current_node.id] = current_node
        
        queue = Append(queue[:idx], queue[idx+1:])
        queue = Append(queue, current_node.before)
        sort.Strings(queue)
        break
      }
    }
  }

  fmt.Printf("Step Order: %s\n", strings.Join(result, ""))
}

func PartB(nodes map[string]Node) {
  fmt.Println(nodes)
  time_elapsed := 0
  worker_limit := 5
  workers := make(map[string]int)
  queue := GetStartNodes(nodes)

  for len(queue) > 0 || len(workers) > 0 {
    fmt.Println(queue)
    fmt.Println(workers)
    fmt.Println(time_elapsed)
    current_node := Node{}

    if len(workers) > 0 {
      time_elapsed++
    }

    for k, v := range workers {
      workers[k] = v - 1
      if workers[k] == 0 {
        worker_node := nodes[k]
        worker_node.finished = true
        nodes[worker_node.id] = worker_node
        
        delete(workers, k)
      }
    }

    for c := 0; c < worker_limit; c++ {
      if len(workers) == worker_limit {
        break
      }

      for idx, val := range queue {
        current_node = nodes[val]

        _, working := workers[current_node.id]

        if current_node.finished || working {
          queue = Append(queue[:idx], queue[idx+1:])
          break
        } 
        
        if RequirementsMet(current_node, nodes) {
          workers[current_node.id] = current_node.time_to_finish

          queue = Append(queue[:idx], queue[idx+1:])
          queue = Append(queue, current_node.before)
          sort.Strings(queue)
          break
        }
      }
    }
  }

  fmt.Printf("Time elapsed: %d", time_elapsed)
}

func ConvertToNodes(lines []string) map[string]Node {
  nodes := make(map[string]Node, 0)
  
  for _, line := range lines {
    split := strings.Split(string(line), " ")

    n1 := split[1]
    n2 := split[7]

    from_node, found := nodes[n1]
    if !found {
      from_node = Node{ n1, make([]string, 0), make([]string, 0), false, int([]byte(n1)[0] - 4) }
    }
    from_node.before = append(from_node.before, n2)

    to_node, found := nodes[n2]
    if !found {
      to_node = Node{ n2, make([]string, 0), make([]string, 0), false, int([]byte(n2)[0] - 4) }
    }
    to_node.required = append(to_node.required, n1)

    nodes[n1] = from_node
    nodes[n2] = to_node
  }

  return nodes
}

func GetStartNodes(nodes map[string]Node) []string {
  start_nodes := make([]string, 0)
  for _, node := range nodes {
    if len(node.required) == 0 {
      start_nodes = append(start_nodes, node.id)
    }
  }
  sort.Strings(start_nodes)
  return start_nodes
}

func Append(slice []string, to_add []string) []string {
  for _, value := range to_add {
    exists := false
    for _, v := range slice {
      if v == value {
        exists = true
      }
    }

    if !exists {
      slice = append(slice, value)
    }
  }
  return slice
}

func RequirementsMet(node Node, nodes map[string]Node) bool {
  all_met := true
  for _, req := range node.required {
    all_met = all_met && nodes[req].finished
  }
  return all_met
}
