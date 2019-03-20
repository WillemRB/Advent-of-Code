public class Main {
    public static void main(String[] args) {
      // 1A
      System.out.println(Captcha("1122")); // 3
      System.out.println(Captcha("1111")); // 4
      System.out.println(Captcha("1234")); // 0
      System.out.println(Captcha("91212129")); // 9
      // 1B
      System.out.println(CaptchaWithStep("1212")); // 6
      System.out.println(CaptchaWithStep("1221")); // 0
      System.out.println(CaptchaWithStep("123425")); // 4
      System.out.println(CaptchaWithStep("123123")); // 12
      System.out.println(CaptchaWithStep("12131415")); // 4
    }
    
    public static int Captcha(String input) {
      return Solve(input, 1);
    }
    
    public static int CaptchaWithStep(String input) {
      int step = input.length() / 2;
      return Solve(input, step);
    }
    
    public static int Solve(String input, int step) {
      int result = 0;
      
      for (int index = 0; index < input.length(); index++) {
        char first = input.charAt(index);
        char second = input.charAt((index + step) % input.length());
        
        if (first == second)
          result += Character.getNumericValue(first);
      }
      
      return result;
    }
}
