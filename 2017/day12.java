import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
 
public class Main {
  private static Map<Integer, String> pipes = new HashMap<Integer, String>();
  private static List<Integer> plumbing = new ArrayList<Integer>();
 
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
     
      Files.lines(toInput).forEach(line -> {
        int key = Integer.parseInt(line.split("<->")[0].trim());
        pipes.put(key, line.split("<->")[1].trim());
      });
      
      buildPlumbing(0);
      
      System.out.println("Contained: " + plumbing.size());
    }
    catch (IOException e) {}
  }
  
  private static void buildPlumbing(int index) {
    String[] parts = pipes.get(index).split(", ");

    for (String part : parts) {
      int value = Integer.parseInt(part);
      if (!plumbing.contains(value)) {
        plumbing.add(value);
        buildPlumbing(value);
      }
    }
  }
}
