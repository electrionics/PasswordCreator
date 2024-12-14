// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;
internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    { 
        //using var stream = File.OpenText(Path.Combine(Application.StartupPath, "Config.txt"));
        //var keyword = stream.ReadToEnd().Replace("keyword:", string.Empty);
        var keyword = Clipboard.GetText().Replace("keyword:", string.Empty);
        var valid = !string.IsNullOrWhiteSpace(keyword);
        if (!valid)
        {
            Console.WriteLine("access denied");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Let's start security!");

        int counter = 0;
        do
        {
            Console.WriteLine(counter++);
            Console.WriteLine("Enter website entity");
            var websiteEntity = Console.ReadLine();
            Console.WriteLine("Enter user entity or skip, if website is single user");
            var userEntity = Console.ReadLine();

            valid = !string.IsNullOrWhiteSpace(websiteEntity);
            if (!valid)
                continue;

            var algoritm = SHA512.Create();
            var token = $"{websiteEntity}/{userEntity}:{keyword}";

            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = algoritm.ComputeHash(bytes);
            var str = Convert.ToHexString(hash);
            string result = str.Substring(0, 8).ToUpper() + str.Substring(8, 6).ToLower() + "_!";

            Clipboard.SetText(result);
            Console.WriteLine("Ready! Enter '.' to exit.");
        }
        while (Console.ReadLine() != ".");
    }
}