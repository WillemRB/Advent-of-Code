public class Main {
  public static void main(String[] args) {
    long A = 65;
    long B = 8921;
    
    long factorA = 16807;
    long factorB = 48271;
    
    long modValue = 2147483647;
    
    int count = 0;
    
    String binA = "";
    String binB = "";
    
    for (int it = 0; it < 2000000; it++) {

      A = (A * factorA) % modValue;
      B = (B * factorB) % modValue;
      
      binA = binString(A);
      binB = binString(B);
      
      if (binA.equals(binB))
        count++;
    }
    
    System.out.println("Final count: " + count);
  }
  
  private static String binString(long V) {
    String bin = Integer.toBinaryString((int)V);
    
    if (bin.length() < 16)
      return bin;
    else
      return bin.substring(bin.length() - 16);
  }
}
