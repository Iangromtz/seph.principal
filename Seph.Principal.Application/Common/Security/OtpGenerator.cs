using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Common.Security
{
    public static class OtpGenerator
    {
        private const string Allowed = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";

        /// <summary>
        /// Genera un código OTP alfanumérico de una longitud específica utilizando caracteres permitidos.
        /// </summary>
        /// <param name="length">La longitud del código OTP a generar.</param>
        /// <returns>Un código OTP alfanumérico generado.</returns>
        public static string Generate(int length = 5)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            var chars = new char[length];
            for (var i = 0; i < length; i++)
            {
                chars[i] = Allowed[bytes[i] % Allowed.Length];
            }
            return new string(chars);
        }
    }
}
