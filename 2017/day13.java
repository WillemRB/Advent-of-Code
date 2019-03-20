import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
 
public class Main {
  private static Map<Integer, Integer> scanners = new HashMap<Integer, Integer>();
  private static int maxLayer = 0;
 
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
     
      Files.lines(toInput).forEach(line -> {
        int layer = Integer.parseInt(line.split(":")[0].trim());
        int range = Integer.parseInt(line.split(":")[1].trim());
        scanners.put(layer, range);
        maxLayer = Math.max(maxLayer, layer);
      });
      
      int delay = 0;
      int severity = travel(delay);
      
      // A
      //System.out.println("Severity: " + severity);
      
      // B
      while (severity > 0) {
        delay++;
        severity = travel(delay);
      }
      
      System.out.println("Delay to not get caught: " + delay);
    }
    catch (IOException e) {}
  }
  
  private static int travel(int step) {
    int severity = 0;
    
    for (int layer = 0; layer <= maxLayer; layer++) {
      int range = scanners.getOrDefault(layer, 0);
      
      if (range > 0) {
        int roundtrip = (2 * range) - 2;
        if (step % roundtrip == 0) {
          // A
          //severity += layer * range;
          // B : Return any non-zero value
          return 1;
        }
      }
      
      step++;
    }
    
    return severity;
  }
}
