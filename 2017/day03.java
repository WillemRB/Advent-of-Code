import java.lang.Math;

public class Main {
  public static void main(String[] args) {
    System.out.println(distance(1));
    System.out.println(distance(12));
    System.out.println(distance(23));
    System.out.println(distance(1024));
  }
  
  public static int distance(int input) {
    if (input == 1) { return 0; }
    
    int total = 1;
    // Determine ring
    int ring = 0;
    while (total < input)
      total += ++ring * 8;
    
    // Determine side
    int side = total - input;
    
    // Determine the number of steps to the middle
    int pos = Math.abs(ring - (side % (ring * 2)));
    
    return ring + pos;
  }
}
