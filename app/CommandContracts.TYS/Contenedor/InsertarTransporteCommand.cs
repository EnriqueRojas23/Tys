

using CommandContracts.Common;
namespace CommandContracts.TYS.Contenedor
{
    public class InsertarTransporteCommand : Command
    {
        public long rbd_int_id { get; set; }
        public string rbd_str_ent_cif_transportista { get; set; }
        public string rbd_str_ent_cif_transportista_descripcion { get; set; }

        public string rbd_str_trans_matricula_camion  { get; set; }
        public string rbd_str_trans_nif_conductor { get; set; }
        public string rbd_str_trans_nombre_conductor  { get; set; }
        public string rbd_str_fecha_recojo  { get; set; }
        public string rbd_str_hora_recojo  { get; set; }

    }
}
