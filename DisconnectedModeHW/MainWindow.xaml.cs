using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DisconnectedModeHW
{
    public partial class MainWindow : Window
    {
        private List<User> allUsers = new List<User>();
        private ObservableCollection<User> displayedUsers = new ObservableCollection<User>();
        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();

            allUsers.Add(new User { Id = nextId++, Login = "admin", Password = "123", Address = "Київ", Phone = "0991112233", IsAdmin = true });
            allUsers.Add(new User { Id = nextId++, Login = "user1", Password = "qwerty", Address = "Львів", Phone = "0974445566", IsAdmin = false });

            RefreshList();
            dgUsers.ItemsSource = displayedUsers;
        }

        private void RefreshList()
        {
            displayedUsers.Clear();
            var list = chkOnlyAdmins.IsChecked == true 
                ? allUsers.Where(u => u.IsAdmin).ToList() 
                : allUsers;

            foreach (var u in list)
            {
                displayedUsers.Add(u);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введіть логін!");
                return;
            }

            if (allUsers.Any(u => u.Login.ToLower() == login.ToLower()))
            {
                MessageBox.Show("Логін вже зайнятий!");
                return;
            }

            User newUser = new User
            {
                Id = nextId++,
                Login = login,
                Password = txtPassword.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                IsAdmin = chkIsAdmin.IsChecked == true
            };

            allUsers.Add(newUser);
            RefreshList();
            ClearForm();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selected)
            {
                selected.Password = txtPassword.Text;
                selected.Address = txtAddress.Text;
                selected.Phone = txtPhone.Text;
                selected.IsAdmin = chkIsAdmin.IsChecked == true;

                RefreshList();
                ClearForm();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selected)
            {
                allUsers.Remove(selected);
                RefreshList();
                ClearForm();
            }
        }

        private void chkOnlyAdmins_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void dgUsers_SelectionChanged(object sender, SelectionPropertyChangedEventArgs e)
        {
            if (dgUsers.SelectedItem is User selected)
            {
                txtLogin.Text = selected.Login;
                txtPassword.Text = selected.Password;
                txtAddress.Text = selected.Address;
                txtPhone.Text = selected.Phone;
                chkIsAdmin.IsChecked = selected.IsAdmin;
            }
        }

        private void ClearForm()
        {
            txtLogin.Clear();
            txtPassword.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            chkIsAdmin.IsChecked = false;
        }
    }
}
