﻿

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguridad
{
    public class AsignarClientesUsuariosCommand : Command
    {
        public int usr_int_id { get; set; }
        public string[] clientes_array { get; set; }
    }
}
