using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using AppS7KSanchezA.Models;
using System.Collections.ObjectModel;

namespace AppS7KSanchezA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tablaEstuadiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            consulta();
        }

        public async void consulta()
        {
            var registros = await con.Table<Estudiante>().ToListAsync();
            tablaEstuadiantes = new ObservableCollection<Estudiante>(registros);
            ListaUsuarios.ItemsSource = tablaEstuadiantes;
        }

        private void ListaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Estudiante)e.SelectedItem;
            var item = Obj.Id.ToString();
            int Id = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Elemento(Id));
            }
            catch (Exception ex)
            {

            }
        }
    }
}