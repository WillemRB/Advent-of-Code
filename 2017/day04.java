import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;

public class Main {
  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      
      int result = validatePhrases(Files.readAllLines(toInput));
      
      System.out.println(result);
    } 
    catch (IOException e) {}
  }
  
  public static int validatePhrases(List<String> phrases) {
    return phrases.stream()
      .map(phrase -> {
        // Solution 4A
        //String[] words = phrase.split(" ");
        
        // Solution 4B
        Object[] words = Arrays
          .stream(phrase.split(" "))
          .map(w -> {
            char[] sorted = w.toCharArray();
            Arrays.sort(sorted);
            return new String(sorted);
          })
          .toArray();
        
        return (words.length == Arrays.stream(words).distinct().count()) ? 1 : 0;
      })
      .reduce(0, Integer::sum);
  }
}
