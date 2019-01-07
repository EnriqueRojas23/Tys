
using FluentValidation;
namespace CommandContracts.TYS.Contenedor.Validators
{
    public class ReservaContenedorValidator : AbstractValidator<ReservaContenedorCommand>
    {
        public ReservaContenedorValidator()
        {

            //
            //--| MGZP   | Inicio
            //--| Fecha  : 16/09/2016 09:38 a.m.
            //--| Motivo : Validar campos obligatorios 
            //--|          - Agregar Validaciones
            //--|          - Modificar Validaciones
            //--|          - Eliminar Validaciones
            //--|          
            //--| 01. Validaciones - Pestaña [Datos Basicos]
            RuleFor(x => x.rb_str_depot).NotNull().WithMessage("El Deposito es obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_oficina).NotNull().WithMessage("La Oficina es Obligatoria (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_numero_booking).NotNull().WithMessage("El Booking es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_cliente).NotNull().WithMessage("La Linea Naviera es Obligaria (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_buque).NotNull().WithMessage("La Nave es Obligatoria (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_viaje).NotNull().WithMessage("El Viaje es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_puerto_origen).NotNull().WithMessage("El Pto Origen es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_puerto_destino).NotNull().WithMessage("El Pto Destino es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_puerto_destino_final).NotNull().WithMessage("El Pto Destino Final es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_str_codigo_agente_carga).NotNull().WithMessage("El Agente de Carga es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_int_espacios).NotNull().WithMessage("El Nro de Espacios es Obligatorio (Pestaña Datos Básico).");
            RuleFor(x => x.rb_int_espacios).GreaterThan(0).WithMessage("El Nro de Espacios debe ser mayor a 0 (Pestaña Datos Básico).");
            //--| 
            //--| 02. Validaciones - Pestaña [Mercancias]
            RuleFor(x => x.rb_str_mercancia).NotNull().WithMessage("La Mercancia es Obligatoria (Pestaña Mercancias.");
            RuleFor(x => x.rb_dec_peso).NotNull().WithMessage("El Peso de la Mercancia es Obligatoria (Pestaña Mercancias.");
            //
            //RuleFor(x => x.rb_str_producto).NotNull().WithMessage("Se requiere que ingrese el producto.");
            //RuleFor(x => x.rb_str_subproducto).NotNull().WithMessage("Se requiere que ingrese el Subproducto."); // --| Comentado por MGZP
        }
    }
}
