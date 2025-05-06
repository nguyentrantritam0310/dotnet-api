using Microsoft.AspNetCore.Identity;
using dotnet_api.Data.Entities;

public class HashGenerator
{
    public static void Main()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser();

        Console.WriteLine("=== Password Hashes for Seed Data ===");
        
        Console.WriteLine("\nAdmin (giamdoc@123):");
        Console.WriteLine(hasher.HashPassword(user, "giamdoc@123"));

        Console.WriteLine("\nManager (chihuy@123):");
        Console.WriteLine(hasher.HashPassword(user, "chihuy@123"));

        Console.WriteLine("\nTechnical Staff (kythuat@123):");
        Console.WriteLine(hasher.HashPassword(user, "kythuat@123"));

        Console.WriteLine("\nWorker (tho@123):");
        Console.WriteLine(hasher.HashPassword(user, "tho@123"));
    }
} 