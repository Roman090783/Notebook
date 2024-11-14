using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF;

namespace WPF
{
    public partial class MainWindow : Window
    {
        // Private Variable für die aktuell ausgewählte Notiz
        private Notiz _aktuelleNotiz;

        // Öffentliche Eigenschaft für die aktuell ausgewählte Notiz
        public Notiz AktuelleNotiz
        {
            get => _aktuelleNotiz;
            set
            {
                _aktuelleNotiz = value;
                if (_aktuelleNotiz != null)
                {
                    // Notizinhalt in die TextBox laden und die TextBox aktivieren
                    tbxNotiz.Text = _aktuelleNotiz.Inhalt;
                    tbxNotiz.IsEnabled = true;
                    btnLöschen.IsEnabled = true;
                }
                else
                {
                    // TextBox und Löschen-Button deaktivieren, wenn keine Notiz ausgewählt ist
                    tbxNotiz.Text = string.Empty;
                    tbxNotiz.IsEnabled = false;
                    btnLöschen.IsEnabled = false;
                }
            }
        }

        // Konstruktor für das Hauptfenster
        public MainWindow()
        {
            InitializeComponent();

            // ComboBox mit den Werten der Kategorie-Enumeration füllen
            cbxKategorie.ItemsSource = Enum.GetValues(typeof(Kategorie));
            cbxKategorie.SelectedItem = Kategorie.Alle;

            // Testnotizen erstellen
            new Notiz(Kategorie.Geburtstage, "Mutter: 18.03.1945");
            new Notiz(Kategorie.Geburtstage, "Vater: 21.08.1940");
            new Notiz(Kategorie.Internet, "www.ibb.com\r\nViele interessante Kurse");
            new Notiz(Kategorie.Urlaub, "Mallorca\r\nwar nicht gut!");
            new Notiz(Kategorie.Wichtig, "Steuererklärung\r\nmuss noch gemacht werden!!!");

            // Liste der Notizen aktualisieren
            ListeAktualisieren();
        }

        // Methode zur Aktualisierung der ListBox
        private void ListeAktualisieren()
        {
            // ListBox leeren
            lbxNotizen.Items.Clear();
            Kategorie ausgewählteKategorie = (Kategorie)cbxKategorie.SelectedItem;

            // Notizen filtern und der ListBox hinzufügen
            var gefilterteNotizen = Notiz.Notizen.Values.Where(n => ausgewählteKategorie == Kategorie.Alle || n.Kategorie == ausgewählteKategorie);
            foreach (var notiz in gefilterteNotizen)
            {
                lbxNotizen.Items.Add(notiz);
            }

            // Anzuzeigende Eigenschaft für die ListBox-Items festlegen
            lbxNotizen.DisplayMemberPath = "Inhalt";
        }

        // Event-Handler für die Kategorieauswahl in der ComboBox
        private void CbxKategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Liste der Notizen aktualisieren und den Neu-Button aktivieren/deaktivieren
            ListeAktualisieren();
            btnNeu.IsEnabled = (Kategorie)cbxKategorie.SelectedItem != Kategorie.Alle;
        }

        // Event-Handler für die Auswahl einer Notiz in der ListBox
        private void LbxNotizen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Aktuelle Notiz auf die ausgewählte Notiz setzen
            AktuelleNotiz = lbxNotizen.SelectedItem as Notiz;
        }

        // Event-Handler für Textänderungen in der TextBox
        private void TbxNotiz_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Speichern-Button aktivieren/deaktivieren, basierend auf dem Inhalt der TextBox
            if (AktuelleNotiz != null && !string.IsNullOrWhiteSpace(tbxNotiz.Text))
            {
                btnSpeichern.IsEnabled = true;
            }
            else
            {
                btnSpeichern.IsEnabled = false;
            }
        }

        // Event-Handler für den Klick auf den Neu-Button
        private void BtnNeu_Click(object sender, RoutedEventArgs e)
        {
            Kategorie ausgewählteKategorie = (Kategorie)cbxKategorie.SelectedItem;
            if (ausgewählteKategorie != Kategorie.Alle)
            {
                // Neue Notiz erstellen und die ListBox aktualisieren
                AktuelleNotiz = new Notiz(ausgewählteKategorie, "Neue Notiz");
                tbxNotiz.Text = AktuelleNotiz.Inhalt;
                tbxNotiz.IsEnabled = true;
                tbxNotiz.Focus();
                tbxNotiz.SelectAll();
                ListeAktualisieren();
            }
        }

        // Event-Handler für den Klick auf den Speichern-Button
        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if (AktuelleNotiz != null)
            {
                // Notizinhalt speichern und die ListBox aktualisieren
                AktuelleNotiz.Inhalt = tbxNotiz.Text;
                btnSpeichern.IsEnabled = false;
                ListeAktualisieren();
            }
        }

        // Event-Handler für den Klick auf den Löschen-Button
        private void BtnLöschen_Click(object sender, RoutedEventArgs e)
        {
            if (AktuelleNotiz != null)
            {
                // Bestätigung vom Benutzer einholen und die Notiz löschen
                var result = MessageBox.Show("Möchten Sie die ausgewählte Notiz wirklich löschen?", "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Notiz.Entfernen(AktuelleNotiz);
                    AktuelleNotiz = null;
                    ListeAktualisieren();
                }
            }
        }

        // Event-Handler für den Klick auf den Beenden-Button
        private void BtnBeenden_Click(object sender, RoutedEventArgs e)
        {
            // Anwendung beenden
            Application.Current.Shutdown();
        }
    }
}


/*
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _wert;
        public int Wert
        {
            get { return _wert; }
            set
            {
                _wert = value;
                OnPropertyChanged();
            }
        }

        public int Zahl { get; set; }

        public Dummy dummy { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            dummy = new Dummy();
            this.DataContext = dummy;

            *//* Button button = new Button
             {
                 Content = "Klick mich",
                 Height = 50,
                 Width = 80
             };*//*
            //grdMain.Children.Add(button);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void IncrementWert(object sender, RoutedEventArgs e)
        {
            Wert++;
        }

        private void IncrementZahl(object sender, RoutedEventArgs e)
        {
            Zahl++;
            OnPropertyChanged(nameof(Zahl));
        }
    }
}*/