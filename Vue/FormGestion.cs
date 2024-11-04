using FranceInformatiqueInventaire.bddmanager;
using Microsoft.VisualBasic.Devices;
using System;
using System.Data;
using System.Data.SQLite;
using System.DirectoryServices;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Forms;
using FranceInformatiqueInventaire.Controlleur;
using System.ComponentModel;

namespace FranceInformatiqueInventaire
{
    /// <summary>
    ///  Form principale du logiciel, partie Vue et des évenements de l'application.
    /// </summary>
    public partial class FormGestion : Form
    {
        private List<DataGridViewRow> inventaireRowsCharge = new List<DataGridViewRow>();
        private List<string> marquesCharge = new List<string>();
        private List<string> typesCharge = new List<string>();
        private bool confirmationAvantSuppression = true;
        private bool confirmationAvantVider = true;
        private BddManager bddManagerRef;
        public string cheminFichierOuvert = "";
        public string titreFichierOuvert = "";
        private DateTimePicker dtpDateEntree = new DateTimePicker();
        private DateTimePicker dtpDateSortie = new DateTimePicker();
        private Rectangle rectangleDtpDateEntree;
        private Rectangle rectangleDtpDateSortie;
        private List<DataGridViewRow> rowsCopiee = new List<DataGridViewRow>();
        public bool insertionAvant = true;
        private Color couleurP = Color.FromArgb(48, 50, 54);
        private Color couleurS = Color.FromArgb(73, 82, 97);
        private Color couleurT = Color.FromArgb(105, 105, 105);
        private bool couperLignes = false;
        private GestionControlleur gestionControlleurRef;

        public FormGestion()
        {
            InitializeComponent();
            bddManagerRef = BddManager.GetInstance(this);
            //dtpEntree :
            dgv_Inventaire.Controls.Add(dtpDateEntree);
            dtpDateEntree.Visible = false;
            dtpDateEntree.Format = DateTimePickerFormat.Custom;
            dtpDateEntree.TextChanged += new EventHandler(dtpDateEntree_TextChange);
            //dtpSortie :
            dgv_Inventaire.Controls.Add(dtpDateSortie);
            dtpDateSortie.Visible = false;
            dtpDateSortie.Format = DateTimePickerFormat.Custom; ;
            dtpDateSortie.TextChanged += new EventHandler(dtpDateSortie_TextChange);
            //Preferences :
            TSMenuItem_Preferences_InsertionLigne_Cb.SelectedIndex = 0;

            gestionControlleurRef = new GestionControlleur(this, dgv_Inventaire, txt_RechercheInventaire, btn_CollerLigneInventaire, bddManagerRef, TSMenuItem_Fichier_Sauvegarder, inventaireRowsCharge, rowsCopiee, cb_FiltreRecherche, lb_Marque, lb_Type, marquesCharge, typesCharge, couperLignes);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < dgv_Inventaire.Columns.Count; i++)
            {
                cb_FiltreRecherche.Items.Add(dgv_Inventaire.Columns[i].HeaderText);
            }
            cb_FiltreRecherche.SelectedIndex = 0;
            this.ActiveControl = null;
            InitialiserCouleurThemeMenuItem();
            DefinirMarqueCharge();
            DefinirTypeCharge();
        }

        /// <summary>
        ///  Actualise la colonne index de la dataGridView pour que les nombres se suivent.
        /// </summary>
        public void ActualiserIndexLignes()
        {
            for (int i = 0; i < dgv_Inventaire.RowCount; i++)
            {
                dgv_Inventaire.Rows[i].Cells["Index"].Value = i;
            }
        }

        /// <summary>
        ///  Récupère la valeur de la dateTimePicker pour remplir la cellule.
        /// </summary>
        private void dtpDateEntree_TextChange(object? sender, EventArgs e)
        {
            dgv_Inventaire.CurrentCell.Value = dtpDateEntree.Text.ToString();
        }

        /// <summary>
        ///  Récupère la valeur de la dateTimePicker pour remplir la cellule.
        /// </summary>
        private void dtpDateSortie_TextChange(object? sender, EventArgs e)
        {
            dgv_Inventaire.CurrentCell.Value = dtpDateSortie.Text.ToString();
        }

        /// <summary>
        ///  Si scroll sur la dataGridView, alors cache les dateTimePicker.
        /// </summary>
        private void dgv_Inventaire_Scroll(object sender, ScrollEventArgs e)
        {
            dtpDateEntree.Visible = false;
            dtpDateSortie.Visible = false;
        }

        /// <summary>
        ///  Si clique sur une cellule appartenant aux colonnes des dates, alors fait apparaitre un dateTimePicker.
        /// </summary>
        private void dgv_Inventaire_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                DataGridViewCell cell = dgv_Inventaire.Rows[e.RowIndex].Cells[e.ColumnIndex];
                switch (dgv_Inventaire.Columns[e.ColumnIndex].Name)
                {
                    case "DateEntree":
                        dtpDateSortie.Visible = false;
                        rectangleDtpDateEntree = dgv_Inventaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        dtpDateEntree.Size = new Size(rectangleDtpDateEntree.Width, rectangleDtpDateEntree.Height);
                        dtpDateEntree.Location = new Point(rectangleDtpDateEntree.X, rectangleDtpDateEntree.Y);
                        dtpDateEntree.Visible = true;
                        if (cell.Value != null)
                        {
                            if (cell.Value.ToString() != "")
                            {
                                dtpDateEntree.Value = DateTime.ParseExact(cell.Value.ToString() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        break;
                    case "DateSortie":
                        dtpDateEntree.Visible = false;
                        rectangleDtpDateSortie = dgv_Inventaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        dtpDateSortie.Size = new Size(rectangleDtpDateSortie.Width, rectangleDtpDateSortie.Height);
                        dtpDateSortie.Location = new Point(rectangleDtpDateSortie.X, rectangleDtpDateSortie.Y);
                        dtpDateSortie.Visible = true;
                        if (cell.Value != null)
                        {
                            if (cell.Value.ToString() != "")
                            {
                                string actuelleTexte = cell.Value.ToString() ?? "";
                                dtpDateSortie.Value = DateTime.ParseExact(actuelleTexte, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        break;
                    default:
                        dtpDateEntree.Visible = false;
                        dtpDateSortie.Visible = false;
                        break;
                }
            }
        }

        /// <summary>
        ///  Si double clique sur une cellule appartenant à la colonne Index alors séléctionne la ligne.
        /// </summary>
        private void dgv_Inventaire_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                switch (dgv_Inventaire.Columns[e.ColumnIndex].Name)
                {
                    case "Index":
                        dgv_Inventaire.Rows[e.RowIndex].Selected = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Si des lignes sont supprimés, alors actualise les index des lignes.
        /// </summary>
        private void dgv_Inventaire_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualiserIndexLignes();
        }

        /// <summary>
        ///  Définit la variable booléen "confirmationAvantSuppresion" selon si la toolStripMenuItem est coché ou non.
        /// </summary>
        private void rToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            confirmationAvantSuppression = TSMenuItem_Preferences_ConfirmationSuppression.Checked;
        }

        /// <summary>
        ///  Définit la variable booléen "confirmationAvantVider" selon si la toolStripMenuItem est coché ou non.
        /// </summary>
        private void TSMenuItem_Preferences_ConfirmationVider_CheckedChanged(object sender, EventArgs e)
        {
            confirmationAvantVider = TSMenuItem_Preferences_ConfirmationVider.Checked;
        }

        /// <summary>
        ///  Selon le nombre de ligne selectionné, définit quel bouton est activé ou non.
        /// </summary>
        private void dgv_Inventaire_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Inventaire.SelectedRows.Count != 0)
            {
                btn_SupprimerLigneInventaire.Enabled = true;
                btn_CopierLigneInventaire.Enabled = true;
                btn_CouperLigneInventaire.Enabled = true;
                btn_ViderLigneInventaire.Enabled = true;
            }
            else
            {
                btn_SupprimerLigneInventaire.Enabled = false;
                btn_CopierLigneInventaire.Enabled = false;
                btn_CouperLigneInventaire.Enabled = false;
                btn_ViderLigneInventaire.Enabled = false;
            }

            if (dgv_Inventaire.SelectedRows.Count == 1)
            {
                btn_InsererLigneInventaire.Enabled = true;
            }
            else
            {
                btn_InsererLigneInventaire.Enabled = false;
            }
        }


        /// <summary>
        ///  Permet de changer le titre de la FormGestion si on ouvre ou sauvegarde un fichier.
        /// </summary>
        public void changerFormTitre(bool titreBasique)
        {
            if (titreBasique)
            {
                this.Text = "Inventaire FRANCEINFORMATIQUE.FR";
            }
            else
            {
                this.Text = "Inventaire FRANCEINFORMATIQUE.FR - " + titreFichierOuvert;
            }
        }

        /// <summary>
        ///  Si clique sur le ToolStripMenuItem "ouvrir", alors ouvre la boite de dialogue pour ouvrir un fichier.
        /// </summary>
        private void TSMenuItem_Fichier_Ouvrir_Click(object sender, EventArgs e)
        {
            Ouvrir();
        }

        /// <summary>
        ///  Ouvre une boite de dialogue de fichier, puis charge le fichier choisi.
        /// </summary>
        private void Ouvrir()
        {
            string tempText = gestionControlleurRef.OuvrirFichierDb();
            if (tempText != "")
            {
                cheminFichierOuvert = tempText;
                string texteConnexion = @"Data Source=" + cheminFichierOuvert + ";Version=3;";
                if (cheminFichierOuvert != "")
                {
                    changerFormTitre(false);
                    ChargerRemplirInventaire(bddManagerRef.RecupererInventaireTable(texteConnexion));
                    DefinirInventaireRowsCharge();
                    ChargerRemplirMarque(bddManagerRef.RecupererMarqueTable(texteConnexion));
                    ChargerRemplirType(bddManagerRef.RecupererTypeTable(texteConnexion));
                    TSMenuItem_Fichier_Sauvegarder.Enabled = true;
                    btn_Sauvegarder.Enabled = true;
                    UpdateInventaireLignesDeColonneType();
                    UpdateInventaireLignesDeColonneMarque();
                    DefinirMarqueCharge();
                    DefinirTypeCharge();
                }
            }
        }

        /// <summary>
        ///  Ajoute une ligne dans la dataGridView et sauvegarde ces rows temporairement dans une collection.
        /// </summary>
        private void AjouterLigneInventaire()
        {
            dgv_Inventaire.Sort(dgv_Inventaire.Columns[0], ListSortDirection.Ascending);
            dgv_Inventaire.Rows.Add();
            DefinirInventaireRowsCharge();
        }

        /// <summary>
        ///  Charge toutes les lignes de la dataGridView à partir d'un ValueTuple.
        /// </summary>
        /// <param name="contenuInventaireDansDbTemp">ValueTuple qui permet de charger toutes les lignes de chaque colonne de la dataGridView inventaire.</ param >
        private void ChargerRemplirInventaire((List<int>, List<string>, List<string>, List<string>, List<string>, List<float>, List<string>, List<string>, List<string>) contenuInventaireDansDbTemp)
        {
            /**Chaque liste correspond aux colonnes suivantes :
             *  Index
                Marque
                Nom
                Annee
                Prix
                DateEntree
                DateSortie
                Commentaire
            **/
            dgv_Inventaire.Rows.Clear();
            var (list0, list1, list2, list3, list4, list5, list6, list7, list8) = contenuInventaireDansDbTemp;
            for (int i = 0; i < list1.Count(); i++)
            {
                dgv_Inventaire.Rows.Add(list0[i], list1[i], list2[i], list3[i], list4[i], null, list6[i], list7[i], list8[i]);
                if (list5[i] != -1f)
                {
                    dgv_Inventaire.Rows[i].Cells[5].Value = list5[i];
                }
            }
        }

        /// <summary>
        ///  Charge la listBox Marque à partir du fichier chargé.
        /// </summary>
        /// <param name="marques">Liste str qui contient les marques.</param>
        private void ChargerRemplirMarque(List<string> marques)
        {
            lb_Marque.Items.Clear();
            for (int i = 0; i < marques.Count; i++)
            {
                lb_Marque.Items.Add(marques[i]);
            }
        }

        /// <summary>
        ///  Charge la listBox Type à partir du fichier chargé.
        /// </summary>
        /// <param name="types">Liste str qui contient les types.</param>
        private void ChargerRemplirType(List<string> types)
        {
            lb_Type.Items.Clear();
            for (int i = 0; i < types.Count; i++)
            {
                lb_Type.Items.Add(types[i]);
            }
        }

        /// <summary>
        ///  Récupère la valeur de la cellule, si elle est vide alors retourne un str vide, sinon retourne simplement la valeur.
        /// </summary>
        /// <param name="celluleValeur">Valeur de la cellule.</param>
        /// <param name="indexColonne">Index de la colonne de la cellule.</param>
        private object GetCellValueOuDefaultValue(object celluleValeur, int indexColonne)
        {
            if (celluleValeur == null || celluleValeur == "")
            {
                return "";
            }
            else
            {
                switch (indexColonne)
                {
                    case 5:
                        if (float.TryParse(celluleValeur.ToString(), out float floatValue))
                        {
                            return floatValue;
                        }
                        break;
                }
                return celluleValeur;
            }
        }

        /// <summary>
        ///  Récupère les valeurs du dataGridView actuelle, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne un ValueTuple qui correspond aux lignes de chaque colonne.</returns>
        public (List<int>, List<string>, List<string>, List<string>, List<string>, List<float>, List<string>, List<string>, List<string>) RecupererListesActuelleDgvInventaire()
        {
            List<int> listeId = new List<int> { };
            List<string> listeType = new List<string> { };
            List<string> listeMarque = new List<string> { };
            List<string> listeNom = new List<string> { };
            List<string> listeAnnee = new List<string> { };
            List<float> listePrix = new List<float> { };
            List<string> listeDateEntree = new List<string> { };
            List<string> listeDateSortie = new List<string> { };
            List<string> listeCommentaire = new List<string> { };
            dgv_Inventaire.Enabled = false;
            for (int i = 0; i < dgv_Inventaire.Rows.Count; i++)
            {
                listeId.Add((int)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[0].Value, 0));
                listeType.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[1].Value, 1));
                listeMarque.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[2].Value, 2));
                listeNom.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[3].Value, 3));
                listeAnnee.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[4].Value, 4));
                if (dgv_Inventaire.Rows[i].Cells[5].Value != null && dgv_Inventaire.Rows[i].Cells[5].Value != "")
                {
                    string value = dgv_Inventaire.Rows[i].Cells[5].Value.ToString();
                    object t = GetCellValueOuDefaultValue(value, 5);
                    listePrix.Add((float)(t));
                }
                else
                {
                    listePrix.Add(-1);
                }

                listeDateEntree.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[6].Value, 6));
                listeDateSortie.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[7].Value, 7));
                listeCommentaire.Add((string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[8].Value, 8));
            }
            dgv_Inventaire.Enabled = true;
            return (listeId, listeType, listeMarque, listeNom, listeAnnee, listePrix, listeDateEntree, listeDateSortie, listeCommentaire);
        }

        /// <summary>
        ///  Récupère les valeurs de la listBox Marque actuelle, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste str qui correspond aux marques actuelles.</returns>
        public List<string> RecupererMarquesActuelle()
        {
            List<string> marques = new List<string>();
            for (int i = 0; i < lb_Marque.Items.Count; i++)
            {
                marques.Add(lb_Marque.Items[i].ToString() ?? "");
            }
            return marques;
        }

        /// <summary>
        ///  Récupère les valeurs de la listBox Type actuelle, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste str qui correspond aux types actuelles.</returns>
        public List<string> RecupererTypesActuelle()
        {
            List<string> types = new List<string>();
            for (int i = 0; i < lb_Type.Items.Count; i++)
            {
                types.Add(lb_Type.Items[i].ToString() ?? "");
            }
            return types;
        }

        /// <summary>
        ///  Sauvegarde du fichier actuellement ouvert.
        /// </summary>
        private void Sauvegarder()
        {
            dgv_Inventaire.Enabled = false;
            dgv_Inventaire.EndEdit();
            bddManagerRef.EcrireBdd(cheminFichierOuvert);
            dgv_Inventaire.Enabled = true;
        }

        /// <summary>
        ///  Lance la sauvegarde du fichier actuellement ouvert.
        /// </summary>
        private void TSMenuItem_Fichier_Sauvegarder_Click(object sender, EventArgs e)
        {
            Sauvegarder();
        }

        /// <summary>
        ///  Lance la sauvegarde d'un nouveau fichier qui va être créer.
        /// </summary>
        private void TSMenuItem_Fichier_SauvegarderSous_Click(object sender, EventArgs e)
        {
            gestionControlleurRef.SauvegarderSous();
        }

        /// <summary>
        ///  Si des lignes sont ajoutées, mets à jour les cellules nécessaires et actualise la colonne Index.
        /// </summary>
        private void dgv_Inventaire_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualiserIndexLignes();
            DataGridViewComboBoxCell comboBoxCellType = (DataGridViewComboBoxCell)dgv_Inventaire.Rows[e.RowIndex].Cells[1];
            DataGridViewComboBoxCell comboBoxCellMarque = (DataGridViewComboBoxCell)dgv_Inventaire.Rows[e.RowIndex].Cells[2];
            UpdateCbCellType(comboBoxCellType);
            UpdateCbCellMarque(comboBoxCellMarque);
        }

        /// <summary>
        ///  Permet de mettre à jour la comboBox de la cellule appartenant à la colonne Type au niveau de ses options, en se basant sur la listBox Type.
        /// </summary>
        private void UpdateCbCellType(DataGridViewComboBoxCell cbCell)
        {
            cbCell.Items.Clear();
            foreach (string type in lb_Type.Items)
            {
                cbCell.Items.Add(type);
            }
        }

        /// <summary>
        ///  Permet de mettre à jour la comboBox de la cellule appartenant à la colonne Marque au niveau de ses options, en se basant sur la listBox Marque.
        /// </summary>
        private void UpdateCbCellMarque(DataGridViewComboBoxCell cbMarque)
        {
            cbMarque.Items.Clear();
            foreach (string type in lb_Marque.Items)
            {
                cbMarque.Items.Add(type);
            }
        }

        /// <summary>
        ///  Permet de mettre à jour les comboBoxCell de la colonne Type, à partir de la listBox Type, cela permet de vider l'option choisi si l'option  actuellement choisi n'est plus disponible car supprimé dans la listBox.
        /// </summary>
        private void UpdateInventaireLignesDeColonneType()
        {
            for (int i = 0; i < dgv_Inventaire.Rows.Count; i++)
            {
                DataGridViewCell cellType = dgv_Inventaire.Rows[i].Cells[1];
                if (cellType.Value != null)
                {
                    string actuelleTexte = cellType.Value.ToString() ?? "-844578";
                    if (!(lb_Type.Items.Contains(actuelleTexte)))
                    {
                        cellType.Value = "";
                    }
                }
                UpdateCbCellType((DataGridViewComboBoxCell)cellType);
            }
        }

        /// <summary>
        ///  Permet de mettre à jour les comboBoxCell de la colonne Marque, à partir de la listBox Marque, cela permet de vider l'option choisi si l'option  actuellement choisi n'est plus disponible car supprimé dans la listBox.
        /// </summary>
        private void UpdateInventaireLignesDeColonneMarque()
        {
            for (int i = 0; i < dgv_Inventaire.Rows.Count; i++)
            {
                DataGridViewCell cellMarque = dgv_Inventaire.Rows[i].Cells[2];
                if (cellMarque.Value != null)
                {
                    string actuelleTexte = cellMarque.Value.ToString() ?? "";
                    if (!(lb_Marque.Items.Contains(actuelleTexte)))
                    {
                        cellMarque.Value = "";
                    }
                }
                UpdateCbCellMarque((DataGridViewComboBoxCell)cellMarque);
            }
        }

        /// <summary>
        ///  Permet de cacher les boutons du toolStripMenu selon la page du tabControl choisi, le filtre de recherche est caché ou non, et le placeholderText de la textBox Recherche est aussi changé.
        /// </summary>
        private void tabControl_Inventaire_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl_Inventaire.SelectedIndex != 0)
            {
                btn_SupprimerLigneInventaire.Visible = false;
                btn_AjouterLigneInventaire.Visible = false;
                btn_InsererLigneInventaire.Visible = false;
                btn_CopierLigneInventaire.Visible = false;
                btn_CollerLigneInventaire.Visible = false;
                btn_CouperLigneInventaire.Visible = false;
                btn_ViderLigneInventaire.Visible = false;
                toolStripSeparator2.Visible = false;
                btn_Sauvegarder.Visible = false;
                btn_SauvegarderSous.Visible = false;
            }
            else if (tabControl_Inventaire.SelectedIndex == 0)
            {
                btn_SupprimerLigneInventaire.Visible = true;
                btn_AjouterLigneInventaire.Visible = true;
                btn_InsererLigneInventaire.Visible = true;
                btn_CopierLigneInventaire.Visible = true;
                btn_CollerLigneInventaire.Visible = true;
                btn_CouperLigneInventaire.Visible = true;
                btn_ViderLigneInventaire.Visible = true;
                toolStripSeparator2.Visible = true;
                btn_Sauvegarder.Visible = true;
                btn_SauvegarderSous.Visible = true;
            }
            switch (tabControl_Inventaire.SelectedIndex)
            {
                case 0:
                    txt_RechercheInventaire.PlaceholderText = "Rechercher dans l'inventaire";
                    cb_FiltreRecherche.Items.Clear();
                    cb_FiltreRecherche.Items.Add("Pas de filtre");
                    for (int i = 1; i < dgv_Inventaire.Columns.Count; i++)
                    {
                        cb_FiltreRecherche.Items.Add(dgv_Inventaire.Columns[i].HeaderText);
                    }
                    cb_FiltreRecherche.SelectedIndex = 0;
                    cb_FiltreRecherche.Visible = true;
                    lbl_Filtre.Visible = true;
                    break;
                case 1:
                    txt_RechercheInventaire.PlaceholderText = "Rechercher dans les marques";
                    cb_FiltreRecherche.Visible = false;
                    lbl_Filtre.Visible = false;
                    break;
                case 2:
                    txt_RechercheInventaire.PlaceholderText = "Rechercher dans les types";
                    cb_FiltreRecherche.Visible = false;
                    lbl_Filtre.Visible = false;
                    break;
            }
        }


        /// <summary>
        ///  Permet de déselectionné la marque choisi dans la listBox Marque si on appuie sur échappe.
        /// </summary>
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lb_Marque.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///  Permet de déselectionné le type choisi dans la listBox Type si on appuie sur échappe.
        /// </summary>
        private void lb_Type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lb_Type.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///  Activé ou non le bouton AjouterMarque selon si le texte AjoutMarque est vide.
        /// </summary>
        private void txt_AjoutMarque_TextChanged(object sender, EventArgs e)
        {
            if (txt_AjoutMarque.Text != "")
            {
                btn_AjouterMarque.Enabled = true;
            }
            else
            {
                btn_AjouterMarque.Enabled = false;
            }
        }

        /// <summary>
        ///  Activé ou non le bouton AjouterType selon si le texte AjoutType est vide.
        /// </summary>
        private void txt_TypeAjouter_TextChanged(object sender, EventArgs e)
        {
            if (txt_AjoutType.Text != "")
            {
                btn_AjouterType.Enabled = true;
            }
            else
            {
                btn_AjouterType.Enabled = false;
            }
        }

        /// <summary>
        ///  Ajoute la marque dans la listBox Marque et actualise ce qui est nécessaire.
        /// </summary>
        private void btn_AjouterMarque_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            lb_Marque.Items.Add(txt_AjoutMarque.Text);
            lb_Marque.SelectedIndex = -1;
            txt_AjoutMarque.Text = "";
            UpdateInventaireLignesDeColonneMarque();
            DefinirMarqueCharge();
        }

        /// <summary>
        ///  Ajoute le type dans la listBox Type et actualise ce qui est nécessaire.
        /// </summary>
        private void btn_AjouterType_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            lb_Type.Items.Add(txt_AjoutType.Text);
            lb_Type.SelectedIndex = -1;
            txt_AjoutType.Text = "";
            UpdateInventaireLignesDeColonneType();
            DefinirTypeCharge();
        }

        /// <summary>
        ///  Activé ou non le bouton SupprimerMarque selon si une marque est sélectionné.
        /// </summary>
        private void lb_Marque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Marque.SelectedIndex != -1)
            {
                btn_SupprimerMarque.Enabled = true;
            }
            else
            {
                btn_SupprimerMarque.Enabled = false;
            }
        }

        /// <summary>
        ///  Supprime la marque choisi et mets à jour ce qui est nécessaire.
        /// </summary>
        private void btn_SupprimerMarque_Click(object sender, EventArgs e)
        {
            string saveSelectedItemText = lb_Marque.SelectedItem.ToString() ?? "";
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            lb_Marque.Items.Remove(saveSelectedItemText);
            lb_Marque.SelectedIndex = -1;
            txt_AjoutMarque.Text = "";
            UpdateInventaireLignesDeColonneMarque();
            DefinirMarqueCharge();
        }

        /// <summary>
        ///  Supprime le type choisi et mets à jour ce qui est nécessaire.
        /// </summary>
        private void btn_SupprimerType_Click(object sender, EventArgs e)
        {
            string saveSelectedItemText = lb_Type.SelectedItem.ToString() ?? "";
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            lb_Type.Items.RemoveAt(lb_Type.SelectedIndex);
            lb_Type.SelectedIndex = -1;
            txt_AjoutType.Text = "";
            UpdateInventaireLignesDeColonneType();
            DefinirTypeCharge();
        }


        /// <summary>
        ///  Activé ou non le bouton SupprimerType selon si un type est sélectionné.
        /// </summary>
        private void lb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Type.SelectedIndex != -1)
            {
                btn_SupprimerType.Enabled = true;
            }
            else
            {
                btn_SupprimerType.Enabled = false;
            }
        }

        /// <summary>
        ///  Permet d'imposer des conditions selon la colonne quand on entre des valeurs dans les cellules.
        /// </summary>
        private void dgv_Inventaire_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            dgv_Inventaire.CellValueChanged -= dgv_Inventaire_CellValueChanged;
            if (dgv_Inventaire.CurrentCell != null)
            {
                switch (dgv_Inventaire.CurrentCell.ColumnIndex)
                {
                    case 5:
                        string cellPrixTexte = (string)(dgv_Inventaire.CurrentCell.Value ?? " ");
                        cellPrixTexte = cellPrixTexte.Replace(",", "");
                        cellPrixTexte = cellPrixTexte.Replace(".", "");
                        if ((!cellPrixTexte.All(char.IsDigit)) || cellPrixTexte[0] == '0')
                        {
                            dgv_Inventaire.CurrentCell.Value = null;
                        }
                        else if (dgv_Inventaire.CurrentCell.Value != null)
                        {
                            string actuelleText = dgv_Inventaire.CurrentCell.Value.ToString() as string ?? string.Empty;
                            dgv_Inventaire.CurrentCell.Value = actuelleText.Replace('.', ',');
                        }
                        break;
                    case 6:
                        string cellDateEntreeTexte = (string)(dgv_Inventaire.CurrentCell.Value ?? "");
                        DateTime dateNonUtilise;
                        if (!(DateTime.TryParseExact(cellDateEntreeTexte, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNonUtilise)))
                        {
                            dgv_Inventaire.CurrentCell.Value = "";
                        }
                        break;
                    case 7:
                        string cellDateSortieTexte = (string)(dgv_Inventaire.CurrentCell.Value ?? "");
                        DateTime dateNonUtilise2;
                        if (!(DateTime.TryParseExact(cellDateSortieTexte, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNonUtilise2)))
                        {
                            dgv_Inventaire.CurrentCell.Value = "";
                        }
                        break;

                }
            }
            dgv_Inventaire.CellValueChanged += dgv_Inventaire_CellValueChanged;
        }

        /// <summary>
        ///  Supprime le lien avec le fichier actuellement ouvert, et permet donc de faire "page blanche".
        /// </summary>
        private void TSMenuItem_Fichier_Nouveau_Click(object sender, EventArgs e)
        {
            dtpDateEntree.Visible = false;
            dtpDateSortie.Visible = false;
            dgv_Inventaire.Rows.Clear();
            changerFormTitre(true);
            cheminFichierOuvert = "";
            TSMenuItem_Fichier_Sauvegarder.Enabled = false;
            btn_Sauvegarder.Enabled = false;
        }

        /// <summary>
        ///  Si on change le texte de la textBox Recherche, alors recherche dans l'inventaire, dans les marques ou dans les types selon la page tabControl choisi.
        /// </summary>
        private void txt_RechercheInventaire_TextChanged(object sender, EventArgs e)
        {
            switch (tabControl_Inventaire.SelectedIndex)
            {
                case 0:
                    gestionControlleurRef.RechercherInventaire(txt_RechercheInventaire.Text);
                    break;
                case 1:
                    gestionControlleurRef.RechercherMarque(txt_RechercheInventaire.Text);
                    break;
                case 2:
                    gestionControlleurRef.RechercherType(txt_RechercheInventaire.Text);
                    break;
            }
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "inventaireRowsCharge" l'inventaire, cette procédure est nécessaire dans la recherche dans l'inventaire car à chaque recherche on "vide" la dataGridView, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        public void DefinirInventaireRowsCharge()
        {
            inventaireRowsCharge.Clear();
            foreach (DataGridViewRow row in dgv_Inventaire.Rows)
            {
                inventaireRowsCharge.Add(row);
            }
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "marquesCharge" la listBox Marque, cette procédure est nécessaire dans la recherche de marque car à chaque recherche on "vide" la listBox, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        private void DefinirMarqueCharge()
        {
            marquesCharge = lb_Marque.Items.OfType<string>().ToList();
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "typesCharge" la listBox Type, cette procédure est nécessaire dans la recherche de type car à chaque recherche on "vide" la listBox, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        private void DefinirTypeCharge()
        {
            typesCharge = lb_Type.Items.OfType<string>().ToList();
        }

        /// <summary>
        ///  Si la FormGestion est redimensionné, alors cache les dateTimePicker Entree et Sortie.
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            dtpDateEntree.Visible = false;
            dtpDateSortie.Visible = false;
        }

        /// <summary>
        ///  Si le filtre de recherche est changé, alors relance la recherche.
        /// </summary>
        private void cb_FiltreRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            gestionControlleurRef.RechercherInventaire(txt_RechercheInventaire.Text);
        }

        /// <summary>
        ///  Arrete la recherche et lance l'ajout d'une ligne à l'inventaire.
        /// </summary>
        private void btn_AjouterLigneInventaire_Click_1(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            AjouterLigneInventaire();
        }

        /// <summary>
        ///  Si la variable "confirmationAvantSuppression" est vrai, alors crée une boite de dialogue de confirmation avant de lancer la suppression des lignes.
        /// </summary>
        private void btn_SupprimerLigneInventaire_Click_1(object sender, EventArgs e)
        {
            if (confirmationAvantSuppression)
            {
                string message;
                if (dgv_Inventaire.SelectedRows.Count > 1)
                {
                    message = "Voulez-vous vraiment supprimer les lignes sélectionnés ?";
                }
                else
                {
                    message = "Voulez-vous vraiment supprimer la ligne sélectionné ?";
                }
                DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    gestionControlleurRef.SupprimerLignesInventaire();
                }
            }
            else
            {
                gestionControlleurRef.SupprimerLignesInventaire();
            }
        }

        /// <summary>
        ///  Lance l'insertion de ligne à l'inventaire.
        /// </summary>
        private void btn_InsererLigneInventaire_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_RechercheInventaire.Text = "";
            gestionControlleurRef.InsererLigneInventaire((int)dgv_Inventaire.CurrentRow.Cells[0].Value);
        }

        /// <summary>
        ///  Lance la copie de ligne de l'inventaire.
        /// </summary>
        private void btn_CopierLigneInventaire_Click(object sender, EventArgs e)
        {
            gestionControlleurRef.CopierLignesInventaire();
        }

        /// <summary>
        ///  Lance le fait de coller les lignes copiées à l'inventaire.
        /// </summary>
        private void btn_CollerLigneInventaire_Click(object sender, EventArgs e)
        {
            gestionControlleurRef.CollerLignesInventaire();
        }

        /// <summary>
        ///  Lance la coupe de ligne de l'inventaire.
        /// </summary>
        private void btn_CouperLigneInventaire_Click(object sender, EventArgs e)
        {
            gestionControlleurRef.CouperLignesInventaire();
        }

        /// <summary>
        ///  Lance le fait de vider la ou les lignes.
        /// </summary>
        private void btn_ViderLigneInventaire_Click(object sender, EventArgs e)
        {
            if (confirmationAvantVider)
            {
                string message;
                if (dgv_Inventaire.SelectedRows.Count > 1)
                {
                    message = "Voulez-vous vraiment vider les lignes sélectionnés ?";
                }
                else
                {
                    message = "Voulez-vous vraiment vider la ligne sélectionné ?";
                }
                DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    gestionControlleurRef.ViderLignesInventaire();
                }
            }
            else
            {
                gestionControlleurRef.ViderLignesInventaire();
            }
        }

        /// <summary>
        ///  Si l'application se ferme alors demande confirmation à partir d'une boite de dialogue.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {/*
            DialogResult confirmation = MessageBox.Show("Sauvegarder avant de quitter ?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (confirmation == DialogResult.Yes)
            {
                if (cheminFichierOuvert != "")
                {
                    Sauvegarder();
                }
                else
                {
                    gestionControlleurRef.SauvegarderSous();
                }
            }
            else if (confirmation == DialogResult.No)
            {
                DialogResult confirmation2 = MessageBox.Show("Êtes-vous sûr ?", "Confirmation", MessageBoxButtons.YesNo);
                if (confirmation2 == DialogResult.Yes)
                {
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }*/
        }

        /// <summary>
        ///  Permet de définir si l'insertion sera fait après ou avant la ligne choisie, à partir d'une comboBox dans un toolStripMenuItem.
        /// </summary>
        private void TSMenuItem_Preferences_InsertionLigne_Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TSMenuItem_Preferences_InsertionLigne_Cb.SelectedIndex)
            {
                case 0:
                    insertionAvant = true;
                    break;
                case 1:
                    insertionAvant = false;
                    break;
            }
        }

        /// <summary>
        ///  Crée une boite de dialogue de couleur pour permettre de choisir la couleur primaire de l'application.
        /// </summary>
        private void TSMenuItem_Preferences_Theme_CouleurP_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = couleurP;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                couleurP = colorDialog1.Color;
                TSMenuItem_Preferences_Theme_CouleurP.ForeColor = couleurP;
                ChangerCouleur();
            }
        }

        /// <summary>
        ///  Crée une boite de dialogue de couleur pour permettre de choisir la couleur secondaire de l'application.
        /// </summary>
        private void TSMenuItem_Preferences_Theme_CouleurS_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = couleurS;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                couleurS = colorDialog1.Color;
                TSMenuItem_Preferences_Theme_CouleurS.ForeColor = couleurS;
                ChangerCouleur();
            }
        }

        /// <summary>
        ///  Crée une boite de dialogue de couleur pour permettre de choisir la couleur tertiaire de l'application.
        /// </summary>
        private void TSMenuItem_Preferences_Theme_CouleurT_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = couleurT;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                couleurT = colorDialog1.Color;
                TSMenuItem_Preferences_Theme_CouleurT.ForeColor = couleurT;
                ChangerCouleur();
            }
        }

        /// <summary>
        ///  Change les couleurs de l'application.
        /// </summary>
        private void ChangerCouleur()
        {
            //Primaire
            this.BackColor = couleurP;
            tab_Inventaire.ForeColor = couleurP;
            tab_Marque.ForeColor = couleurP;
            tab_Type.ForeColor = couleurP;
            //Secondaire
            ts_Inventaire.BackColor = couleurS;
            menuStrip1.BackColor = couleurS;
            //Tertiaire
            dgv_Inventaire.BackgroundColor = couleurT;
        }

        /// <summary>
        ///  Initialise les couleurs des toolStripMenuItem correspondant aux couleurs primaire, secondaire et tertiaire.
        /// </summary>
        private void InitialiserCouleurThemeMenuItem()
        {
            TSMenuItem_Preferences_Theme_CouleurP.ForeColor = couleurP;
            TSMenuItem_Preferences_Theme_CouleurS.ForeColor = couleurS;
            TSMenuItem_Preferences_Theme_CouleurT.ForeColor = couleurT;
        }

        /// <summary>
        ///  Permet d'utiliser les fameux raccourcis CTRL+X pour couper,CTRL+C pour copier, CTRL+V pour coller.
        /// </summary>
        private void dgv_Inventaire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CopierLigneInventaire.Enabled)
                {
                    gestionControlleurRef.CopierLignesInventaire();
                }
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CollerLigneInventaire.Enabled)
                {
                    gestionControlleurRef.CollerLignesInventaire();
                }
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CouperLigneInventaire.Enabled)
                {
                    gestionControlleurRef.CouperLignesInventaire();
                }
            }
        }

        private void btn_Sauvegarder_Click(object sender, EventArgs e)
        {
            Sauvegarder();
        }

        private void btn_SauvegarderSous_Click(object sender, EventArgs e)
        {
            gestionControlleurRef.SauvegarderSous();
        }

        
    }
}