using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EID.Wrapper;

namespace EIDVoorbeeld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReadEid_Click(object sender, RoutedEventArgs e)
        {
            Wrapper wrapper = new Wrapper();
            CardData data = new CardData();

            data = (CardData)wrapper.GetCardData();

            if (data.FirstCard == null)
            {
                //No card found
                MessageBox.Show("Kaart niet gevonden, controleer aub of de kaart goed in de kaartlezer zit.");
            }
            else
            {
                txtAdress.Text = data.FirstCard.StreetAndNumber;
                txtDateOfBirth.Text = data.FirstCard.BirthDate.ToShortDateString();
                txtName.Text = data.FirstCard.FirstNames;
                txtNationality.Text = data.FirstCard.Nationality;
                txtPlaceOfBirth.Text = data.FirstCard.BirthPlace;
                txtSurName.Text = data.FirstCard.Surname;
                //comment
                //Backcomment
                //Kurt zijn comment samen met uwe voide helloworld
                //ik verander iets
            }
        }

        private void HelloWorld()
        {
            Console.WriteLine("hello");
        }
    }
}
