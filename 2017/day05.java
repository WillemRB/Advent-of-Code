import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;

public class Main {
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      
      int[] values = Files.lines(toInput).mapToInt(n-> Integer.parseInt(n)).toArray();
      int result = escapeMaze(values);
      
      System.out.println(result);
    } 
    catch (IOException e) {}
  }
  
  public static int escapeMaze(int[] values) {
    int steps = 0, index = 0;
    
    try {
      while(true) {
        int current = values[index];
        
        // A
        //values[index]++;
        
        // B
        if (current >= 3) { values[index]--; } else { values[index]++; }
        
        index += current;
        steps++;
      }
    }
    catch (IndexOutOfBoundsException e) {}
    
    return steps;
  }
}
