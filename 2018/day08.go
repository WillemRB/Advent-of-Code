package main

import (
  "fmt"
  "io/ioutil"
  "strings"
  "strconv"
)

type Node struct {
  childCount int
  metaCount int
  children []Node
  metadata []int
}

func main() {
  content, _ := ioutil.ReadFile("input")
  lines := strings.Split(string(content), " ")

  data := make([]int, len(lines))
  head := 0

  for idx, line := range lines {
    val, _ := strconv.Atoi(line)
    data[idx] = val
  }

  tree, head := CreateTree(head, data)

  // A
  fmt.Println("Sum of metadata:", MetadataSum(tree))
  // B
  fmt.Println("Root node value: ", RootNodeValue(tree))
}

func CreateTree(head int, data []int) (Node, int) {
  childCount := data[head]
  head++
  metaCount := data[head]
  head++

  node := Node{ childCount: childCount, metaCount: metaCount }
  children := make([]Node, 0)

  for c := 0; c < childCount; c++ {
    child, new_head := CreateTree(head, data)
    head = new_head
    children = append(children, child)
  }

  node.children = children
  node.metadata = data[head:head+metaCount]
  head = head + metaCount

  return node, head
}

func MetadataSum(node Node) int {
  sum := 0

  for _, val := range node.metadata {
    sum += val
  }

  for _, n := range node.children {
    sum += MetadataSum(n)
  }

  return sum
}

func RootNodeValue(node Node) int {
  sum := 0

  if node.childCount == 0 {
    for _, val := range node.metadata {
      sum += val
    }
  } else {
    for _, idx := range node.metadata {
      if idx <= node.childCount {
        sum += RootNodeValue(node.children[idx-1])
      }
    }
  }

  return sum
}
