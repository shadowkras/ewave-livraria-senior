using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaVirtual.Application.Enums
{
    /// <summary>
    /// Classe com permissões de usuários.
    /// </summary>
    public static class UserClaims
    {
        public const string AccessAdministrativeTools = nameof(AccessAdministrativeTools);
        public const string AccessModerationTools = nameof(AccessModerationTools);
    }
}
