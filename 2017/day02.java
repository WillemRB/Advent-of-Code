import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;

public class Main {
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      int result = checksum(Files.readAllLines(toInput));
      System.out.println(result);
    } 
    catch (IOException e) {}
  }
  
  public static int checksum(List<String> input) {
    return input
      .stream()
      .map(line -> {
        int[] values = Arrays
          .stream(line.split("\t"))
          .mapToInt(n -> Integer.parseInt(n))
          .toArray();
        
        // A  
        //return Arrays.stream(values).max().getAsInt() - Arrays.stream(values).min().getAsInt();
        
        // B
        for (int x = 0; x < values.length- 1; x++) {
          for (int y = x + 1; y < values.length; y++) {
            if ((Math.max(values[x], values[y]) % Math.min(values[x], values[y])) == 0)
              return Math.max(values[x], values[y]) / Math.min(values[x], values[y]);
          }
        }
        // Without this the compiler returns an error:
        // error: lambda body is neither value nor void compatible
        return 0;
      })
      .reduce(0, Integer::sum);
  }
}
