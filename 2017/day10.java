import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;

public class Main {
  private static int[] data = new int[256];
  private static int current = 0;
  private static int skip = 0;
  
  public static void main(String[] args) {
    for (int a = 0; a < data.length; a++) {
      data[a] = a;
    }
    
    try {
      Path toInput = Paths.get("input");
      
      // A
      //int[] input = Arrays.stream(Files.readAllLines(toInput).get(0).split(",")).mapToInt(v -> Integer.parseInt(v)).toArray();
      //int runs = 1;
      
      // B
      int[] temp = Files.readAllLines(toInput).get(0).chars().map(v -> (int)v).toArray();
      int[] input = getInput(temp);
      int runs= 64;
      
      computeHash(input, runs);
      
      System.out.println("Hash: " + data[0] * data[1]);
      denseHash();
    } 
    catch (IOException e) {}
  }
  
  private static void computeHash(int[] input, int runs) {
    for (int r = 0; r < runs; r++) {
      for (int val : input) {
        int[] copy = new int[val];
        
        for (int i = 0; i < val; i++) {
          copy[i] = data[(current + i) % data.length];
        }
        
        for (int i = copy.length; i > 0; i--) {
          data[(current + (copy.length - i)) % data.length] = copy[i - 1];
        }
        
        current = (current + val + skip) % data.length;
        skip++;
      }
    }
  }
  
  private static void denseHash() {
    int[] hash = new int[16];
    for (int i = 0; i < 16; i++) {
      int[] current = Arrays.copyOfRange(data, i * 16, (i + 1) * 16);
      hash[i] = Arrays.stream(current).reduce((x, y) -> x ^ y).getAsInt();
    }
    
    Arrays.stream(hash).forEach(h -> System.out.print(Integer.toHexString(h)));
    System.out.print("\n");
  }
  
  private static int[] getInput(int[] temp) {
    int[] input = Arrays.copyOf(temp, temp.length + 5);
      
    input[input.length - 5] = 17;
    input[input.length - 4] = 31;
    input[input.length - 3] = 73;
    input[input.length - 2] = 47;
    input[input.length - 1] = 23;
    
    return input;
  }
}
