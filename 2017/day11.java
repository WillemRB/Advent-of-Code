import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
import java.util.stream.Collectors;

public class Main {
  private static List<String> path = new ArrayList<String>();
  private static int maxDistance = 0;
  
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      
      int[] results = Files.readAllLines(toInput).stream().mapToInt(line -> hexgridDistance(line.split(","))).toArray();
      
      Arrays.stream(results).forEach(r -> System.out.println("Score: " + r));
    } 
    catch (IOException e) {}
  }
  
  public static int hexgridDistance(String[] directions) {
    path.clear();
    maxDistance = 0;
    
    for (int i = 0; i < directions.length; i++) {
      switch (directions[i]) {
        case "n":
          replace("n", "s", "se", "ne", "sw", "nw");
          break;
        case "ne":
          replace("ne", "sw", "s", "se", "nw", "n");
          break;
        case "se":
          replace("se", "nw", "sw", "s", "n", "ne");
          break;
        case "s":
          replace("s", "n", "nw", "sw", "ne", "se");
          break;
        case "sw":
          replace("sw", "ne", "n", "nw", "se", "s");
          break;
        case "nw":
          replace("nw", "se", "ne", "n", "s", "sw");
          break;
      }
    }
    
    System.out.println("Max Distance: " + maxDistance);
    return path.size();
  }
  
  private static void replace(String curr, String opposite, String left, String leftReplace, String right, String rightReplace) {
    if (path.contains(opposite)) {
      path.remove(opposite);
    }
    else if (path.contains(left)) {
      path.remove(left);
      path.add(leftReplace);
    }
    else if (path.contains(right)) {
      path.remove(right);
      path.add(rightReplace);
    }
    else {
      path.add(curr);
    }
    
    maxDistance = Math.max(maxDistance, path.size());
  }
}
