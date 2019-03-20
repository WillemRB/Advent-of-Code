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
      
      int[] results = Files.readAllLines(toInput).stream().mapToInt(stream -> processStream(stream)).toArray();
      
      Arrays.stream(results).forEach(r -> System.out.println("Score: " + r));
    } 
    catch (IOException e) {}
  }
  
  public static int processStream(String stream) {
    int open = 0, score = 0, index = 0, garbageScore = 0;
    boolean garbage = false;
    
    char[] data = stream.toCharArray();
    
    while (index < data.length) {
      char current = data[index];
      if (current == '!') { 
        ++index;
      }
      else if (current == '<' && !garbage) {
        garbage = true;
      }
      else if (current == '>' && garbage) {
        garbage = false;
      }
      else if (garbage) {
        ++garbageScore;
      }
      else if (current == '{' && !garbage) {
        ++open;
      }
      else if (current == '}' && !garbage) {
        score += open;
        --open;
      }
      
      ++index;
    }
    
    // A
    //return score;
    
    // B
    return garbageScore;
  }
}
