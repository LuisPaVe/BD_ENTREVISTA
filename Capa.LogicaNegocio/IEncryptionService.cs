using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.LogicaNegocio
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Decrypt(string cipher);
    }
}
