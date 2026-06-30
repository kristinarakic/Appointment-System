using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.SharedKernel;

public static class Ensure
{
    public static void NotNullOrEmpty(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException(message);
    }

    public static void NotNegativeOrZero(decimal value, string message)
    {
        if (value <= 0)
            throw new InvalidOperationException(message);
    }

    public static void NotNegativeOrZero(int value, string message)
    {
        if (value <= 0)
            throw new InvalidOperationException(message);
    }

    public static void ValidEmail(string email, string message)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new InvalidOperationException(message);
    }

    public static void ValidPhone(string phone, string message)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length < 9)
            throw new InvalidOperationException(message);
    }
}