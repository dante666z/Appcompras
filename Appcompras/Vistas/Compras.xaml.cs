using Appcompras.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appcompras.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Compras : ContentPage
    {
        VMcompras vm;
        public Compras()
        {
            InitializeComponent();
            vm = new VMcompras(Navigation, Carrilderecha, Carrilizquierda);
            //BindingContext = new VMcompras(Navigation, Carrilderecha, Carrilizquierda); para cuando es collectionview
            BindingContext = vm;
            this.Appearing += Compras_Appearing;
        }
        // para cuando es un lista
        private async void Compras_Appearing(object sender, EventArgs e)
        {
            await vm.MostrarVistapreviaDc();
            await vm.MostrarDetalleC();
            await vm.Sumarcantidades();
        }

        private async void DeslizarPanelContador(object sender, SwipedEventArgs e)
        {
            await vm.MostrarpanelDc(gridProductos, Paneldetallecompra, Panelcontador);
        }

        private async void DeslizarPanelDeatalleCompra(object sender, SwipedEventArgs e)
        {
            await vm.MostrargridProductos(gridProductos, Paneldetallecompra, Panelcontador);
        }
    }
}