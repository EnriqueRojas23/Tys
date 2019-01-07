﻿

namespace Web.TYS.Security
{
    using System;
    using System.Linq;
    using System.Web.Security;

    using Seguridad.Common;

    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public string NombreUsuario { get; set; }

        public long? IdUserPassword { get; set; }

        public int? UserRoleId { get; set; }

        public string UserRoleName { get; set; }

        #endregion

        public CustomMembershipUser(Usuario user)
            : base("CustomMembershipProvider", user.CodigoUsuario, user.Idusuario, user.EmailUsuario, user.RecordatorioPwd, string.Empty, user.IsApproved, user.IsLockedout, user.FechaRegistro, user.LastLoginDate, user.LastActivityDate, DateTime.Now, user.LastLockedOutDate)
        {
            NombreUsuario = user.NombreUsuario;
            IdUserPassword = user.IdUsuarioPwd;

            if (user.Perfiles.Any())
            {
                UserRoleId = user.Perfiles.Last().IdPerfil;
                UserRoleName = user.Perfiles.Last().NombrePerfil;
            }
        }

    }
}