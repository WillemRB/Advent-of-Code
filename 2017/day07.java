import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
import java.util.regex.*;
 
public class Main {
  private static Map<String,Integer> names = new HashMap<String,Integer>();
  private static Map<String,String> dependencies = new HashMap<String,String>();
 
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      Pattern namePattern = Pattern.compile("[a-z]+");
      Pattern weightPattern = Pattern.compile("\\d+");
     
      Files.lines(toInput).forEach(line -> {
        Matcher nameMatcher = namePattern.matcher(line);
        Matcher weightMatcher = weightPattern.matcher(line);
       
        nameMatcher.find();
        weightMatcher.find();
        Integer weight = Integer.parseInt(weightMatcher.group());
        String parent = nameMatcher.group();
       
        names.put(parent, weight);
       
        while(nameMatcher.find()) {
          dependencies.put(nameMatcher.group(), parent);
        }
      });
     
      names.forEach((k,v) -> Traverse(k));
    }
    catch (IOException e) {}
  }
 
  private static void Traverse(String key) {
    if (dependencies.containsKey(key)) {
      return;
    }
    
    // A
    System.out.println("Root: " + key);

    // B
    computeWeight(getChildren(key));
  }

  private static int computeWeight(List<String> nodes) {
    if (nodes.isEmpty()) {
      return 0;
    }
    
    int[] weights = new int[nodes.size()];
    
    for (int n = 0; n < nodes.size(); n++) {
      weights[n] = names.get(nodes.get(n)) + computeWeight(getChildren(nodes.get(n)));
    }
    
    if (Arrays.stream(weights).distinct().count() > 1) {
      for (int w = 0; w < weights.length - 1; w++) {
        int wPlus1 = (w + 1) % weights.length;
        int wPlus2 = (w + 2) % weights.length;

        if (weights[w] != weights[wPlus1] && weights[w] != weights[wPlus2]) {
          int diff = weights[wPlus1] - weights[w];
          System.out.println("Wrong node: " + nodes.get(w) + " Expected " + (names.get(nodes.get(w)) + diff) + " found " + names.get(nodes.get(w)));
          break;
        }
      }
      
      System.exit(0);
    }

    return Arrays.stream(weights).sum();
  }

  // Should be done with streams...
  private static List<String> getChildren(String node) {
    List<String> filtered = new ArrayList<String>();

    Object[] keySet = dependencies.keySet().toArray();

    for (int di = 0; di < keySet.length; di++) {
      if (dependencies.get(keySet[di]).equals(node)) {
       filtered.add(String.valueOf(keySet[di]));
      }
    }
   
    return filtered;
  }
}
