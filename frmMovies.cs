using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_7
{
    public partial class frmMovies : Form
    {
        // Create list of movies
        List<MovieInformation> myMovies = new List<MovieInformation>();
        public frmMovies()
        {
            InitializeComponent();
        }

        private bool ValidateMovieFields()
        {
            // Create an array that stores each input field
            var fields = new string[]
            {
                txtTitle.Text,
                txtActors.Text,
                cboGenre.Text,
                txtDescription.Text
            };

            // Check to see if any of the fields contain a null value or are empty
            if (fields.Any(string.IsNullOrWhiteSpace))
            {
                MessageBox.Show("Please enter values for all fields");
                return false;
            }

            return true;
        }


        private struct MovieInformation
        {
            public string movieTitle;
            public string actors;
            public string genre;
            public string description;
        }

        private void DisplayMovie()
        {
            txtTitle.Text = myMovies[cboMovies.SelectedIndex].movieTitle;
            txtActors.Text = myMovies[cboMovies.SelectedIndex].actors;
            txtDescription.Text = myMovies[cboMovies.SelectedIndex].description;
            cboGenre.Text = myMovies[cboMovies.SelectedIndex].genre;
        }

        private void EnableControls (bool enabled)
        {
            // Set btnAdd, btnEdit, btnDelete, cboMovies enabled property
            // to the Boolean value passed to the method.
            btnAdd.Enabled = enabled;
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;
            cboMovies.Enabled = enabled;

            // Disable everything in the groupbox except for the labels
            foreach (Control ctrl in grpMovies.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox || ctrl is Button)
                {
                    ctrl.Enabled = !enabled;
                }
            }
        }

        private void LoadMovieList()
        {
            // Clear the movie list
            cboMovies.Items.Clear();

            // Loop through the list to display each movie
            for (int i = 0; i < myMovies.Count; i++)
            {
                cboMovies.Items.Add(myMovies[i].movieTitle);
            }

            // If the movie count is greater than 0, set the selected index to 0
            if (cboMovies.Items.Count > 0)
            {
                cboMovies.SelectedIndex = 0;
                
                // Call DisplayMovie to update textboxes
                DisplayMovie();
            }
        }

        private void clearMovieInformation()
        {
            txtActors.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
            cboGenre.SelectedIndex = -1;
        }

        private void LoadSampleMovies()
        {
            // Add Movies
            myMovies.Add(new MovieInformation
            {
                movieTitle = "Casablanca",
                actors = "Humphrey Bogart, Ingrid Bergman",
                genre = "Romance",
                description = "\"Casablanca\" is a classic romantic drama set during World War II. " +
                "The film revolves around Rick Blaine, " +
                "an American expatriate who runs a nightclub in Casablanca, Morocco."
            });

            myMovies.Add(new MovieInformation
            {
                movieTitle = "Avengers: Endgame",
                actors = "Robery Downey Jr., Chris Evans",
                genre = "Action",
                description = "Following the catastrophic events of \"Avengers: Infinity War,\" the remaining Avengers" +
                " and their allies work to undo the devastation caused by Thanos, " +
                "who wiped out half of all life in the universe with the snap of his fingers."
            });
            myMovies.Add(new MovieInformation
            {
                movieTitle = "Titanic (1997)",
                actors = "Leonardo DiCaprio, Kate Winslet",
                genre = "Romance",
                description = "Titanic tells the story of the ill-fated RMS Titanic, " +
                "focusing on the fictional romance between Jack Dawson (Leonardo DiCaprio) " +
                "and Rose DeWitt Bukater (Kate Winslet)."
            });
            myMovies.Add(new MovieInformation
            {
                movieTitle = "Furious 7",
                actors = "Vin Diesel, Dwayne Johnson",
                genre = "Action",
                description = "The film follows Dominic Toretto (Vin Diesel) and his crew " +
                "as they face a new threat from Deckard Shaw (Jason Statham), " +
                "a rogue assassin seeking revenge for the defeat of his brother. "
            });
        }


        private void frmMovies_Load(object sender, EventArgs e)
        {
            // Call LoadSampleMovies and LoadMovieList at form load
            LoadSampleMovies();
            LoadMovieList();

            // Disable controls for the groupbox
            EnableControls(true);
        }

        private void cboMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update textboxes when the movie selection changes
            DisplayMovie();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Enable controls for the groupbox
            EnableControls(false);

            // Clear movie information
            clearMovieInformation();

            // Set the update button tag to "A"
            btnUpdate.Tag = "A";

            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Verify that a movie was selected
            if (cboMovies.SelectedIndex > -1)
            {
                // Display the movie information
                DisplayMovie();

                // Enable controls for the groupbox
                EnableControls(false);

                // Set the update button tag to "E"
                btnUpdate.Tag = "E";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Verify that a movie was selected
            if (cboMovies.SelectedIndex > -1)
            {
                // Confirm that the user wants to delete the movie
                string message = "Do you want to delete " + cboMovies.Text + "?";
                string title = "Delete Movie";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    {
                        // Remove the movie from the list
                        myMovies.RemoveAt(cboMovies.SelectedIndex);

                        // Remove the movie from the ComboBox
                        cboMovies.Items.RemoveAt(cboMovies.SelectedIndex);

                        // Clear the text boxes and ComboBox
                        clearMovieInformation();

                        // Refresh the movie list
                        LoadMovieList();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Disable controls for the groupbox
            EnableControls(true);

            // Display movie information
            DisplayMovie();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check to see if there are any empty fields
            if (!ValidateMovieFields()) return;

            // Create a temporary structure to gather data
            MovieInformation tempMovie = new MovieInformation();
            tempMovie.movieTitle = txtTitle.Text;
            tempMovie.actors = txtActors.Text;
            tempMovie.genre = cboGenre.Text;
            tempMovie.description = txtDescription.Text;

            // Remember the currently selected index
            int selectedIndex = cboMovies.SelectedIndex;

            // If the update button tag is set to "A", add the movie to the myMovies list
            // Otherwise, overwrite the movie in the myMovies list based on the current selected index
            if (btnUpdate.Tag.ToString() == "A")
            {
                myMovies.Add(tempMovie);
                selectedIndex = myMovies.Count - 1;
            }
            else
            {
                myMovies[cboMovies.SelectedIndex] = tempMovie;
            }

            // Refresh the movie list
            LoadMovieList();

            // Set the ComboBox back to the previously selected index
            cboMovies.SelectedIndex = selectedIndex;

            // Disable controls for the groupbox
            EnableControls(true);
        }
    }
}