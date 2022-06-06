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

namespace Lab07_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rnd = new Random();
        static string selectedHall;
        static string selectedMovie;
        public static int generateDatesAmount = 2; 
        static int generateTicketsAmount = 24; //max 24
        static Hashtable hashtable = new Hashtable(1);
        static int index = 221000;

        public MainWindow()
        {
            InitializeComponent();
            GenerateCinemaHalls();
            GenerateCheckBoxes();

            RoutedEventArgs args = new RoutedEventArgs();
            CheckBtn_Click(this, args);

            MessageBox.Show("loadness: " + hashtable.loadness + "/" + hashtable.size);
        }

        static List<CheckBox> checkBoxes = new List<CheckBox>();
        public void GenerateCheckBoxes()
        {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.HorizontalAlignment = HorizontalAlignment.Left;
                    checkBox.VerticalAlignment = VerticalAlignment.Top;
                    checkBox.Height = 20;
                    checkBox.Width = 100;
                    checkBox.Content = " " + (i * 10 + j + 1);
                    checkBox.Margin = new Thickness(60 + j*40, 140 + i*40, 0, 0);
                    checkBox.IsHitTestVisible = false;
                    checkBox.Focusable = false;
                    checkBoxes.Add(checkBox);
                }
            }
            foreach(CheckBox cb in checkBoxes)
            {
                myGrid.Children.Add(cb);
            }   
        }

        public void GenerateCinemaHalls()
        {
            bool moviesGenerated = false;
            foreach (string s in CinemaHallNames) // 10
            {
                int i = 0;
                foreach (string s2 in MovieTitleNames) // 7 
                {
                    List<int> seats = new List<int>();
                    for (int j = 0; j< generateDatesAmount; j++)
                    {
                        Date date = new Date(rnd.Next(2000, 2030), MonthNames[rnd.Next(12)], rnd.Next(1, 31));
                        DateCB.Items.Add(date.ToString());
                        for(int k = 0; k< generateTicketsAmount; k++)
                        {
                        x1:
                            int seat = rnd.Next(50);
                            if (!seats.Contains(seat))
                            {
                                seats.Add(seat);
                            }
                            else goto x1;
                            Key myKey = new Key(index);
                            Value value = new Value(s2, s, date, seat);
                            hashtable.insertEntry(myKey, value);
                            index++;
                        }
                    }
                    if (!moviesGenerated) MovieCB.Items.Add(s2);
                    i++;
                }
                moviesGenerated = true;
                CinemaCB.Items.Add(s);
            }
            CinemaCB.SelectedIndex = 0;
            MovieCB.SelectedIndex = 0;
        }


        public string[] CinemaHallNames =
        {
            "Florencia",
            "Multiplex",
            "Wizoria"/*,
            "Kinoman",
            "Suputnik",
            "Dnipro",
            "Oskar",
            "Butterfly",
            "Boomer",
            "Miromax"*/
        };

        public string[] MovieTitleNames =
        {
            "Green mile",
            "Fight club",
            "Forest Gump",
            "Fury",
            "Pulp fiction",
            "American psycho",
            "Drive"
        };

        public string[] MonthNames =
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(CheckBox cb in checkBoxes)
            {
                cb.IsChecked = false;
                cb.Background = Brushes.White;
            }
            selectedHall = CinemaCB.SelectedItem.ToString();
            selectedMovie = MovieCB.SelectedItem.ToString();
            string selectedDate = DateCB.SelectedItem.ToString();
            AdditionalKey key = new AdditionalKey(selectedMovie, selectedHall);
            int code = hashtable.hashCode(key);
            for(int i =0; i<hashtable.size; i++)
            {
                if(hashtable.additionalTable[code].key.movieTitle == key.movieTitle && hashtable.additionalTable[code].key.cinemaHall == key.cinemaHall)
                {  
                    foreach (Ticket ticket in hashtable.additionalTable[code].ticketList)
                    {
                        if (selectedDate == ticket.date.ToString())
                        {
                            checkBoxes[ticket.seat].IsChecked = true;

                            if (ticket.seat != 49)
                                if((ticket.seat+1) % 10 != 0)
                                    if(checkBoxes[ticket.seat+1].IsChecked == false)
                                    {
                                        checkBoxes[ticket.seat + 1].Background = Brushes.DarkGray;
                                    }       
                            if(ticket.seat != 0)
                                if((ticket.seat-1)%10 != 9)
                                    if (checkBoxes[ticket.seat - 1].IsChecked == false)
                                    {
                                        checkBoxes[ticket.seat - 1].Background = Brushes.DarkGray;
                                    }
                            checkBoxes[ticket.seat].Background = Brushes.LightBlue;
                        }   
                    }
                    foreach(CheckBox cb in checkBoxes)
                    {
                        if (cb.Background == Brushes.White)
                        {
                            cb.Background = Brushes.LimeGreen;
                        }    
                    }
                    break;
                }
                else
                {
                    code = (code + i * i) % hashtable.size;
                }
            }
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateCB.Items.Clear();
            if (CinemaCB.SelectedIndex != -1)
                selectedHall = CinemaCB.SelectedItem.ToString();
            else return;

            if (MovieCB.SelectedIndex != -1)
                selectedMovie = MovieCB.SelectedItem.ToString();
            else return;
            AdditionalKey key = new AdditionalKey(selectedMovie, selectedHall);
            int code = hashtable.hashCode(key);
            List<string> checkList = new List<string>();
            for (int i = 0; i < hashtable.size; i++)
            {
                if (hashtable.additionalTable[code].key.movieTitle == key.movieTitle && hashtable.additionalTable[code].key.cinemaHall == key.cinemaHall)
                {
                    foreach (Ticket ticket in hashtable.additionalTable[code].ticketList)
                    {
                        if (!checkList.Contains(ticket.date.ToString()))
                        {
                            checkList.Add(ticket.date.ToString());
                            DateCB.Items.Add(ticket.date.ToString());
                        }
                    }
                    break;             
                }
                else
                {
                    code = (code + i * i) % hashtable.size;
                }
            }
            DateCB.SelectedIndex = 0;
        }

        private void DateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeatTB.Text = "";
            AddBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }

        private void SeatTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SeatTB.Text.Replace(" ","") != "")
            {
                AddBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
            }
            else
            {
                AddBtn.IsEnabled = false;
                DeleteBtn.IsEnabled = false;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedHall = CinemaCB.SelectedItem.ToString();
            selectedMovie = MovieCB.SelectedItem.ToString();
            string selectedDate = DateCB.SelectedItem.ToString();
            int seat = 0;
            try
            {
                seat = int.Parse(SeatTB.Text) - 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //string[] dateString = selectedDate.Split(' ');
            AdditionalKey key = new AdditionalKey(selectedMovie, selectedHall);
            int code = hashtable.hashCode(key);

            for (int i = 0; i < hashtable.size; i++)
            {
                if(hashtable.table[i] != null)
                    if (hashtable.table[i].value.movieTitle == selectedMovie)
                    {
                        if (hashtable.table[i].value.cinemaHall == selectedHall)
                        {
                            if (hashtable.table[i].value.date.ToString() == selectedDate)
                            {
                                if (hashtable.table[i].value.seat == seat)
                                {
                                    hashtable.table[i].status = Status.IsDeleted;
                                    hashtable.loadness--;
                                }
                            }
                        }
                    }
                if (hashtable.additionalTable[code].key.movieTitle == key.movieTitle && hashtable.additionalTable[code].key.cinemaHall == key.cinemaHall)
                {
                    
                    foreach (Ticket ticket in hashtable.additionalTable[code].ticketList)
                    {
                        if (ticket.seat == seat) 
                        {
                            hashtable.additionalTable[code].ticketList.Remove(ticket);
                            CheckBtn_Click(this, e);
                            break;
                        }
                    }
                }
                else
                {
                    code = (code + i * i) % hashtable.size;
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedHall = CinemaCB.SelectedItem.ToString();
            selectedMovie = MovieCB.SelectedItem.ToString();
            string selectedDate = DateCB.SelectedItem.ToString();
            int seat = 0;
            try
            {
                seat = int.Parse(SeatTB.Text) - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            string[] dateString = selectedDate.Split(' ');
            Date date = new Date(int.Parse(dateString[0]), dateString[1], int.Parse(dateString[2]));
            Value value = new Value(selectedMovie, selectedHall, date, seat);
            Key key = new Key(index);
            int countChecked = 0;
            foreach(CheckBox ch in checkBoxes)
            {
                if (ch.IsChecked == true)
                    countChecked++;
            }

            if (countChecked < 25)
            {
                if (checkBoxes[seat].Background == Brushes.LimeGreen)
                {
                    index++;
                    hashtable.insertEntry(key, value);
                    CheckBtn_Click(this, e);
                }
            }
            else MessageBox.Show("Hall is full (50%)");
        }
    }
    public class Hashtable
    {
        public Entry[] table;
        public AdditionalEntry[] additionalTable;
        public int loadness;
        public int size;

        public Hashtable(int size)
        {
            this.size = size;
            table = new Entry[this.size];
            additionalTable = new AdditionalEntry[this.size];
            loadness = 0;
        }
        public void insertEntry(Key key, Value value)
        {
            if (loadness/Convert.ToDouble(size) >= 0.5)
            {
                this.rehashing();
            }
            table[hashCode(key)] = new Entry(key, value);
            AdditionalKey addKey = new AdditionalKey(value.movieTitle, value.cinemaHall);
            Ticket newTicket = new Ticket(value.date, value.seat);
            int i = 0;
            while (true)
            {
                //MessageBox.Show("" +i);
                int hash = (hashCode(addKey) + (i * i)) % size;

                if (additionalTable[hash] == null || additionalTable[hashCode(addKey)].status == Status.IsDeleted)
                {
                    additionalTable[hash] = new AdditionalEntry(addKey);
                    additionalTable[hash].ticketList.Add(newTicket);
                    break;
                }
                else if (additionalTable[hash].key.cinemaHall == value.cinemaHall)
                {
                    if (additionalTable[hash].key.movieTitle == value.movieTitle)
                    {
                        additionalTable[hash].ticketList.Add(newTicket);
                        break;
                    }
                }
                i++;
            }
            loadness++;
        }

        public void rehashing()
        {
            size = size * 2;
            Hashtable bufferHashtable = new Hashtable(size);
            foreach(Entry entry in table)
            {
                if( entry != null)
                    bufferHashtable.insertEntry(entry.key, entry.value);
            }
            this.table = bufferHashtable.table;
            this.additionalTable = bufferHashtable.additionalTable;
            this.loadness = bufferHashtable.loadness;
            this.size = bufferHashtable.size;
        }

        public int hashCode(Key key)
        {
            return key.ticketId - 221000;
        }

        public int hashCode(AdditionalKey key)
        {
            string stringKey = key.cinemaHall + key.movieTitle;
            int buffer = 0;
            foreach (char c in stringKey)
            {
                buffer += Convert.ToInt32(c);
            }
            return buffer % size;
        }
    }

    public class Ticket
    {
        public Date date;
        public int seat;

        public Ticket(Date date, int seat)
        {
            this.date = date;
            this.seat = seat;
        }
    }

    public class AdditionalEntry
    {
        public AdditionalKey key;
        public List<Ticket> ticketList = new List<Ticket>();
        public Status status;

        public AdditionalEntry(AdditionalKey key)
        {
            this.key = key;
            this.status = Status.IsOkay;
        }
    }

    public class AdditionalKey
    {
        public string movieTitle;
        public string cinemaHall;

        public AdditionalKey(string movieTitle, string cinemaHall)
        {
            this.movieTitle = movieTitle;
            this.cinemaHall = cinemaHall;
        }
    }

    public class Entry
    {
        public Key key;
        public Value value;
        public Status status;

        public Entry(Key key, Value value)
        {
            this.key = key;
            this.value = value;
            status = Status.IsOkay;
        }
    }

    public enum Status
    {
        IsOkay, IsDeleted
    }

    public class Key
    {
        public int ticketId;

        public Key(int ticketId)
        {
            this.ticketId = ticketId;
        }
    }

    public class Value
    {
        public string movieTitle;
        public string cinemaHall;
        public Date date;
        public int seat;

        public Value(string movieTitle, string cinemaHall, Date date, int seat)
        {
            this.movieTitle = movieTitle;
            this.cinemaHall = cinemaHall;
            this.date = date;
            this.seat = seat;
        }
    }

    public class Date
    {
        public int year;
        public string month;
        public int day;
        public Date(int year, string month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public override string ToString()
        {
            return this.year + " " + this.month + " " + this.day;
        }
    }
}
