import java.io.IOException;
import java.lang.String;
import java.nio.charset.Charset;
import java.nio.file.*;
import java.util.*;
import java.util.stream.Collectors;

public class Main {
  private static Map<String,Integer> registers = new HashMap<String,Integer>();
  private static int highest = 0;

  public static void main(String[] args) {
    try {
      Path toInput = Paths.get("input");
      Files.readAllLines(toInput).forEach(i -> process(i));
      
      int result = registers.values().stream().max(Integer::compare).get();
      
      System.out.println("Result: " + result);
      System.out.println("Highest: " + highest);
    } 
    catch (IOException e) {}
  }
  
  private static void process(String instruction) {
    String[] split = instruction.split(" ");
    
    if (shouldRun(split[4], split[5], Integer.parseInt(split[6]))) {
      int value = Integer.parseInt(split[2]);
      if (split[1].equals("inc"))
        registers.merge(split[0], value, Integer::sum);
      else
        registers.merge(split[0], -1 * value, Integer::sum);
        
      highest = Math.max(highest, registers.values().stream().max(Integer::compare).get());
    }
  }
  
  private static boolean shouldRun(String reg, String op, int value) {
    registers.putIfAbsent(reg, 0);
    switch (op) {
      case ">":
        return registers.get(reg) > value;
      case ">=":
        return registers.get(reg) >= value;
      case "<":
        return registers.get(reg) < value;
      case "<=":
        return registers.get(reg) <= value;
      case "!=":
        return registers.get(reg) != value;
      case "==":
        return registers.get(reg) == value;
      default:
        System.out.println("Invalid operation " + op);
        return false;
    }
  }
}
