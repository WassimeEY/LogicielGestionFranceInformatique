using FranceInformatiqueInventaire.dal;
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
using FranceInformatiqueInventaire.Model;
using System.Diagnostics;

namespace FranceInformatiqueInventaire
{
    public enum visibiliteToolstrip {VISIBLE,CACHE,CACHEAVECFOND,MODIFDGV};
    public enum ongletPrincipal { TABDEBORD, INVENTAIRE, FACTURES, SITESFAV, PLANNING };

    /// <summary>
    ///  Form principale du logiciel, partie Vue et des évenements de l'application.
    /// </summary>
    public partial class FormGestion : Form
    {
        //Variables pour les valeurs par défaut
        private List<string> defautMarquesCharge = new List<string>();
        private List<string> defautTypesCharge = new List<string>();
        private List<string> defautPrestationsCharge = new List<string>();
        //Reste des variables
        private List<DataGridViewRow> inventaireRowsCharge = new List<DataGridViewRow>();
        private List<DataGridViewRow> factureRowsCharge = new List<DataGridViewRow>();
        private List<string> marquesCharge = new List<string>();
        private List<string> typesCharge = new List<string>();
        private List<string> prestationsCharge = new List<string>();
        private List<SiteFavorisLigne> sitesFavorisCharge = new List<SiteFavorisLigne>();
        private bool confirmationAvantSuppression = true;
        private bool confirmationAvantVider = true;
        private BddManager bddManagerRef;
        public string cheminFichierOuvert = "";
        public string titreFichierOuvert = "";
        private DateTimePicker dtpInventaireDateEntree = new DateTimePicker();
        private DateTimePicker dtpInventaireDateSortie = new DateTimePicker();
        private Rectangle rectangleDtpInventaireDateEntree;
        private Rectangle rectangleDtpInventaireDateSortie;
        private List<DataGridViewRow> rowsInventaireCopiee = new List<DataGridViewRow>();
        public bool insertionAvant = true;
        private Color couleurP = Color.FromArgb(48, 50, 54);
        private Color couleurS = Color.FromArgb(73, 82, 97);
        private Color couleurT = Color.FromArgb(105, 105, 105);
        private bool couperLignes = false;
        private GestionControlleur gestionControlleurRef;
        private ongletPrincipal ongletPrincipalActuel = ongletPrincipal.INVENTAIRE;
        private DateTimePicker dtpFactureDate = new DateTimePicker();
        private Rectangle rectangleDtpFactureDate;
        private List<DataGridViewRow> rowsFactureCopiee = new List<DataGridViewRow>();
        private bool tentativeAccesSite = false;

        public FormGestion()
        {
            InitializeComponent();
            bddManagerRef = BddManager.GetInstance(this);
            //dtpInventaireEntree :
            dgv_Inventaire.Controls.Add(dtpInventaireDateEntree);
            dtpInventaireDateEntree.Visible = false;
            dtpInventaireDateEntree.Format = DateTimePickerFormat.Custom;
            dtpInventaireDateEntree.TextChanged += new EventHandler(dtpInventaireDateEntree_TextChange);
            //dtpInventaireSortie :
            dgv_Inventaire.Controls.Add(dtpInventaireDateSortie);
            dtpInventaireDateSortie.Visible = false;
            dtpInventaireDateSortie.Format = DateTimePickerFormat.Custom; ;
            dtpInventaireDateSortie.TextChanged += new EventHandler(dtpInventaireDateSortie_TextChange);
            //dtpFacture :
            dgv_Facture.Controls.Add(dtpFactureDate);
            dtpFactureDate.Visible = false;
            dtpFactureDate.Format = DateTimePickerFormat.Custom;
            dtpFactureDate.TextChanged += new EventHandler(dtpFactureDate_TextChange);

            //Preferences :
            TSMenuItem_Preferences_InsertionLigne_Cb.SelectedIndex = 0;
            gestionControlleurRef = new GestionControlleur(this, dgv_Inventaire, txt_Recherche, btn_CollerLigne, bddManagerRef, TSMenuItem_Fichier_Sauvegarder, inventaireRowsCharge, rowsInventaireCopiee, cb_FiltreRecherche, lb_Marque, lb_Type, marquesCharge, typesCharge, couperLignes, dgv_Facture, factureRowsCharge, rowsFactureCopiee);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangerContenuCbFiltre(ongletPrincipal.INVENTAIRE);
            cb_FiltreRecherche.SelectedIndex = 0;
            this.ActiveControl = null;
            InitialiserCouleurThemeMenuItem();
            DefinirMarqueCharge();
            DefinirTypeCharge();
            DefinirPrestationCharge();
            DefinirVariablesDefaut();
            label_CopyrightVersion.Text = label_CopyrightVersion.Text.Insert(label_CopyrightVersion.Text.LastIndexOf('©') + 2, DateTime.Now.Year.ToString());
            lb_SitesFav.DataSource = sitesFavorisCharge;
        }

        /// <summary>
        ///  Actualise la colonne index de la dataGridView inventaire pour que les nombres se suivent.
        /// </summary>
        public void ActualiserIndexLignesInventaire()
        {
            for (int i = 0; i < dgv_Inventaire.RowCount; i++)
            {
                dgv_Inventaire.Rows[i].Cells["IndexInventaire"].Value = i;
            }
        }

        /// <summary>
        ///  Récupère la valeur de la dateTimePicker pour remplir la cellule.
        /// </summary>
        private void dtpInventaireDateEntree_TextChange(object? sender, EventArgs e)
        {
            dgv_Inventaire.CurrentCell.Value = dtpInventaireDateEntree.Text.ToString();
        }

        /// <summary>
        ///  Récupère la valeur de la dateTimePicker pour remplir la cellule.
        /// </summary>
        private void dtpInventaireDateSortie_TextChange(object? sender, EventArgs e)
        {
            dgv_Inventaire.CurrentCell.Value = dtpInventaireDateSortie.Text.ToString();
        }

        /// <summary>
        ///  Si scroll sur la dataGridView, alors cache les dateTimePicker.
        /// </summary>
        private void dgv_Inventaire_Scroll(object sender, ScrollEventArgs e)
        {
            dtpInventaireDateEntree.Visible = false;
            dtpInventaireDateSortie.Visible = false;
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
                        dtpInventaireDateSortie.Visible = false;
                        rectangleDtpInventaireDateEntree = dgv_Inventaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        dtpInventaireDateEntree.Size = new Size(rectangleDtpInventaireDateEntree.Width, rectangleDtpInventaireDateEntree.Height);
                        dtpInventaireDateEntree.Location = new Point(rectangleDtpInventaireDateEntree.X, rectangleDtpInventaireDateEntree.Y);
                        dtpInventaireDateEntree.Visible = true;
                        if (cell.Value != null)
                        {
                            if (cell.Value.ToString() != "")
                            {
                                dtpInventaireDateEntree.Value = DateTime.ParseExact(cell.Value.ToString() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        break;
                    case "DateSortie":
                        dtpInventaireDateEntree.Visible = false;
                        rectangleDtpInventaireDateSortie = dgv_Inventaire.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        dtpInventaireDateSortie.Size = new Size(rectangleDtpInventaireDateSortie.Width, rectangleDtpInventaireDateSortie.Height);
                        dtpInventaireDateSortie.Location = new Point(rectangleDtpInventaireDateSortie.X, rectangleDtpInventaireDateSortie.Y);
                        dtpInventaireDateSortie.Visible = true;
                        if (cell.Value != null)
                        {
                            if (cell.Value.ToString() != "")
                            {
                                string actuelleTexte = cell.Value.ToString() ?? "";
                                dtpInventaireDateSortie.Value = DateTime.ParseExact(actuelleTexte, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        break;
                    default:
                        dtpInventaireDateEntree.Visible = false;
                        dtpInventaireDateSortie.Visible = false;
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
                    case "IndexInventaire":
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
            ActualiserIndexLignesInventaire();
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
        ///  Selon le nombre de ligne selectionné, définit quel bouton est activé ou non, ici pour l'inventaire.
        /// </summary>
        private void dgv_Inventaire_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Inventaire.SelectedRows.Count != 0)
            {
                btn_SupprimerLigne.Enabled = true;
                btn_CopierLigne.Enabled = true;
                btn_CouperLigne.Enabled = true;
                btn_ViderLigne.Enabled = true;
            }
            else
            {
                btn_SupprimerLigne.Enabled = false;
                btn_CopierLigne.Enabled = false;
                btn_CouperLigne.Enabled = false;
                btn_ViderLigne.Enabled = false;
            }

            if (dgv_Inventaire.SelectedRows.Count == 1)
            {
                btn_InsererLigne.Enabled = true;
            }
            else
            {
                btn_InsererLigne.Enabled = false;
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
                    ChargerRemplirInventaire(bddManagerRef.RecupererTableInventaire(texteConnexion));
                    DefinirInventaireRowsCharge();
                    ChargerRemplirMarque(bddManagerRef.RecupererTableMarque(texteConnexion));
                    ChargerRemplirType(bddManagerRef.RecupererTableType(texteConnexion));
                    UpdateInventaireColonneType();
                    UpdateInventaireColonneMarque();
                    DefinirMarqueCharge();
                    DefinirTypeCharge();
                    if (bddManagerRef.VerifierExistanceTableFacture(texteConnexion))
                    {
                        ChargerRemplirFacture(bddManagerRef.RecupererTableFacture(texteConnexion));
                        DefinirFactureRowsCharge();
                        ChargerRemplirPrestation(bddManagerRef.RecupererTablePrestation(texteConnexion));
                        UpdateFactureColonnePrestation();
                        DefinirPrestationCharge();
                    }
                    TSMenuItem_Fichier_Sauvegarder.Enabled = true;
                    btn_Sauvegarder.Enabled = true;
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
        ///  Charge toutes les lignes de la dataGridView inventaire à partir d'une liste de InventaireLigne.
        /// </summary>
        /// <param name="contenuInventaireDansDbTemp">Liste de InventaireLigne qui permet de charger toutes les lignes de chaque colonne de la dataGridView inventaire.</ param >
        private void ChargerRemplirInventaire(List<InventaireLigne> contenuInventaireDansDbTemp)
        {
            dgv_Inventaire.Rows.Clear();
            for (int i = 0; i < contenuInventaireDansDbTemp.Count(); i++)
            {
                dgv_Inventaire.Rows.Add(contenuInventaireDansDbTemp[i].id, contenuInventaireDansDbTemp[i].type, contenuInventaireDansDbTemp[i].marque, contenuInventaireDansDbTemp[i].nom, contenuInventaireDansDbTemp[i].annee, null, contenuInventaireDansDbTemp[i].dateEntree, contenuInventaireDansDbTemp[i].dateSortie, contenuInventaireDansDbTemp[i].commentaire);
                if (contenuInventaireDansDbTemp[i].prix != -1f)
                {
                    dgv_Inventaire.Rows[i].Cells[5].Value = contenuInventaireDansDbTemp[i].prix + "€";
                }
            }
        }

        /// <summary>
        ///  Charge la listBox Marque à partir du fichier chargé.
        /// </summary>
        /// <param name="marques">Liste InventaireMarque à charger.</param>
        private void ChargerRemplirMarque(List<InventaireMarque> marques)
        {
            lb_Marque.Items.Clear();
            for (int i = 0; i < marques.Count; i++)
            {
                lb_Marque.Items.Add(marques[i].nom);
            }
        }

        /// <summary>
        ///  Charge la listBox Type à partir du fichier chargé.
        /// </summary>
        /// <param name="types">Liste InventaireType à charger.</param>
        private void ChargerRemplirType(List<InventaireType> types)
        {
            lb_Type.Items.Clear();
            for (int i = 0; i < types.Count; i++)
            {
                lb_Type.Items.Add(types[i].nom);
            }
        }

        /// <summary>
        ///  Charge toutes les lignes de la dataGridView facture à partir d'une liste de FactureLigne.
        /// </summary>
        /// <param name="contenuFactureDansDbTemp">Liste de FactureLigne qui permet de charger toutes les lignes de chaque colonne de la dataGridView facture.</ param >
        private void ChargerRemplirFacture(List<FactureLigne> contenuFactureDansDbTemp)
        {
            dgv_Facture.Rows.Clear();
            for (int i = 0; i < contenuFactureDansDbTemp.Count(); i++)
            {
                dgv_Facture.Rows.Add(contenuFactureDansDbTemp[i].id, contenuFactureDansDbTemp[i].nom, contenuFactureDansDbTemp[i].prestation, contenuFactureDansDbTemp[i].date, contenuFactureDansDbTemp[i].commentaire, null, null, null);
                if (contenuFactureDansDbTemp[i].prixHT != -1f)
                {
                    dgv_Facture.Rows[i].Cells[5].Value = contenuFactureDansDbTemp[i].prixHT + "€";
                }
                if (contenuFactureDansDbTemp[i].prixTTC != -1f)
                {
                    dgv_Facture.Rows[i].Cells[6].Value = contenuFactureDansDbTemp[i].prixTTC + "€";
                }
                if (contenuFactureDansDbTemp[i].difference != -1f)
                {
                    dgv_Facture.Rows[i].Cells[7].Value = contenuFactureDansDbTemp[i].difference + "€";
                }
            }
        }

        /// <summary>
        ///  Charge la listBox prestation à partir du fichier chargé.
        /// </summary>
        /// <param name="types">Liste FacturePrestation à charger.</param>
        private void ChargerRemplirPrestation(List<FacturePrestation> prestations)
        {
            lb_Prestation.Items.Clear();
            for (int i = 0; i < prestations.Count; i++)
            {
                lb_Prestation.Items.Add(prestations[i].nom + " (" + prestations[i].pourcentageTVA * 100 + "%)");
            }
        }

        /// <summary>
        ///  Récupère la valeur de la cellule, si elle est vide alors retourne un str vide, sinon retourne simplement la valeur.
        /// </summary>
        /// <param name="celluleValeur">Valeur de la cellule.</param>
        /// <param name="indexColonne">Index de la colonne de la cellule.</param>
        private object GetCellValueOuDefaultValue(object celluleValeur, string nomColonne)
        {
            if (celluleValeur == null || celluleValeur == "")
            {
                return "";
            }
            else
            {
                if (nomColonne.Contains("Prix") || nomColonne == "Difference")
                {
                    celluleValeur = ((string)celluleValeur).Replace("€", "");
                    if (float.TryParse(celluleValeur.ToString(), out float floatValue))
                    {
                        return floatValue;
                    }
                }
                return celluleValeur;
            }
        }

        /// <summary>
        ///  Récupère les valeurs du dataGridView inventaire, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste de la classe métier InventaireLigne correspondant aux lignes de l'inventaire.</returns>
        public List<InventaireLigne> RecupererInventaireActuelle()
        {
            List<InventaireLigne> inventaireLignes = new List<InventaireLigne> { };
            int id;
            string type;
            string marque;
            string nom;
            string annee;
            float prix;
            string dateEntree;
            string dateSortie;
            string commentaire;
            dgv_Inventaire.Enabled = false;
            for (int i = 0; i < dgv_Inventaire.Rows.Count; i++)
            {
                id = (int)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[0].Value, dgv_Inventaire.Columns[0].Name);
                type = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[1].Value, dgv_Inventaire.Columns[1].Name);
                marque = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[2].Value, dgv_Inventaire.Columns[2].Name);
                nom = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[3].Value, dgv_Inventaire.Columns[3].Name);
                annee = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[4].Value, dgv_Inventaire.Columns[4].Name);
                if (dgv_Inventaire.Rows[i].Cells[5].Value != null && dgv_Inventaire.Rows[i].Cells[5].Value != "")
                {
                    string value = dgv_Inventaire.Rows[i].Cells[5].Value.ToString();
                    object t = GetCellValueOuDefaultValue(value, dgv_Inventaire.Columns[5].Name);
                    prix = (float)(t);
                }
                else
                {
                    prix = -1;
                }

                dateEntree = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[6].Value, dgv_Inventaire.Columns[6].Name);
                dateSortie = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[7].Value, dgv_Inventaire.Columns[7].Name);
                commentaire = (string)GetCellValueOuDefaultValue(dgv_Inventaire.Rows[i].Cells[8].Value, dgv_Inventaire.Columns[8].Name);
                InventaireLigne nouvelleLigne = new InventaireLigne(id, type, marque, nom, annee, prix, dateEntree, dateSortie, commentaire);
                inventaireLignes.Add(nouvelleLigne);
            }
            dgv_Inventaire.Enabled = true;
            return inventaireLignes;
        }

        /// <summary>
        ///  Récupère les valeurs de la listBox marque, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste de type de la classe métier InventaireMarque qui correspond aux marques actuelles.</returns>
        public List<InventaireMarque> RecupererMarquesActuelle()
        {
            List<InventaireMarque> marques = new List<InventaireMarque>();
            for (int i = 0; i < lb_Marque.Items.Count; i++)
            {
                InventaireMarque nouvelleMarque = new InventaireMarque(lb_Marque.Items[i].ToString() ?? "");
                marques.Add(nouvelleMarque);
            }
            return marques;
        }

        /// <summary>
        ///  Récupère les valeurs de la listBox type, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste de la classe métier InventaireType qui correspond aux types actuelles.</returns>
        public List<InventaireType> RecupererTypesActuelle()
        {
            List<InventaireType> types = new List<InventaireType>();
            for (int i = 0; i < lb_Type.Items.Count; i++)
            {
                InventaireType nouveauType = new InventaireType(lb_Type.Items[i].ToString() ?? "");
                types.Add(nouveauType);
            }
            return types;
        }

        /// <summary>
        ///  Récupère les valeurs du dataGridView facture, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste de la classe métier FactureLigne qui correspond aux factures.</returns>
        public List<FactureLigne> RecupererFacturesActuelle()
        {
            List<FactureLigne> factures = new List<FactureLigne>();
            int id;
            string nom;
            string prestation;
            string date;
            string commentaire;
            float prixHT;
            float prixTTC;
            float difference;
            dgv_Facture.Enabled = false;
            for (int i = 0; i < dgv_Facture.Rows.Count; i++)
            {
                id = (int)GetCellValueOuDefaultValue(dgv_Facture.Rows[i].Cells[0].Value, dgv_Facture.Columns[0].Name);
                nom = (string)GetCellValueOuDefaultValue(dgv_Facture.Rows[i].Cells[1].Value, dgv_Facture.Columns[1].Name);
                prestation = (string)GetCellValueOuDefaultValue(dgv_Facture.Rows[i].Cells[2].Value, dgv_Facture.Columns[2].Name);
                date = (string)GetCellValueOuDefaultValue(dgv_Facture.Rows[i].Cells[3].Value, dgv_Facture.Columns[3].Name);
                commentaire = (string)GetCellValueOuDefaultValue(dgv_Facture.Rows[i].Cells[4].Value, dgv_Facture.Columns[4].Name);
                if (dgv_Facture.Rows[i].Cells[5].Value != null && dgv_Facture.Rows[i].Cells[5].Value != "")
                {
                    string value = dgv_Facture.Rows[i].Cells[5].Value.ToString();
                    object t = GetCellValueOuDefaultValue(value, dgv_Facture.Columns[5].Name);
                    prixHT = (float)(t);
                }
                else
                {
                    prixHT = -1;
                }
                if (dgv_Facture.Rows[i].Cells[6].Value != null && dgv_Facture.Rows[i].Cells[6].Value != "")
                {
                    string value = dgv_Facture.Rows[i].Cells[6].Value.ToString();
                    object t = GetCellValueOuDefaultValue(value, dgv_Facture.Columns[6].Name);
                    prixTTC = (float)(t);
                }
                else
                {
                    prixTTC = -1;
                }
                if (dgv_Facture.Rows[i].Cells[7].Value != null && dgv_Facture.Rows[i].Cells[7].Value != "")
                {
                    string value = dgv_Facture.Rows[i].Cells[7].Value.ToString();
                    object t = GetCellValueOuDefaultValue(value, dgv_Facture.Columns[7].Name);
                    difference = (float)(t);
                }
                else
                {
                    difference = -1;
                }
                FactureLigne nouvelleLigne = new FactureLigne(id, nom, prestation, date, commentaire, prixHT, prixTTC, difference);
                factures.Add(nouvelleLigne);
            }
            dgv_Facture.Enabled = true;
            return factures;
        }

        /// <summary>
        ///  Récupère les valeurs de la listBox prestation, pour pouvoir les sauvegarder dans un fichier.
        /// </summary>
        /// <returns>Retourne une liste de la classe métier FacturePrestation qui correspond aux prestations actuelles.</returns>
        public List<FacturePrestation> RecupererPrestationsActuelle()
        {
            List<FacturePrestation> prestations = new List<FacturePrestation>();
            for (int i = 0; i < lb_Prestation.Items.Count; i++)
            {
                string stringOriginal = lb_Prestation.Items[i].ToString() ?? "";
                string nomSepare;
                float pourcentageSepare;
                (nomSepare, pourcentageSepare) = SeparerPrestationNomPourcentage(stringOriginal);
                FacturePrestation nouvellePrestation = new FacturePrestation(nomSepare, pourcentageSepare);
                prestations.Add(nouvellePrestation);
            }
            return prestations;
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
        ///  Si des lignes sont ajoutées, mets à jour les cellules nécessaires et actualise la colonne Index du dataGridView inventaire.
        /// </summary>
        private void dgv_Inventaire_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualiserIndexLignesInventaire();
            DataGridViewComboBoxCell comboBoxCellType = (DataGridViewComboBoxCell)dgv_Inventaire.Rows[e.RowIndex].Cells[1];
            DataGridViewComboBoxCell comboBoxCellMarque = (DataGridViewComboBoxCell)dgv_Inventaire.Rows[e.RowIndex].Cells[2];
            UpdateCbCellType(comboBoxCellType);
            UpdateCbCellMarque(comboBoxCellMarque);
        }

        /// <summary>
        ///  Permet de mettre à jour la comboBox de la cellule appartenant à la colonne Type du dataGridView inventaire au niveau de ses options, en se basant sur la listBox type.
        /// </summary>
        private void UpdateCbCellType(DataGridViewComboBoxCell cbType)
        {
            cbType.Items.Clear();
            foreach (string type in lb_Type.Items)
            {
                cbType.Items.Add(type);
            }
        }

        /// <summary>
        ///  Permet de mettre à jour la comboBox de la cellule appartenant à la colonne Marque du dataGridView inventaire au niveau de ses options, en se basant sur la listBox marque.
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
        ///  Permet de mettre à jour la comboBox de la cellule appartenant à la colonne Prestation du dataGridView facture au niveau de ses options, en se basant sur la listBox prestation.
        /// </summary>
        private void UpdateCbCellPrestation(DataGridViewComboBoxCell cbPrestation)
        {
            cbPrestation.Items.Clear();
            foreach (string prestation in lb_Prestation.Items)
            {
                cbPrestation.Items.Add(prestation);
            }
        }

        /// <summary>
        ///  Permet de mettre à jour les comboBoxCell de la colonne Type du dataGridView inventaire, à partir de la listBox Type, cela permet de vider l'option choisi si l'option actuellement choisi n'est plus disponible car supprimée dans la listBox.
        /// </summary>
        private void UpdateInventaireColonneType()
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
        ///  Permet de mettre à jour les comboBoxCell de la colonne Marque du dataGridView inventaire, à partir de la listBox marque, cela permet de vider l'option choisi si l'option  actuellement choisi n'est plus disponible car supprimée dans la listBox.
        /// </summary>
        private void UpdateInventaireColonneMarque()
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
        ///  Permet de mettre à jour les comboBoxCell de la colonne Prestation du dataGridView facture, à partir de la listBox prestation, cela permet de vider l'option choisi si l'option  actuellement choisi n'est plus disponible car supprimée dans la listBox.
        /// </summary>
        private void UpdateFactureColonnePrestation()
        {
            for (int i = 0; i < dgv_Facture.Rows.Count; i++)
            {
                DataGridViewCell cellPrestation = dgv_Facture.Rows[i].Cells[2];
                if (cellPrestation.Value != null)
                {
                    string actuelleTexte = cellPrestation.Value.ToString() ?? "";
                    if (!(lb_Prestation.Items.Contains(actuelleTexte)))
                    {
                        cellPrestation.Value = "";
                    }
                }
                UpdateCbCellPrestation((DataGridViewComboBoxCell)cellPrestation);
            }
        }

        /// <summary>
        ///  Permet de cacher les boutons du toolStripMenu selon la page du tabControl choisi, le filtre de recherche est caché ou non, et le placeholderText de la textBox Recherche est aussi changé.
        /// </summary>
        private void tabControl_Inventaire_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl_Inventaire.SelectedIndex != 0)
            {
                DefinirVisibiliteToolStrip(visibiliteToolstrip.CACHEAVECFOND);
            }
            else if (tabControl_Inventaire.SelectedIndex == 0)
            {
                DefinirVisibiliteToolStrip(visibiliteToolstrip.MODIFDGV);
            }
            switch (tabControl_Inventaire.SelectedIndex)
            {
                case 0:
                    txt_Recherche.PlaceholderText = "Rechercher dans l'inventaire";
                    ChangerContenuCbFiltre(ongletPrincipal.INVENTAIRE);
                    cb_FiltreRecherche.Visible = true;
                    lbl_Filtre.Visible = true;
                    break;
                case 1:
                    txt_Recherche.PlaceholderText = "Rechercher dans les marques";
                    cb_FiltreRecherche.Visible = false;
                    lbl_Filtre.Visible = false;
                    break;
                case 2:
                    txt_Recherche.PlaceholderText = "Rechercher dans les types";
                    cb_FiltreRecherche.Visible = false;
                    lbl_Filtre.Visible = false;
                    break;
            }
        }

        private void DefinirVisibiliteToolStrip(visibiliteToolstrip visibilite, bool inclureBtnsEnregistrer = false)
        {
            switch (visibilite)
            {
                case visibiliteToolstrip.VISIBLE:
                    ts_Inventaire.Visible = true;
                    break;
                case visibiliteToolstrip.CACHE:
                    ts_Inventaire.Visible = false;
                    break;
                case visibiliteToolstrip.CACHEAVECFOND:
                    btn_SupprimerLigne.Visible = false;
                    btn_AjouterLigne.Visible = false;
                    btn_InsererLigne.Visible = false;
                    btn_CopierLigne.Visible = false;
                    btn_CollerLigne.Visible = false;
                    btn_CouperLigne.Visible = false;
                    btn_ViderLigne.Visible = false;
                    toolStripSeparator2.Visible = false;
                    break;
                case visibiliteToolstrip.MODIFDGV:
                    btn_SupprimerLigne.Visible = true;
                    btn_AjouterLigne.Visible = true;
                    btn_InsererLigne.Visible = true;
                    btn_CopierLigne.Visible = true;
                    btn_CollerLigne.Visible = true;
                    btn_CouperLigne.Visible = true;
                    btn_ViderLigne.Visible = true;
                    toolStripSeparator2.Visible = true;
                    btn_Sauvegarder.Visible = true;
                    btn_SauvegarderSous.Visible = true;
                    break;
            }
            if (visibilite != visibiliteToolstrip.VISIBLE && inclureBtnsEnregistrer)
            {
                btn_Sauvegarder.Visible = false;
                btn_SauvegarderSous.Visible = false;
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
            txt_Recherche.Text = "";
            lb_Marque.Items.Add(txt_AjoutMarque.Text);
            lb_Marque.SelectedIndex = -1;
            txt_AjoutMarque.Text = "";
            UpdateInventaireColonneMarque();
            DefinirMarqueCharge();
        }

        /// <summary>
        ///  Ajoute le type dans la listBox Type et actualise ce qui est nécessaire.
        /// </summary>
        private void btn_AjouterType_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_Recherche.Text = "";
            lb_Type.Items.Add(txt_AjoutType.Text);
            lb_Type.SelectedIndex = -1;
            txt_AjoutType.Text = "";
            UpdateInventaireColonneType();
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
            txt_Recherche.Text = "";
            lb_Marque.Items.Remove(saveSelectedItemText);
            lb_Marque.SelectedIndex = -1;
            txt_AjoutMarque.Text = "";
            UpdateInventaireColonneMarque();
            DefinirMarqueCharge();
        }

        /// <summary>
        ///  Supprime le type choisi et mets à jour ce qui est nécessaire.
        /// </summary>
        private void btn_SupprimerType_Click(object sender, EventArgs e)
        {
            string saveSelectedItemText = lb_Type.SelectedItem.ToString() ?? "";
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_Recherche.Text = "";
            lb_Type.Items.RemoveAt(lb_Type.SelectedIndex);
            lb_Type.SelectedIndex = -1;
            txt_AjoutType.Text = "";
            UpdateInventaireColonneType();
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
        ///  Permet d'imposer des conditions selon la colonne quand on entre des valeurs dans les cellules de l'inventaire.
        /// </summary>
        private void dgv_Inventaire_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            dgv_Inventaire.CellValueChanged -= dgv_Inventaire_CellValueChanged;
            if (dgv_Inventaire.CurrentCell != null)
            {
                switch (dgv_Inventaire.Columns[dgv_Inventaire.CurrentCell.ColumnIndex].Name)
                {
                    case "Prix":
                        GererInputPrix(dgv_Inventaire);
                        break;
                    case "DateEntree":
                        string cellDateEntreeTexte = (string)(dgv_Inventaire.CurrentCell.Value ?? "");
                        DateTime dateNonUtilise;
                        if (!(DateTime.TryParseExact(cellDateEntreeTexte, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNonUtilise)))
                        {
                            dgv_Inventaire.CurrentCell.Value = "";
                        }
                        break;
                    case "DateSortie":
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
        ///  Supprime le lien avec le fichier actuellement ouvert, et permet de faire "page blanche".
        /// </summary>
        private void TSMenuItem_Fichier_Nouveau_Click(object sender, EventArgs e)
        {
            cheminFichierOuvert = "";
            FairePageBlanche();
        }



        /// <summary>
        ///  Si on change le texte de la textBox Recherche, alors recherche dans l'inventaire, dans les marques ou dans les types selon la page tabControl choisi.
        /// </summary>
        private void txt_RechercheInventaire_TextChanged(object sender, EventArgs e)
        {
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                switch (tabControl_Inventaire.SelectedIndex)
                {
                    case 0:
                        gestionControlleurRef.RechercherInventaire(txt_Recherche.Text);
                        break;
                    case 1:
                        gestionControlleurRef.RechercherMarque(txt_Recherche.Text);
                        break;
                    case 2:
                        gestionControlleurRef.RechercherType(txt_Recherche.Text);
                        break;
                }
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                gestionControlleurRef.RechercherFacture(txt_Recherche.Text);
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
        ///  Permet de sauvegarder dans la variable "factureRowsCharge" les factures, cette procédure est nécessaire dans la recherche dans la facture car à chaque recherche on "vide" la dataGridView, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        public void DefinirFactureRowsCharge()
        {
            factureRowsCharge.Clear();
            foreach (DataGridViewRow row in dgv_Facture.Rows)
            {
                factureRowsCharge.Add(row);
            }
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "marquesCharge" la listBox marque, cette procédure est nécessaire dans la recherche de marque car à chaque recherche on "vide" la listBox, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        private void DefinirMarqueCharge()
        {
            marquesCharge = lb_Marque.Items.OfType<string>().ToList();
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "typesCharge" la listBox type, cette procédure est nécessaire dans la recherche de type car à chaque recherche on "vide" la listBox, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        private void DefinirTypeCharge()
        {
            typesCharge = lb_Type.Items.OfType<string>().ToList();
        }

        /// <summary>
        ///  Permet de sauvegarder dans la variable "prestationsCharge" la listBox prestation, cette procédure est nécessaire dans la recherche de prestation car à chaque recherche on "vide" la listBox, il faut donc la re-remplir pour refaire une recherche.
        /// </summary>
        private void DefinirPrestationCharge()
        {
            prestationsCharge = lb_Prestation.Items.OfType<string>().ToList();
        }

        /// <summary>
        ///  Si la FormGestion est redimensionné, alors cache les dateTimePicker InventaireEntreeDate, InventaireSortieDate et FactureDate.
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            dtpInventaireDateEntree.Visible = false;
            dtpInventaireDateSortie.Visible = false;
            dtpFactureDate.Visible = false;
        }

        /// <summary>
        ///  Si le filtre de recherche est changé, alors relance la recherche.
        /// </summary>
        private void cb_FiltreRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            gestionControlleurRef.RechercherInventaire(txt_Recherche.Text);
        }

        /// <summary>
        ///  Arrete la recherche et lance l'ajout d'une ligne à l'inventaire ou aux factures.
        /// </summary>
        private void btn_AjouterLigneInventaire_Click_1(object sender, EventArgs e)
        {
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                cb_FiltreRecherche.SelectedIndex = 0;
                txt_Recherche.Text = "";
                AjouterLigneInventaire();
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                cb_FiltreRecherche.SelectedIndex = 0;
                txt_Recherche.Text = "";
                AjouterLigneFacture();
            }
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
                    if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
                    {
                        gestionControlleurRef.SupprimerLignesInventaire();
                    }
                    else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
                    {
                        gestionControlleurRef.SupprimerLignesFacture();
                    }
                }
            }
            else
            {
                if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
                {
                    gestionControlleurRef.SupprimerLignesInventaire();
                }
                else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
                {
                    gestionControlleurRef.SupprimerLignesFacture();
                }
            }
        }

        /// <summary>
        ///  Lance l'insertion de ligne à l'inventaire ou aux factures.
        /// </summary>
        private void btn_InsererLigneInventaire_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_Recherche.Text = "";
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                gestionControlleurRef.InsererLigneInventaire((int)dgv_Inventaire.CurrentRow.Cells[0].Value);
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                gestionControlleurRef.InsererLigneFacture((int)dgv_Facture.CurrentRow.Cells[0].Value);
            }
        }

        /// <summary>
        ///  Lance la copie de ligne de l'inventaire ou aux factures.
        /// </summary>
        private void btn_CopierLigneInventaire_Click(object sender, EventArgs e)
        {
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                gestionControlleurRef.CopierLignesInventaire();
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                gestionControlleurRef.CopierLignesFacture();
            }
        }

        /// <summary>
        ///  Lance le fait de coller les lignes copiées à l'inventaire ou aux factures.
        /// </summary>
        private void btn_CollerLigneInventaire_Click(object sender, EventArgs e)
        {
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                gestionControlleurRef.CollerLignesInventaire();
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                gestionControlleurRef.CollerLignesFacture();
            }
        }

        /// <summary>
        ///  Lance la coupe de ligne de l'inventaire ou aux factures.
        /// </summary>
        private void btn_CouperLigneInventaire_Click(object sender, EventArgs e)
        {
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                gestionControlleurRef.CouperLignesInventaire();
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                gestionControlleurRef.CouperLignesFacture();
            }
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
                    if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
                    {
                        gestionControlleurRef.ViderLignesInventaire();
                    }
                    else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
                    {
                        gestionControlleurRef.ViderLignesFacture();
                    }
                }
            }
            else
            {
                if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
                {
                    gestionControlleurRef.ViderLignesInventaire();
                }
                else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
                {
                    gestionControlleurRef.ViderLignesFacture();
                }
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
        ///  Permet d'utiliser les fameux raccourcis CTRL+X pour couper, CTRL+C pour copier, CTRL+V pour coller, ici pour l'inventaire.
        /// </summary>
        private void dgv_Inventaire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CopierLigne.Enabled)
                {
                    gestionControlleurRef.CopierLignesInventaire();
                }
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CollerLigne.Enabled)
                {
                    gestionControlleurRef.CollerLignesInventaire();
                }
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                if (dgv_Inventaire.SelectedRows.Count != 0 && btn_CouperLigne.Enabled)
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


        private void tabControl_Onglets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ongletPrincipalActuel = (ongletPrincipal)tabControl_Onglets.SelectedIndex;
            if (ongletPrincipalActuel == ongletPrincipal.INVENTAIRE)
            {
                ChangerContenuCbFiltre(ongletPrincipal.INVENTAIRE);
                tabControl_Inventaire_Selecting(null, null);
                dgv_Inventaire_SelectionChanged(null, null);
                btn_CollerLigne.Enabled = (rowsInventaireCopiee.Count != 0);
            }
            else if (ongletPrincipalActuel == ongletPrincipal.FACTURES)
            {
                ChangerContenuCbFiltre(ongletPrincipal.FACTURES);
                DefinirVisibiliteToolStrip(visibiliteToolstrip.MODIFDGV);
                tabControl_FactureOnglet_SelectedIndexChanged(null, null);
                dgv_Facture_SelectionChanged(null, null);
                btn_CollerLigne.Enabled = (rowsFactureCopiee.Count != 0);
            }
        }

        /// <summary>
        ///  Ajoute une ligne dans la dataGridView facture et sauvegarde ses lignes temporairement dans une collection.
        /// </summary>
        private void AjouterLigneFacture()
        {
            dgv_Facture.Sort(dgv_Facture.Columns[0], ListSortDirection.Ascending);
            dgv_Facture.Rows.Add();
            DefinirFactureRowsCharge();
        }


        /// <summary>
        ///  Actualise la colonne index de la dataGridView facture pour que les nombres se suivent.
        /// </summary>
        public void ActualiserIndexLignesFacture()
        {
            for (int i = 0; i < dgv_Facture.RowCount; i++)
            {
                dgv_Facture.Rows[i].Cells["IndexFacture"].Value = i;
            }
        }

        /// <summary>
        /// Si des lignes sont supprimés, alors actualise les index des lignes.
        /// </summary>
        private void dgv_Facture_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualiserIndexLignesFacture();
        }

        /// <summary>
        ///  Si des lignes sont ajoutées, mets à jour les cellules nécessaires et actualise la colonne Index.
        /// </summary>
        private void dgv_Facture_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualiserIndexLignesFacture();
            DataGridViewComboBoxCell comboBoxCellPrestation = (DataGridViewComboBoxCell)dgv_Facture.Rows[e.RowIndex].Cells[2];
            UpdateCbCellPrestation(comboBoxCellPrestation);
        }


        /// <summary>
        ///  Si scroll sur la dataGridView facture, alors cache le dateTimePicker.
        /// </summary>
        private void dgv_Facture_Scroll(object sender, ScrollEventArgs e)
        {
            dtpFactureDate.Visible = false;
        }

        /// <summary>
        ///  Si clique sur une cellule appartenant à la colonne de la date de la facture, alors fait apparaitre un dateTimePicker.
        /// </summary>
        private void dgv_Facture_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                dgv_Facture.EndEdit();
                DataGridViewCell cell = dgv_Facture.Rows[e.RowIndex].Cells[e.ColumnIndex];
                switch (dgv_Facture.Columns[e.ColumnIndex].Name)
                {
                    case "DateFacture":
                        dtpFactureDate.Visible = false;
                        rectangleDtpFactureDate = dgv_Facture.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        dtpFactureDate.Size = new Size(rectangleDtpFactureDate.Width, rectangleDtpFactureDate.Height);
                        dtpFactureDate.Location = new Point(rectangleDtpFactureDate.X, rectangleDtpFactureDate.Y);
                        dtpFactureDate.Visible = true;
                        if (cell.Value != null)
                        {
                            if (cell.Value.ToString() != "")
                            {
                                dtpFactureDate.Value = DateTime.ParseExact(cell.Value.ToString() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        break;
                    default:
                        dtpFactureDate.Visible = false;
                        break;
                }
            }
        }

        /// <summary>
        ///  Si double clique sur une cellule appartenant à la colonne Index alors séléctionne la ligne.
        /// </summary>
        private void dgv_Facture_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                switch (dgv_Facture.Columns[e.ColumnIndex].Name)
                {
                    case "IndexFacture":
                        dgv_Facture.Rows[e.RowIndex].Selected = true;
                        break;
                }
            }
        }

        /// <summary>
        ///  Permet d'imposer des conditions selon la colonne quand on entre des valeurs dans les cellules.
        /// </summary>
        private void dgv_Facture_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            dgv_Facture.CellValueChanged -= dgv_Facture_CellValueChanged;
            string currentCellStr;
            DataGridViewCell currentCell = dgv_Facture.CurrentCell;
            if (currentCell != null)
            {
                switch (dgv_Facture.Columns[currentCell.ColumnIndex].Name)
                {
                    case "DateFacture":
                        currentCellStr = (string)(currentCell.Value ?? "");
                        DateTime dateNonUtilise;
                        if (!(DateTime.TryParseExact(currentCellStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNonUtilise)))
                        {
                            currentCell.Value = "";
                        }
                        break;
                    case "PrixHT":
                        GererInputPrix(dgv_Facture);
                        if (currentCell.Value != null)
                        {
                            DefinirCellsLieTVA(currentCell, false);
                        }
                        else
                        {
                            DefinirCellsLieTVA(currentCell, false, true);
                        }
                        break;
                    case "PrixTTC":
                        GererInputPrix(dgv_Facture);
                        if (currentCell.Value != null)
                        {
                            DefinirCellsLieTVA(dgv_Facture.Rows[currentCell.RowIndex].Cells[5], true);
                        }
                        else
                        {
                            DefinirCellsLieTVA(dgv_Facture.Rows[currentCell.RowIndex].Cells[5], true, true);
                        }
                        break;
                    case "PrestationFacture":
                        if (dgv_Facture.Rows[currentCell.RowIndex].Cells[5].Value != null)
                        {
                            DefinirCellsLieTVA(dgv_Facture.Rows[currentCell.RowIndex].Cells[5], false);
                        }
                        else if (dgv_Facture.Rows[currentCell.RowIndex].Cells[6].Value != null)
                        {
                            DefinirCellsLieTVA(dgv_Facture.Rows[currentCell.RowIndex].Cells[5], true);
                        }
                        break;
                }
            }
            dgv_Facture.CellValueChanged += dgv_Facture_CellValueChanged;
        }

        /// <summary>
        ///  Permet d'utiliser les fameux raccourcis CTRL+X pour couper, CTRL+C pour copier, CTRL+V pour coller, ici pour les factures.
        /// </summary>
        private void dgv_Facture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (dgv_Facture.SelectedRows.Count != 0 && btn_CopierLigne.Enabled)
                {
                    gestionControlleurRef.CopierLignesFacture();
                }
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                if (dgv_Facture.SelectedRows.Count != 0 && btn_CollerLigne.Enabled)
                {
                    gestionControlleurRef.CollerLignesFacture();
                }
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                if (dgv_Facture.SelectedRows.Count != 0 && btn_CouperLigne.Enabled)
                {
                    gestionControlleurRef.CouperLignesFacture();
                }
            }
        }

        /// <summary>
        ///  Selon le nombre de ligne selectionné, définit quel bouton du toolStrip est activé ou non, ici pour les factures.
        /// </summary>
        private void dgv_Facture_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Facture.SelectedRows.Count != 0)
            {
                btn_SupprimerLigne.Enabled = true;
                btn_CopierLigne.Enabled = true;
                btn_CouperLigne.Enabled = true;
                btn_ViderLigne.Enabled = true;
            }
            else
            {
                btn_SupprimerLigne.Enabled = false;
                btn_CopierLigne.Enabled = false;
                btn_CouperLigne.Enabled = false;
                btn_ViderLigne.Enabled = false;
            }

            if (dgv_Facture.SelectedRows.Count == 1)
            {
                btn_InsererLigne.Enabled = true;
            }
            else
            {
                btn_InsererLigne.Enabled = false;
            }
        }

        /// <summary>
        ///  Récupère la valeur de la dateTimePicker pour remplir la cellule.
        /// </summary>
        private void dtpFactureDate_TextChange(object? sender, EventArgs e)
        {
            dgv_Facture.CurrentCell.Value = dtpFactureDate.Text.ToString();
        }

        private void ChangerContenuCbFiltre(ongletPrincipal onglet, bool clear = true)
        {
            if (clear)
            {
                cb_FiltreRecherche.Items.Clear();
            }
            cb_FiltreRecherche.Items.Add("Pas de filtre");
            switch (onglet)
            {
                case ongletPrincipal.INVENTAIRE:
                    for (int i = 1; i < dgv_Inventaire.Columns.Count; i++)
                    {
                        cb_FiltreRecherche.Items.Add(dgv_Inventaire.Columns[i].HeaderText);
                    }
                    break;
                case ongletPrincipal.FACTURES:
                    for (int i = 1; i < dgv_Facture.Columns.Count; i++)
                    {
                        cb_FiltreRecherche.Items.Add(dgv_Facture.Columns[i].HeaderText);
                    }
                    break;
            }
            cb_FiltreRecherche.SelectedIndex = 0;
        }

        private void tabControl_Onglets_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void ts_Inventaire_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void lbl_RechercheInventaire_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void menuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void tlp_Main_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void tlp_Millieu_MouseClick(object sender, MouseEventArgs e)
        {
            RetirerFocusDeTxtRecherche();
        }

        private void RetirerFocusDeTxtRecherche()
        {
            lbl_RechercheInventaire.Focus();
        }

        private void GererInputPrix(DataGridView dgv)
        {
            string cellPrixTexte = (string)(dgv.CurrentCell.Value ?? " ");
            string cellPrixTexteSansEuro = cellPrixTexte;
            char premierCharPrixTexteSansEuro = cellPrixTexteSansEuro[0];
            char dernierCharPrixTexteSansEuro = cellPrixTexteSansEuro[cellPrixTexteSansEuro.Length - 1];
            char caractereAvantEuro;
            bool virguleMalPlace = false;
            bool charEuroMalPlace = false;
            cellPrixTexteSansEuro = cellPrixTexteSansEuro.Replace("€", "");
            if (premierCharPrixTexteSansEuro == ',' || premierCharPrixTexteSansEuro == '.' || dernierCharPrixTexteSansEuro == ',' || dernierCharPrixTexteSansEuro == '.' || cellPrixTexteSansEuro.Count(f => f == ',') > 1 || cellPrixTexteSansEuro.Count(f => f == '.') > 1)
            {
                virguleMalPlace = true;
            }
            if (!cellPrixTexte.Contains("€"))
            {
                cellPrixTexte = cellPrixTexte + "€";
            }
            if (cellPrixTexte.IndexOf("€") == (cellPrixTexte.Length - 1))
            {
                caractereAvantEuro = cellPrixTexte[cellPrixTexte.IndexOf("€") - 1];
                charEuroMalPlace = !(char.IsDigit(caractereAvantEuro) && caractereAvantEuro != 0);
            }
            else
            {
                charEuroMalPlace = true;
            }
            if (cellPrixTexte.Any(char.IsDigit))
            {
                cellPrixTexte = cellPrixTexte.Replace(",", "");
                cellPrixTexte = cellPrixTexte.Replace(".", "");
                cellPrixTexte = cellPrixTexte.Replace("€", "");
                if ((charEuroMalPlace || virguleMalPlace || !cellPrixTexte.All(char.IsDigit)))
                {
                    dgv.CurrentCell.Value = null;
                }
                else if (dgv.CurrentCell.Value != null)
                {
                    string actuelleText = dgv.CurrentCell.Value.ToString() as string ?? string.Empty;
                    dgv.CurrentCell.Value = actuelleText.Replace('.', ',');
                    if (!((string)dgv.CurrentCell.Value).Contains("€"))
                    {
                        dgv.CurrentCell.Value = dgv.CurrentCell.Value + "€";
                    }
                }
            }
            else
            {
                dgv.CurrentCell.Value = null;
            }
        }

        private void DefinirCellsLieTVA(DataGridViewCell cellHT, bool depuisCellTTC, bool clearCellsLieTVA = false)
        {
            float tva = -1f;
            DataGridViewCell dgvCellPrixTTC = dgv_Facture.Rows[cellHT.RowIndex].Cells[6];
            DataGridViewCell dgvCellDifference = dgv_Facture.Rows[cellHT.RowIndex].Cells[7];
            DataGridViewCell dgvCellPrestation = dgv_Facture.Rows[cellHT.RowIndex].Cells[2];
            if (clearCellsLieTVA)
            {
                cellHT.Value = null;
                dgvCellPrixTTC.Value = null;
                dgvCellDifference.Value = null;
                return;
            }
            else if (dgvCellPrestation.Value != null)
            {
                tva = SeparerPrestationNomPourcentage((string)dgvCellPrestation.Value).Item2;
            }
            else
            {
                return;
            }
            float prixHT;
            string cellHTStr = (string)(cellHT.Value ?? "");
            string cellprixTTCStr = (string)(dgvCellPrixTTC.Value ?? "");
            DataGridViewComboBoxCell prestationCell = (DataGridViewComboBoxCell)dgv_Facture.Rows[cellHT.RowIndex].Cells[2];
            int indexPrestation = prestationCell.Items.IndexOf(((string)prestationCell.Value) ?? "");
            if (depuisCellTTC && cellprixTTCStr != "")
            {
                float prixTTC = float.Parse(cellprixTTCStr.Replace("€", ""));
                cellHT.Value = Math.Round((decimal)(prixTTC / (1 + tva)), 2) + "€";
                cellHTStr = (string)(cellHT.Value ?? "");
                prixHT = float.Parse(cellHTStr.Replace("€", ""));
                dgvCellDifference.Value = Math.Round((decimal)(prixHT * tva), 2) + "€";
            }
            else
            {
                prixHT = float.Parse(cellHTStr.Replace("€", ""));
                dgvCellPrixTTC.Value = Math.Round((decimal)(prixHT * (1 + tva)), 2) + "€";
                dgvCellDifference.Value = Math.Round((decimal)(prixHT * tva), 2) + "€";
            }

        }

        private void dgv_Facture_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgv_Facture.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        ///  Ajoute la prestation dans la listBox prestation et actualise ce qui est nécessaire.
        /// </summary>
        private void btn_AjoutPrestation_Click(object sender, EventArgs e)
        {
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_Recherche.Text = "";
            lb_Prestation.Items.Add(txt_AjoutPrestationNom.Text + " (" + nupd_AjoutPourcentageTVA.Value + "%)");
            lb_Prestation.SelectedIndex = -1;
            txt_AjoutPrestationNom.Text = "";
            nupd_AjoutPourcentageTVA.Value = 0;
            UpdateFactureColonnePrestation();
            DefinirPrestationCharge();
        }

        /// <summary>
        ///  Supprime la prestation choisi et mets à jour ce qui est nécessaire.
        /// </summary>
        private void btn_SupprPrestation_Click(object sender, EventArgs e)
        {
            string saveSelectedItemText = lb_Prestation.SelectedItem.ToString() ?? "";
            cb_FiltreRecherche.SelectedIndex = 0;
            txt_Recherche.Text = "";
            lb_Prestation.Items.Remove(saveSelectedItemText);
            lb_Prestation.SelectedIndex = -1;
            txt_AjoutPrestationNom.Text = "";
            nupd_AjoutPourcentageTVA.Value = 0;
            UpdateFactureColonnePrestation();
            DefinirPrestationCharge();
        }

        /// <summary>
        ///  Activé ou non le bouton AjouterPrestation selon si le texte AjoutPrestation est vide, pareil pour le numericUpDown ajoutPourcentageTVA.
        /// </summary>
        private void txt_AjoutPrestationNom_TextChanged(object sender, EventArgs e)
        {
            if (txt_AjoutPrestationNom.Text != "" && nupd_AjoutPourcentageTVA.Value > 0)
            {
                btn_AjoutPrestation.Enabled = true;
            }
            else
            {
                btn_AjoutPrestation.Enabled = false;
            }
        }

        /// <summary>
        ///  Activé ou non le bouton AjouterPrestation selon si le texte AjoutPrestation est vide, pareil pour le numericUpDown ajoutPourcentageTVA.
        /// </summary>
        private void nupd_AjoutPourcentageTVA_ValueChanged(object sender, EventArgs e)
        {
            if (txt_AjoutPrestationNom.Text != "" && nupd_AjoutPourcentageTVA.Value > 0)
            {
                btn_AjoutPrestation.Enabled = true;
            }
            else
            {
                btn_AjoutPrestation.Enabled = false;
            }
        }

        /// <summary>
        ///  Permet de déselectionné la prestation choisi dans la listBox prestation si on appuie sur échappe.
        /// </summary>
        private void lb_Prestation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lb_Prestation.SelectedIndex = -1;
            }
        }

        /// <summary>
        ///  Activé ou non le bouton SupprimerPrestation selon si une prestation est sélectionné.
        /// </summary>
        private void lb_Prestation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Prestation.SelectedIndex != -1)
            {
                btn_SupprimerPrestation.Enabled = true;
            }
            else
            {
                btn_SupprimerPrestation.Enabled = false;
            }
        }

        private string AssemblerPrestationNomPourcentage(string nomSepare, float pourcentageSepare)
        {
            return nomSepare + " (" + pourcentageSepare * 100 + "%)";
        }

        private (string, float) SeparerPrestationNomPourcentage(string stringOriginal)
        {
            string nomSepare = stringOriginal;
            string strPourcentageSepare;
            float pourcentageSepare;
            nomSepare = nomSepare.Substring(0, nomSepare.IndexOf('(') - 1);
            strPourcentageSepare = stringOriginal.Substring(stringOriginal.IndexOf('(') + 1, (stringOriginal.IndexOf(')') - stringOriginal.IndexOf('(')) - 2);
            pourcentageSepare = float.Parse(strPourcentageSepare) / 100;
            return (nomSepare, pourcentageSepare);
        }

        private void DefinirVariablesDefaut()
        {
            defautMarquesCharge = marquesCharge.ToList();
            defautTypesCharge = typesCharge.ToList();
            defautPrestationsCharge = prestationsCharge.ToList();
        }

        private void FairePageBlanche()
        {
            dtpInventaireDateEntree.Visible = false;
            dtpInventaireDateSortie.Visible = false;
            dtpFactureDate.Visible = false;
            dgv_Inventaire.Rows.Clear();
            dgv_Facture.Rows.Clear();
            lb_Marque.Items.Clear();
            lb_Type.Items.Clear();
            lb_Prestation.Items.Clear();
            inventaireRowsCharge.Clear();
            factureRowsCharge.Clear();
            marquesCharge.Clear();
            typesCharge.Clear();
            prestationsCharge.Clear();
            foreach (string marque in defautMarquesCharge)
            {
                lb_Marque.Items.Add(marque);
            }
            foreach (string type in defautTypesCharge)
            {
                lb_Type.Items.Add(type);
            }
            foreach (string prestation in defautPrestationsCharge)
            {
                lb_Prestation.Items.Add(prestation);
            }
            DefinirMarqueCharge();
            DefinirTypeCharge();
            DefinirPrestationCharge();
            changerFormTitre(true);
            TSMenuItem_Fichier_Sauvegarder.Enabled = false;
            btn_Sauvegarder.Enabled = false;
        }

        private void tabControl_FactureOnglet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_Facture.SelectedIndex != 0)
            {
                DefinirVisibiliteToolStrip(visibiliteToolstrip.CACHEAVECFOND);
            }
            else if (tabControl_Facture.SelectedIndex == 0)
            {
                DefinirVisibiliteToolStrip(visibiliteToolstrip.MODIFDGV);
            }
            switch (tabControl_Facture.SelectedIndex)
            {
                case 0:
                    txt_Recherche.PlaceholderText = "Rechercher dans les factures";
                    ChangerContenuCbFiltre(ongletPrincipal.FACTURES);
                    cb_FiltreRecherche.Visible = true;
                    lbl_Filtre.Visible = true;
                    break;
                case 1:
                    txt_Recherche.PlaceholderText = "Rechercher dans les prestations";
                    cb_FiltreRecherche.Visible = false;
                    lbl_Filtre.Visible = false;
                    break;
            }
        }

        private void txt_AjoutSiteWebUrl_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                btn_AjouterSiteWeb.Enabled = true;
            }
            else
            {
                btn_AjouterSiteWeb.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex != -1)
            {
                btn_SupprimerSiteWeb.Enabled = true;
                btn_ModifierSiteWeb.Enabled = true;
                btn_AccederSiteWeb.Enabled = true;
            }
            else
            {
                btn_SupprimerSiteWeb.Enabled = false;
                btn_ModifierSiteWeb.Enabled = false;
                btn_AccederSiteWeb.Enabled = false;
            }
        }

        private void btn_AjouterSiteWeb_Click(object sender, EventArgs e)
        {
            string siteNom;
            string url = txt_AjoutSiteWebUrl.Text;
            if (!url.Contains("www."))
            {
                url = url.Insert(0, "www.");
            }
            if (txt_AjoutSiteWebNom.Text != "")
            {
                siteNom = txt_AjoutSiteWebNom.Text;
            }
            else
            {
                siteNom = url;
            }
            sitesFavorisCharge.Add(new SiteFavorisLigne(siteNom, url));
            UpdateListBoxFromDataSource(lb_SitesFav);
            txt_AjoutSiteWebNom.Text = "";
            txt_AjoutSiteWebUrl.Text = "";
        }

        private void lb_SitesFav_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AccederSiteWebSelected();
        }

        private void btn_AccederSiteWeb_Click(object sender, EventArgs e)
        {
            if (!tentativeAccesSite)
            {
                AccederSiteWebSelected();
            }
        }

        private async void AccederSiteWebSelected()
        {
            tentativeAccesSite = true;
            if (lb_SitesFav.SelectedIndex != -1)
            {
                try
                {
                    ProcessStartInfo psInfo = new ProcessStartInfo
                    {
                        FileName = ((SiteFavorisLigne)lb_SitesFav.SelectedItem).url,
                        UseShellExecute = true
                    };
                    Process.Start(psInfo);
                }
                catch (Exception exc)
                {
                    await ClignoterRougeBtnAccederSite();
                    await Task.Delay(100);
                    await ClignoterRougeBtnAccederSite();
                }
                tentativeAccesSite = false;
            }
        }

        private async Task ClignoterRougeBtnAccederSite()
        {
            Color ancienneForeColorBtnAccederSiteWeb = btn_AccederSiteWeb.ForeColor;
            btn_AccederSiteWeb.ForeColor = Color.Red;
            await Task.Delay(100);
            btn_AccederSiteWeb.ForeColor = ancienneForeColorBtnAccederSiteWeb;
        }

        public void UpdateListBoxFromDataSource(ListBox lb)
        {
            object dataSourceSaved = lb.DataSource;
            lb.DataSource = null;
            lb.DataSource = dataSourceSaved;
        }

        private void btn_SupprimerSiteWeb_Click(object sender, EventArgs e)
        {
            sitesFavorisCharge.Remove((SiteFavorisLigne)lb_SitesFav.SelectedItem);
            UpdateListBoxFromDataSource(lb_SitesFav);
        }
    }
}