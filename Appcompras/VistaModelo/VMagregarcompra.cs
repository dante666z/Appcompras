﻿using Appcompras.Datos;
using Appcompras.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appcompras.VistaModelo
{
    class VMagregarcompra : BaseViewModel
    {
        #region VARIABLES
        int _Cantidad;
        string _Preciotexto;
        public Mproductos Parametrosrecibe { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMagregarcompra(INavigation navigation, Mproductos parametrosTrae)
        {
            Navigation = navigation;
            Parametrosrecibe = parametrosTrae;
            Preciotexto = "$" + Parametrosrecibe.Precio;
        }
        #endregion
        #region OBJETOS
        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        }
        public string Preciotexto
        {
            get { return _Preciotexto; }
            set { SetValue(ref _Preciotexto, value); }
        }
        #endregion

        #region PROCESOS
        public async Task Validarcompra()
        {
            var funcion = new Ddetallecompras();
            var listaXidproducto = await funcion.MostrarDcXidproducto(Parametrosrecibe.Idproducto);
            if (listaXidproducto.Count > 0)
            {
                await EditarDc();
            }
            else
            {
                await InsertarDc();
            }
        }
        public async Task EditarDc()
        {
            if(Cantidad < 1)
            {
                Cantidad = 1;
            }
            var funcion = new Ddetallecompras();
            var parametros = new Mdetallecompras();
            parametros.Cantidad = Cantidad.ToString();
            parametros.Idproducto = Parametrosrecibe.Idproducto;
            parametros.Preciocompra = Parametrosrecibe.Precio;
            await funcion.Editardetalle(parametros);
            await Volver();
        }
        public async Task InsertarDc()
        {
            if(Cantidad == 0)
            {
                Cantidad = 1;
            }
            var funcion = new Ddetallecompras();
            var parametros = new Mdetallecompras();
            parametros.Cantidad = Cantidad.ToString();
            parametros.Idproducto = Parametrosrecibe.Idproducto;
            parametros.Preciocompra = Parametrosrecibe.Precio;
            double total = 0;
            double preciocompra = Convert.ToDouble(Parametrosrecibe.Precio);
            double cantidad = Convert.ToDouble(Cantidad);
            total = cantidad * preciocompra;
            parametros.Total = total.ToString();
            await funcion.InsertarDc(parametros);
            await Volver();
        }
        public async Task Volver()
        {
            await Navigation.PopAsync();
        }
        public void Aumentar()
        {

            Cantidad++;

        }
        public void Disminuir()
        {
            if (Cantidad > 0)
            {
                Cantidad -= 1;
            }
        }
        #endregion

        #region COMANDOS
        public ICommand VolverCommand => new Command(async () => await Volver());
        public ICommand AumentarCommand => new Command(Aumentar);
        public ICommand DisminuirCommand => new Command(Disminuir);
        public ICommand InsertarCommand => new Command(async () => await Validarcompra());
        #endregion
    }
}
