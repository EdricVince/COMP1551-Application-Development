using System;
using System.Text;

namespace COMP1551
{
    class Program
    {
        static void Main()
        {
            // Main program loop to allow continuous operation
            while (true)
            {
                Console.WriteLine("\n=== String Encoding Program ===");
                string choice;

                // Show menu of encoding choices
                do
                {
                    Console.WriteLine("\nChoose encoding type:");
                    Console.WriteLine("0. Demonstration");
                    Console.WriteLine("1. Caesar Cipher");
                    Console.WriteLine("2. Atbash Cipher");
                    Console.WriteLine("3. Swap Adjacent Characters");
                    Console.WriteLine("4. Vowel Replacement");
                    Console.WriteLine("5. Mirror String");
                    Console.Write("Your choice (0-5): ");
                    choice = Console.ReadLine();

                    if (choice != "0" && choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
                    {
                        Console.WriteLine("Invalid choice! Please choose a number between 0 and 5.");
                    }
                } while (choice != "0" && choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5");

                // Get input string
                string s;
                while (true)
                {
                    Console.Write("Enter a string (uppercase letters only, max 40 characters): ");
                    s = Console.ReadLine();
                    try
                    {
                        // Create a temporary processor to validate input
                        var tempProcessor = new StringProcessing(s, 0);
                        break; // If no exception, input is valid
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Invalid input! {ex.Message}");
                    }
                }

                // Handle the chosen encoding type
                CipherProcessor processor = null;
                int n = 0;

                switch (choice)
                {
                    case "0": // Demonstration mode
                        Console.WriteLine("\n--- DEMONSTRATION ---");

                        // Prompt for N for Caesar Cipher
                        while (true)
                        {
                            Console.Write("Enter shift value N for Caesar Cipher (-25 to 25): ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out n))
                            {
                                try
                                {
                                    // Validate N by creating a temporary processor
                                    var tempProcessor = new StringProcessing(s, n);
                                    break;
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine($"Invalid number! {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                            }
                        }

                        // Caesar Cipher with user-defined shift
                        processor = new StringProcessing(s, n);
                        Console.WriteLine($"\nCaesar Cipher (N={n}): {processor.Print()}");
                        Console.WriteLine($"Decoded: {processor.Decode()}");
                        Console.WriteLine($"Sorted Input: {processor.Sort()}");
                        Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                        Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");

                        // Atbash Cipher
                        processor = new AtbashProcessor(s);
                        Console.WriteLine($"\nAtbash Cipher: {processor.Print()}");
                        Console.WriteLine($"Decoded: {processor.Decode()}");
                        Console.WriteLine($"Sorted Input: {processor.Sort()}");
                        Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                        Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");

                        // Swap Adjacent Characters
                        processor = new AdjacentSwapProcessor(s);
                        Console.WriteLine($"\nSwap Adjacent Characters: {processor.Print()}");
                        Console.WriteLine($"Decoded: {processor.Decode()}");
                        Console.WriteLine($"Sorted Input: {processor.Sort()}");
                        Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                        Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");

                        // Vowel Replacement
                        processor = new VowelReplacerProcessor(s);
                        Console.WriteLine($"\nVowel Replacement: {processor.Print()}");
                        Console.WriteLine($"Decoded: {processor.Decode()}");
                        Console.WriteLine($"Sorted Input: {processor.Sort()}");
                        Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                        Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");

                        // Mirror String
                        processor = new MirrorProcessor(s);
                        Console.WriteLine($"\nMirror String: {processor.Print()}");
                        Console.WriteLine($"Decoded: {processor.Decode()}");
                        Console.WriteLine($"Sorted Input: {processor.Sort()}");
                        Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                        Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");
                        break;

                    case "1": // Caesar Cipher
                        while (true)
                        {
                            Console.Write("Enter shift value N (-25 to 25): ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out n))
                            {
                                try
                                {
                                    processor = new StringProcessing(s, n);
                                    break;
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine($"Invalid number! {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                            }
                        }
                        break;

                    case "2": // Atbash Cipher
                        processor = new AtbashProcessor(s);
                        break;

                    case "3": // Swap Adjacent Characters
                        processor = new AdjacentSwapProcessor(s);
                        break;

                    case "4": // Vowel Replacement
                        processor = new VowelReplacerProcessor(s);
                        break;

                    case "5": // Mirror String
                        processor = new MirrorProcessor(s);
                        break;
                }

                // Display results for non-demonstration mode
                if (choice != "0")
                {
                    Console.WriteLine($"\nEncoded result: {processor.Print()}");
                    Console.WriteLine($"Decoded: {processor.Decode()}");
                    Console.WriteLine($"Sorted Input: {processor.Sort()}");
                    Console.WriteLine($"ASCII of Input: {string.Join(" ", processor.InputCode())}");
                    Console.WriteLine($"ASCII of Output: {string.Join(" ", processor.OutputCode())}");

                    // Verify decoding for Caesar Cipher
                    if (choice == "1")
                    {
                        var decodeProcessor = new StringProcessing(processor.Print(), -n);
                        string decoded = decodeProcessor.Print();
                        Console.WriteLine($"Verification: Decoding encoded string with N={-n}: {decoded}");
                        if (decoded == s)
                            Console.WriteLine("Verification: Decoded string matches original input.");
                        else
                            Console.WriteLine("Verification: Decoded string does NOT match original input.");
                    }
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear(); // Clear console for next iteration
            }
        }
    }

    // Abstract base class for all cipher processors
    abstract class CipherProcessor
    {
        private string userInput;
        public string UserInput
        {
            get => userInput;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 40 || !value.All(c => char.IsUpper(c) && char.IsLetter(c)))
                    throw new ArgumentException("Input must be uppercase letters only, max 40 characters.");
                userInput = value;
            }
        }

        protected string encodedString; // Store encoded result

        public CipherProcessor(string input)
        {
            UserInput = input; // Validate input via Property
        }

        public abstract string Encode();
        public abstract string Decode();
        public abstract int[] InputCode();
        public abstract int[] OutputCode();

        // Return the encoded string
        public virtual string Print() => Encode();

        // Sort the input string alphabetically
        public virtual string Sort()
        {
            char[] chars = UserInput.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }

    // Class for Caesar Cipher
    class StringProcessing : CipherProcessor
    {
        private int shiftNumber;
        public int ShiftNumber
        {
            get => shiftNumber;
            private set
            {
                if (value < -25 || value > 25)
                    throw new ArgumentException("Shift must be between -25 and 25.");
                shiftNumber = value;
            }
        }

        public StringProcessing(string input, int shift) : base(input)
        {
            ShiftNumber = shift;
            encodedString = Encode();
        }

        public override string Encode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < UserInput.Length; i++)
            {
                char letter = UserInput[i];
                int newCode = letter - 'A';
                newCode = (newCode + ShiftNumber + 26) % 26;
                result.Append((char)(newCode + 'A'));
            }
            return result.ToString();
        }

        public override string Decode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < UserInput.Length; i++)
            {
                char letter = UserInput[i];
                int newCode = letter - 'A';
                newCode = (newCode - ShiftNumber + 26) % 26;
                result.Append((char)(newCode + 'A'));
            }
            return result.ToString();
        }

        public override int[] InputCode()
        {
            int[] codes = new int[UserInput.Length];
            for (int i = 0; i < UserInput.Length; i++)
            {
                codes[i] = (int)UserInput[i];
            }
            return codes;
        }

        public override int[] OutputCode()
        {
            int[] codes = new int[encodedString.Length];
            for (int i = 0; i < encodedString.Length; i++)
            {
                codes[i] = (int)encodedString[i];
            }
            return codes;
        }
    }

    // Class for Atbash Cipher
    class AtbashProcessor : CipherProcessor
    {
        public AtbashProcessor(string input) : base(input)
        {
            encodedString = Encode();
        }

        public override string Encode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < UserInput.Length; i++)
            {
                char letter = UserInput[i];
                int newCode = 'Z' - (letter - 'A');
                result.Append((char)newCode);
            }
            return result.ToString();
        }

        public override string Decode()
        {
            return Encode(); // Atbash is its own inverse
        }

        public override int[] InputCode()
        {
            int[] codes = new int[UserInput.Length];
            for (int i = 0; i < UserInput.Length; i++)
            {
                codes[i] = (int)UserInput[i];
            }
            return codes;
        }

        public override int[] OutputCode()
        {
            int[] codes = new int[encodedString.Length];
            for (int i = 0; i < encodedString.Length; i++)
            {
                codes[i] = (int)encodedString[i];
            }
            return codes;
        }
    }

    // Class for swapping adjacent characters
    class AdjacentSwapProcessor : CipherProcessor
    {
        public AdjacentSwapProcessor(string input) : base(input)
        {
            encodedString = Encode();
        }

        public override string Encode()
        {
            char[] letters = UserInput.ToCharArray();
            for (int i = 0; i < letters.Length - 1; i += 2)
            {
                char temp = letters[i];
                letters[i] = letters[i + 1];
                letters[i + 1] = temp;
            }
            return new string(letters);
        }

        public override string Decode()
        {
            return Encode(); // Swapping twice returns the original
        }

        public override int[] InputCode()
        {
            int[] codes = new int[UserInput.Length];
            for (int i = 0; i < UserInput.Length; i++)
            {
                codes[i] = (int)UserInput[i];
            }
            return codes;
        }

        public override int[] OutputCode()
        {
            int[] codes = new int[encodedString.Length];
            for (int i = 0; i < encodedString.Length; i++)
            {
                codes[i] = (int)encodedString[i];
            }
            return codes;
        }
    }

    // Class for replacing vowels
    class VowelReplacerProcessor : CipherProcessor
    {
        public VowelReplacerProcessor(string input) : base(input)
        {
            encodedString = Encode();
        }

        public override string Encode()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < UserInput.Length; i++)
            {
                char letter = UserInput[i];
                if (letter == 'A' || letter == 'E' || letter == 'I' || letter == 'O' || letter == 'U')
                    result.Append('*');
                else
                    result.Append(letter);
            }
            return result.ToString();
        }

        public override string Decode()
        {
            return UserInput; // Cannot fully decode, return original as placeholder
        }

        public override int[] InputCode()
        {
            int[] codes = new int[UserInput.Length];
            for (int i = 0; i < UserInput.Length; i++)
            {
                codes[i] = (int)UserInput[i];
            }
            return codes;
        }

        public override int[] OutputCode()
        {
            int[] codes = new int[encodedString.Length];
            for (int i = 0; i < encodedString.Length; i++)
            {
                codes[i] = (int)encodedString[i];
            }
            return codes;
        }
    }

    // Class for mirroring the string
    class MirrorProcessor : CipherProcessor
    {
        public MirrorProcessor(string input) : base(input)
        {
            encodedString = Encode();
        }

        public override string Encode()
        {
            StringBuilder reversed = new StringBuilder();
            for (int i = UserInput.Length - 1; i >= 0; i--)
            {
                reversed.Append(UserInput[i]);
            }
            return UserInput + reversed.ToString();
        }

        public override string Decode()
        {
            return UserInput; // First half of encoded string is the original
        }

        public override int[] InputCode()
        {
            int[] codes = new int[UserInput.Length];
            for (int i = 0; i < UserInput.Length; i++)
            {
                codes[i] = (int)UserInput[i];
            }
            return codes;
        }

        public override int[] OutputCode()
        {
            int[] codes = new int[encodedString.Length];
            for (int i = 0; i < encodedString.Length; i++)
            {
                codes[i] = (int)encodedString[i];
            }
            return codes;
        }
    }
}