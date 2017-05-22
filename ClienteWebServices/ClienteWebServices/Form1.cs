using System;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ClienteWebServices
{
    public partial class Form1 : Form
    {
        String ip;
        String sonda;
        String user;
        Boolean loged = false;

        sonda1.Sonda1 son1 = new sonda1.Sonda1();
        sonda2.Sonda2 son2 = new sonda2.Sonda2();

        public Form1()
        {
            InitializeComponent();
            button3.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            label4.Visible = false;
            comboBox3.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            label11.Visible = false;
            comboBox2.Visible = false;
            button5.Visible = false;
            label12.Visible = false;
            textBox7.Visible = false;
            label13.Visible = false;
            textBox8.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (loged == true)
            {
                ip = textBox3.Text;
                sonda = textBox4.Text;

                if (sonda == "sonda1")
                {
                    son1.Url = "http://" + ip + ":8080/Sonda1/services/Sonda1?wsdl";
                    try
                    {
                        String peticion = comboBox1.Text;
                        String result = son1.get(peticion);
                        textBox2.Text = result;

                    }
                    catch (Exception)
                    {
                        textBox2.Text = "Sonda no encontrada";
                    }
                }
                else if (sonda == "sonda2")
                {
                    son2.Url = "http://" + ip + ":8080/Sonda2/services/Sonda2?wsdl";
                    try
                    {
                        String peticion = comboBox1.Text;
                        String result = son2.get(peticion);
                        textBox2.Text = result;

                    }
                    catch (Exception)
                    {
                        textBox2.Text = "Sonda no encontrada";
                    }
                }
                string log = "User:" + user + " Do:GET " + "On:" + sonda + " At:" + DateTime.Now.ToString("h:mm:ss tt") + Environment.NewLine;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\EPS\Desktop\log.txt", true))
                {
                    file.WriteLine(log);
                }
            }else
            {
                textBox2.Text = "No estas logeado";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (loged == true)
            {
                ip = textBox3.Text;
                sonda = textBox4.Text;

                if (sonda == "sonda1")
                {
                    son1.Url = "http://" + ip + ":8080/Sonda1/services/Sonda1?wsdl";
                    try
                    {
                        String peticion = comboBox3.Text;
                        String cambio = textBox1.Text;
                        son1.set(peticion, cambio);

                    }
                    catch (Exception)
                    {
                        textBox2.Text = "Sonda no encontrada";
                    }
                }
                else if (sonda == "sonda2")
                {
                    son2.Url = "http://" + ip + ":8080/Sonda2/services/Sonda2?wsdl";
                    try
                    {
                        String peticion = comboBox3.Text;
                        String cambio = textBox1.Text;
                        son2.set(peticion, cambio);

                    }
                    catch (Exception)
                    {
                        textBox2.Text = "Sonda no encontrada";
                    }
                }
                string log = "User:" + user + " Do:SET " + "On:" + sonda + " At:" + DateTime.Now.ToString("h:mm:ss tt") + Environment.NewLine;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\EPS\Desktop\log.txt", true))
                {
                    file.WriteLine(log);
                }
            }else
            {
                textBox2.Text = "No estas logeado";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String logi = textBox7.Text;
            String pass = textBox8.Text;
            ip = textBox3.Text;
            sonda = textBox4.Text;

            if (sonda == "sonda1")
            {
                son1.Url = "http://" + ip + ":8080/Sonda1/services/Sonda1?wsdl";
                try
                {
                    son1.login(logi, pass);
                    loged = true;
                }catch (Exception){
                    loged = false;
                    textBox2.Text = "Error en login";
                }
            }else if (sonda == "sonda2"){
                son2.Url = "http://" + ip + ":8080/Sonda2/services/Sonda2?wsdl";
                try
                {
                    son2.login(logi, pass);
                    loged = true;
                }
                catch (Exception)
                {
                    textBox2.Text = "Error en login";
                    loged = false;
                }
            }
            string log = "User:" + user + " Do:LOGIN " + "On:" + sonda + " At:" + DateTime.Now.ToString("h:mm:ss tt") + Environment.NewLine;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\EPS\Desktop\log.txt", true))
            {
                file.WriteLine(log);
            }
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }
        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        private void aes()
        {
            try
            {
                string username = textBox5.Text;
                string userpass = textBox6.Text;
                // Create a new instance of the RijndaelManaged 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    // Encrypt the string to an array of bytes. 
                    byte[] userencrypted = EncryptStringToBytes(username, myRijndael.Key, myRijndael.IV);
                    byte[] passencrypted = EncryptStringToBytes(userpass, myRijndael.Key, myRijndael.IV);

                    // Decrypt the bytes to a string. 
                    string userroundtrip = DecryptStringFromBytes(userencrypted, myRijndael.Key, myRijndael.IV);
                    string passroundtrip = DecryptStringFromBytes(passencrypted, myRijndael.Key, myRijndael.IV);

                    //Display the original data and the decrypted data.
                    Console.WriteLine("user:   {0}", username);
                    Console.WriteLine("user decoded: {0}", userroundtrip);
                    Console.WriteLine("user coded: {0}", userencrypted);

                    Console.WriteLine("pass:   {0}", userpass);
                    Console.WriteLine("pass decoded: {0}", passroundtrip);
                    Console.WriteLine("pass coded: {0}", passencrypted);
                }
            }
            catch (Exception excep)
            {
                Console.WriteLine("Error: {0}", excep.Message);
            }

        }

        public String cifrador(String textoacifrar)
        {
            char[] charArray = textoacifrar.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\EPS\Desktop\Usuarios.txt");
            bool correcto = false;

            String claveuser = cifrador(textBox5.Text);
            String clavepass = cifrador(textBox6.Text);

            foreach (string line in lines)
            {
                String[] substrings = line.Split('#');
                if (claveuser.Equals(substrings[0]) && clavepass.Equals(substrings[1]))
                {
                    correcto = true;
                }
            }

            if (correcto == false)
            {
                label9.Visible = true;
            }else
            {
                user = textBox5.Text;
                label9.Visible = false;
                ocultarLogin();
                abrirmenu();
            }
        }

        private void ocultarLogin()
        {
            label7.Visible = false;
            textBox5.Visible = false;
            label8.Visible = false;
            textBox6.Visible = false;
            button4.Visible = false;
            label9.Visible = false;
        }

        private void abrirmenu()
        {
            button3.Visible = true;
            label6.Visible = true;
            label3.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            button1.Visible = true;
            label2.Visible = true;
            comboBox1.Visible = true;
            label1.Visible = true;
            textBox2.Visible = true;
            button2.Visible = true;
            label4.Visible = true;
            comboBox3.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            label11.Visible = true;
            comboBox2.Visible = true;
            button5.Visible = true;
            label12.Visible = true;
            textBox7.Visible = true;
            label13.Visible = true;
            textBox8.Visible = true;
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            label4.Visible = false;
            comboBox3.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            label11.Visible = false;
            comboBox2.Visible = false;
            label9.Visible = false;
            button5.Visible = false;
            label12.Visible = false;
            textBox7.Visible = false;
            label13.Visible = false;
            textBox8.Visible = false;

            label7.Visible = true;
            textBox5.Visible = true;
            label8.Visible = true;
            textBox6.Visible = true;
            button4.Visible = true;
            loged = false;
        }

        private void label1_Click(object sender, EventArgs e){}
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){}
        private void label2_Click(object sender, EventArgs e){}
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e){}
        private void label6_Click(object sender, EventArgs e){}
        private void label3_Click(object sender, EventArgs e){}
        private void label8_Click(object sender, EventArgs e){}
    }
}
