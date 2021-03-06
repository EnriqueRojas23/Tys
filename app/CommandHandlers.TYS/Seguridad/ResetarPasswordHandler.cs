﻿using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Seguridad;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguridad;
using Domain.TYS.Seguridad.Exceptions;
using QueryHandlers.TYS.Seguridad;
using QueryContracts.TYS.Seguridad.Parameters;
using QueryContracts.TYS.Seguridad.Result;
using CommandContracts.TYS.Seguridad.Output;

namespace CommandHandlers.TYS.Seguridad
{
    public class ResetarPasswordHandler : ICommandHandler<ResetarPasswordCommand>
    {
        private readonly IRepository<Usuario> _usuario;
        public ResetarPasswordHandler(IRepository<Usuario> pusuario)
        {
            this._usuario = pusuario;
        }

        public CommandResult Handle(ResetarPasswordCommand command)
        {
            var dominio_usuario = _usuario.Get(x => x.usr_int_id == command.usr_int_id).LastOrDefault();
            if (dominio_usuario == null) throw new UsuarioException("No se encontro el usuario");

            dominio_usuario.usr_dat_fecvctopwd = DateTime.Now.AddDays(30);
            dominio_usuario.usr_int_numintentos = 0;

            var nuevopassword = GenerarCadena();

            var query = new EncriptarPasswordQuery();
            var parameter = new EncriptarPasswordParameter();
            parameter.usr_int_id = command.usr_int_id;
            parameter.usr_str_password = nuevopassword;
            var result = (EncriptarPasswordResult)query.Handle(parameter);

            _usuario.Commit();
            return new ResetarPasswordOutput() { Correo= dominio_usuario.usr_str_email
                , Nombres = dominio_usuario.usr_str_nombre
                , Usuario = dominio_usuario.usr_str_red
                ,PasswordClaro = nuevopassword };
            
        }

        private string GenerarCadena()
        { 
            Random obj = new Random();
    	    string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
	        int longitud = posibles.Length;
	        char letra;
	        int longitudnuevacadena = 6;
	        string nuevacadena = "";
	
            for (int i = 0; i < longitudnuevacadena; i++)
	        {
	            letra = posibles[obj.Next(longitud)];
	            nuevacadena += letra.ToString();
	        }
            return nuevacadena;
        }
    }
}
