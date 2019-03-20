import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
import java.util.stream.Collectors;

public class Main {
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      
      String data = Files.lines(toInput).findFirst().get();
      int[] values = Arrays.stream(data.split("\t")).mapToInt(n-> Integer.parseInt(n)).toArray();
      int result = debug(values);
      
      System.out.println("Iterations: " + result);
    } 
    catch (IOException e) {}
  }
  
  private static ArrayList<String> cache = new ArrayList();
  
  public static int debug(int[] values) {
    int iterations = 0;
    
    while (true) {
      String str = getString(values);
      
      if (cache.contains(str)) {
        int cycle = iterations - cache.indexOf(str);
        System.out.println("Cycle: " + cycle);
        break;
      }
      
      iterations++;
      cache.add(str);
      
      int max = Arrays.stream(values).max().getAsInt();
      int indexOfMax = 0;
      
      ArrayList<Integer> indices = new ArrayList();
      for (int i = 0; i < values.length; i++) {
        if (values[i] == max) {
          indexOfMax = i;
          break;
        }
      }
      
      int val = values[indexOfMax];
      values[indexOfMax] = 0;
      
      int x = (indexOfMax + 1) % values.length;
      while (val > 0) {
        values[x]++;
        x = ++x % values.length;
        val--;
      }
    }
    
    return iterations;
  }
  
  public static String getString(int[] values) {
    return Arrays.stream(values)
        .mapToObj(String::valueOf)
        .collect(Collectors.joining(","));
  }
}
