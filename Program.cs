using System;

namespace PasswordHashing
{
    class Program
    {
        
        public Program()
        {
           
        }
        static void Main(string[] args)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            Console.WriteLine("Write some text to hash.");
            string text = Console.ReadLine();
            string hash = passwordHasher.Hash(text);
            Console.WriteLine(hash);

            Console.WriteLine(passwordHasher.Check(hash, text));

            Console.ReadLine();
        }
    }
}
