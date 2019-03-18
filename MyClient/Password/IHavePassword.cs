using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.Password
{

    /// <summary>
    /// Интерфейс для безопасного получения пароля
    /// </summary>

    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
