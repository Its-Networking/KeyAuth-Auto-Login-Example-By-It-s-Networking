using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyAuth;

namespace KeyAuth_Autologin_By_It_s_Networking
{
    public partial class LoginForm : Form
    {
        #region KeyAuth
        static string name = "Networking"; // application name. right above the blurred text aka the secret on the licenses tab among other tabs
        static string ownerid = "oVQJxJ2j9L"; // ownerid, found in account settings. click your profile picture on top right of dashboard and then account settings.
        static string secret = "c6c9386450edfdbc9c1afa5bfcd78832bfed17a7e74dade743262a83b4f5a90e"; // app secret, the blurred text on licenses tab and other tabs
        static string version = "1.0"; // leave alone unless you've changed version on website
        public static api KeyAuthApp = new api(name, ownerid, secret, version);
        #endregion

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            usernameBox.Text = Properties.Settings.Default.remUsername;
            passwordBox.Text = Properties.Settings.Default.remPassword;

            if (usernameBox.Text != "" && passwordBox.Text != "")
            {
                siticoneCheckBox1.Checked = true;
                siticoneRoundedButton1.PerformClick();
            }
            else
            {
                siticoneCheckBox1.Checked = false;
            }
        }

        private void siticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Close(); //closes the application
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            if (KeyAuthApp.login(usernameBox.Text, passwordBox.Text))  // tries to login with the text from username/password box
            {
                MessageBox.Show("Welcome, " + usernameBox.Text); // shows successful messagebox with your name if successful login
                Main m = new Main();
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to login!"); // shows failed messagebox if failed login
            }
        }

        private void siticoneCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (siticoneCheckBox1.Checked == true)
            {
                Properties.Settings.Default.remUsername = usernameBox.Text;
                Properties.Settings.Default.remPassword = passwordBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.remUsername = "";
                Properties.Settings.Default.remPassword = "";
                Properties.Settings.Default.Save();
            }
        }

        private void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            registerPanel.Visible = false;
        }

        private void siticoneRoundedButton4_Click(object sender, EventArgs e)
        {
            if (KeyAuthApp.register(regUsernamebox.Text, regPasswordbox.Text, regKeybox.Text))
            {
                MessageBox.Show("Registered with the username " + regUsernamebox.Text);
            }
            else
            {
                MessageBox.Show("Failed to register");
            }
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            registerPanel.Visible = true;
        }
    }
}
