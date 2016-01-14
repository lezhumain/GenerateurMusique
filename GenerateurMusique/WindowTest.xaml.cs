using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace GenerateurMusique
{
    /// <summary>
    /// Logique d'interaction pour WindowTest.xaml
    /// </summary>
    public partial class WindowTest : Window
    {
        public ObservableCollection<GenerationTest> Geners = new ObservableCollection<GenerationTest>(); 

        public WindowTest()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                Geners.Add(
                    new GenerationTest
                    {
                        GName = "Generation" + i,
                        Indivs = new ObservableCollection<IndividuTest>
                        {
                            new IndividuTest {IName = "Indiv1"},
                            new IndividuTest {IName = "Indiv2"},  
                            new IndividuTest {IName = "Indiv3"}   
                        }
                    }    
                );
            }

            MainList.ItemsSource = Geners;
        }
    }

    public class GenerationTest
    {
        public ObservableCollection<IndividuTest> Indivs = new ObservableCollection<IndividuTest>();
        public string GName { get; set; }
    }

    public class IndividuTest
    {
        public string IName { get; set; }
    }
}
