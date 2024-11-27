using FranceInformatiqueInventaire.dal;
using FranceInformatiqueInventaire.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static FranceInformatiqueInventaire.Model.EnumPeriodes;

namespace FranceInformatiqueInventaire.Controlleur
{
    /// <summary>
    ///  Classe utilisée comme controlleur de l'application.
    /// </summary>
    public partial class GestionControlleur
    {
        private FormGestion formGestionRef;
        private DataGridView dgv_Inventaire;
        private DataGridView dgv_Facture;
        private TextBox txt_Recherche;
        private ToolStripButton btn_CollerLigne;
        BddManager bddManagerRef;
        ToolStripMenuItem TSMenuItem_Fichier_Sauvegarder;
        private List<DataGridViewRow> inventaireRowsCharge;
        private List<DataGridViewRow> factureRowsCharge;
        private List<DataGridViewRow> rowsInventaireCopiee = new List<DataGridViewRow>();
        private ComboBox cb_FiltreRecherche;
        private ListBox lb_Marque;
        private ListBox lb_Type;
        private ListBox lb_Prestation;
        private List<string> marquesCharge = new List<string>();
        private List<string> typesCharge = new List<string>();
        private bool couperLignes;
        private List<DataGridViewRow> rowsFactureCopiee = new List<DataGridViewRow>(); 

        public GestionControlleur(FormGestion formGestionRef, DataGridView dgv_Inventaire, TextBox txt_Recherche, ToolStripButton btn_CollerLigne, BddManager bddManagerRef, ToolStripMenuItem TSMenuItem_Fichier_Sauvegarder, List<DataGridViewRow> inventaireRowsCharge, List<DataGridViewRow> rowsInventaireCopiee, ComboBox cb_FiltreRecherche, ListBox lb_Marque, ListBox lb_Type, List<string> marquesCharge, List<string> typesCharge, bool couperLignes, DataGridView dgv_Facture, List<DataGridViewRow>  factureRowsCharge, List<DataGridViewRow> rowsFactureCopiee, ListBox lb_Prestation)
        {
            this.formGestionRef = formGestionRef;
            this.dgv_Inventaire = dgv_Inventaire;
            this.txt_Recherche = txt_Recherche;
            this.btn_CollerLigne = btn_CollerLigne;
            this.bddManagerRef = bddManagerRef;
            this.TSMenuItem_Fichier_Sauvegarder = TSMenuItem_Fichier_Sauvegarder;
            this.inventaireRowsCharge = inventaireRowsCharge;
            this.rowsInventaireCopiee = rowsInventaireCopiee;
            this.cb_FiltreRecherche = cb_FiltreRecherche;
            this.lb_Marque = lb_Marque;
            this.lb_Type = lb_Type;
            this.marquesCharge = marquesCharge;
            this.typesCharge = typesCharge;
            this.couperLignes = couperLignes;
            this.dgv_Facture = dgv_Facture;
            this.factureRowsCharge = factureRowsCharge;
            this.rowsFactureCopiee = rowsFactureCopiee;
            this.lb_Prestation = lb_Prestation;
        }

        /// <summary>
        ///  Supprime des lignes de la dataGriwView inventaire en mettant à jour certaines choses par la même occasion.
        /// </summary>
        public void SupprimerLignesInventaire()
        {
            DataGridViewSelectedRowCollection savedSelectedRows;
            savedSelectedRows = dgv_Inventaire.SelectedRows;
            txt_Recherche.Text = "";
            foreach (DataGridViewRow ligne in savedSelectedRows)
            {
                dgv_Inventaire.Rows.Remove(ligne);
            }
            formGestionRef.ActualiserIndexLignesInventaire();
            formGestionRef.DefinirInventaireRowsCharge();
        }

        /// <summary>
        ///  Permet de récupérer le nom du fichier directement et la non le chemin complet.
        /// </summary>
        /// <returns>Retourne le nom du fichier.</returns>
        public string GetFileNameSansChemin()
        {
            string temp = formGestionRef.cheminFichierOuvert.Substring(formGestionRef.cheminFichierOuvert.LastIndexOf("\\") + 1);
            return temp.Remove(temp.Count() - 3);
        }

        /// <summary>
        ///  Permet d'ouvrir un fichier si la boite de dialogue de fichier a choisi un fichier, on récupérer le chemin du fichier.
        /// </summary>
        /// <returns>Retourne le chemin du fichier choisi.</returns>
        public string OuvrirFichierDb()
        {
            dgv_Inventaire.EndEdit();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "SQLite Database|*.db";
            fileDialog.Title = "Choisissez le fichier à ouvrir";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                formGestionRef.cheminFichierOuvert = fileDialog.FileName;
                formGestionRef.titreFichierOuvert = GetFileNameSansChemin();
                return fileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        ///  Ouvre une boite de dialogue de fichier, et permet de "créer" un nouveau fichier de sauvegarde.
        /// </summary>
        public void SauvegarderSous()
        {
            dgv_Inventaire.EndEdit();
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "SQLite Database|*.db";
            fileDialog.Title = "Choisissez le nom du fichier";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                dgv_Inventaire.Enabled = false;
                formGestionRef.cheminFichierOuvert = fileDialog.FileName;
                formGestionRef.titreFichierOuvert = GetFileNameSansChemin();
                bddManagerRef.EcrireBdd(formGestionRef.cheminFichierOuvert);
                formGestionRef.changerFormTitre(false);
                TSMenuItem_Fichier_Sauvegarder.Enabled = true;
                dgv_Inventaire.Enabled = true;
            }
        }
        
        /// <summary>
        ///  Permet de rechercher dans l'inventaire en supprimant les rows de la dataGridView inventaire qui ne contiennent pas l'élément recherché, on parcours chaque colonne ou une colonne spécifique selon le filtre choisi.
        /// </summary>
        /// <param name="texteARechercher">Le texte qui sera recherché dans les cellules.</param>
        public void RechercherInventaire(string texteARechercher)
        {
            texteARechercher = texteARechercher.ToLower();
            bool trouverTexteDansRow = false;
            dgv_Inventaire.Rows.Clear();
            foreach (DataGridViewRow row in inventaireRowsCharge)
            {
                dgv_Inventaire.Rows.Add(row);
            }
            List<int> indexsRowASupprimer = new List<int>();
            for (int i = 0; i < dgv_Inventaire.Rows.Count; i++)
            {
                int filtreSelectedIndex = cb_FiltreRecherche.SelectedIndex;
                if (filtreSelectedIndex > 0)
                {
                    DataGridViewCell cell = dgv_Inventaire.Rows[i].Cells[filtreSelectedIndex];
                    if (!(cell.Value == null))
                    {
                        string cellText = (cell.Value.ToString()) ?? "";
                        cellText = cellText.ToLower();
                        if (cellText.Contains(texteARechercher))
                        {
                            trouverTexteDansRow = true;
                        }
                    }
                }
                else
                {
                    for (int e = 0; e < dgv_Inventaire.Rows[i].Cells.Count; e++)
                    {
                        DataGridViewCell cell = dgv_Inventaire.Rows[i].Cells[e];
                        if (!(cell.Value == null))
                        {
                            string cellText = (cell.Value.ToString()) ?? "";
                            cellText = cellText.ToLower();
                            if (cellText.Contains(texteARechercher))
                            {
                                trouverTexteDansRow = true;
                                break;
                            }
                        }
                    }
                }

                if (!trouverTexteDansRow)
                {
                    if (!(indexsRowASupprimer.Contains(i)))
                    {
                        indexsRowASupprimer.Insert(0, i);
                    }
                }
                trouverTexteDansRow = false;
            }
            foreach (int i in indexsRowASupprimer)
            {
                dgv_Inventaire.Rows.RemoveAt(i);
            }
        }

        /// <summary>
        ///  Permet de rechercher dans les marques en supprimant les lignes de la listBox marque qui ne contiennent pas l'élément recherché.
        /// </summary>
        /// <param name="texteARechercher">Le texte qui sera recherché dans les lignes.</param>
        public void RechercherMarque(string texteARechercher)
        {
            texteARechercher = texteARechercher.ToLower();
            bool trouverTexteDansRow = false;
            lb_Marque.Items.Clear();
            foreach (var item in marquesCharge)
            {
                lb_Marque.Items.Add(item);
            }
            List<int> indexsRowASupprimer = new List<int>();
            for (int i = 0; i < lb_Marque.Items.Count; i++)
            {
                var item = lb_Marque.Items[i];
                if (!(item == null))
                {
                    string itemText = (item.ToString()) ?? "";
                    itemText = itemText.ToLower();
                    if (itemText.Contains(texteARechercher))
                    {
                        trouverTexteDansRow = true;
                    }
                }
                if (!trouverTexteDansRow)
                {
                    if (!(indexsRowASupprimer.Contains(i)))
                    {
                        indexsRowASupprimer.Insert(0, i);
                    }
                }
                trouverTexteDansRow = false;
            }
            foreach (int i in indexsRowASupprimer)
            {
                lb_Marque.Items.RemoveAt(i);
            }

        }

        /// <summary>
        ///  Permet de rechercher dans les types en supprimant les lignes de la listBox type qui ne contiennent pas l'élément recherché.
        /// </summary>
        /// <param name="texteARechercher">Le texte qui sera recherché dans les lignes.</param>
        public void RechercherType(string texteARechercher)
        {
            texteARechercher = texteARechercher.ToLower();
            bool trouverTexteDansRow = false;
            lb_Type.Items.Clear();
            foreach (var item in typesCharge)
            {
                lb_Type.Items.Add(item);
            }
            List<int> indexsRowASupprimer = new List<int>();
            for (int i = 0; i < lb_Type.Items.Count; i++)
            {
                var item = lb_Type.Items[i];
                if (!(item == null))
                {
                    string itemText = (item.ToString()) ?? "";
                    itemText = itemText.ToLower();
                    if (itemText.Contains(texteARechercher))
                    {
                        trouverTexteDansRow = true;
                    }
                }
                if (!trouverTexteDansRow)
                {
                    if (!(indexsRowASupprimer.Contains(i)))
                    {
                        indexsRowASupprimer.Insert(0, i);
                    }
                }
                trouverTexteDansRow = false;
            }
            foreach (int i in indexsRowASupprimer)
            {
                lb_Type.Items.RemoveAt(i);
            }
        }

        /// <summary>
        ///  Permet de rechercher dans les prestations en supprimant les lignes de la listBox prestation qui ne contiennent pas l'élément recherché.
        /// </summary>
        /// <param name="texteARechercher">Le texte qui sera recherché dans les lignes.</param>
        public void RechercherPrestation(string texteARechercher)
        {
            texteARechercher = texteARechercher.ToLower();
            bool trouverTexteDansRow = false;
            lb_Prestation.Items.Clear();
            foreach (var item in prestationsCharge)
            {
                lb_Prestation.Items.Add(item);
            }
            List<int> indexsRowASupprimer = new List<int>();
            for (int i = 0; i < lb_Prestation.Items.Count; i++)
            {
                var item = lb_Prestation.Items[i];
                if (!(item == null))
                {
                    string itemText = (item.ToString()) ?? "";
                    itemText = itemText.ToLower();
                    if (itemText.Contains(texteARechercher))
                    {
                        trouverTexteDansRow = true;
                    }
                }
                if (!trouverTexteDansRow)
                {
                    if (!(indexsRowASupprimer.Contains(i)))
                    {
                        indexsRowASupprimer.Insert(0, i);
                    }
                }
                trouverTexteDansRow = false;
            }
            foreach (int i in indexsRowASupprimer)
            {
                lb_Marque.Items.RemoveAt(i);
            }

        }

        /// <summary>
        ///  Permet d'insérer une ligne dans la dataGridView inventaire, insérer avant ou après la ligne séléctionné selon l'option choisi.
        /// </summary>
        public void InsererLigneInventaire(int indexRowSelected)
        {
            txt_Recherche.Text = "";
            if (formGestionRef.insertionAvant)
            {
                dgv_Inventaire.Rows.Insert(indexRowSelected, "");
            }
            else
            {
                dgv_Inventaire.Rows.Insert(indexRowSelected + 1, "");
            }
        }

        /// <summary>
        ///  Permet de récupérer et copier les rows séléctionnées dans la liste de DataGridViewRow "rowsInventaireCopiee".
        /// </summary>
        public void CopierLignesInventaire()
        {
            rowsInventaireCopiee.Clear();
            int premierIndex = dgv_Inventaire.SelectedRows[(dgv_Inventaire.SelectedRows.Count - 1)].Index;
            int dernierIndex = dgv_Inventaire.SelectedRows[0].Index;
            DataGridViewRow nouvelleRow = new DataGridViewRow();
            nouvelleRow = (DataGridViewRow)dgv_Inventaire.Rows[0].Clone();
            foreach (DataGridViewRow row in dgv_Inventaire.SelectedRows)
            {
                nouvelleRow = new DataGridViewRow();
                nouvelleRow = (DataGridViewRow)dgv_Inventaire.Rows[0].Clone();
                for (int e = 1; e < 9; e++)
                {
                    nouvelleRow.Cells[e].Value = dgv_Inventaire.Rows[row.Index].Cells[e].Value;
                }
                rowsInventaireCopiee.Insert(0, nouvelleRow);
            }
            txt_Recherche.Text = "";
            btn_CollerLigne.Enabled = true;
        }

        /// <summary>
        ///  Permet de coller les lignes copiées dans les lignes actuellement séléctionnées.
        /// </summary>
        public void CollerLignesInventaire()
        {
            int indexPremierRowSelected = dgv_Inventaire.SelectedRows[(dgv_Inventaire.SelectedRows.Count - 1)].Index;
            int indexDernierRowSelected = dgv_Inventaire.SelectedRows[0].Index;
            txt_Recherche.Text = "";
            List<DataGridViewRow> rowsAremplacer = new List<DataGridViewRow>();
            int e = 0;
            if (((indexDernierRowSelected - indexPremierRowSelected) + 1) == rowsInventaireCopiee.Count || dgv_Inventaire.SelectedRows.Count == 1)
            {
                for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
                {
                    for (int k = 1; k < 9; k++)
                    {
                        dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[k].Value = rowsInventaireCopiee[e].Cells[k].Value;
                    }
                    e++;
                }
            }
            else
            {
                for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
                {
                    if (dgv_Inventaire.SelectedRows[i].Index <= (dgv_Inventaire.Rows.Count - 1) && e < rowsInventaireCopiee.Count)
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[k].Value = rowsInventaireCopiee[e].Cells[k].Value;
                        }
                        e++;
                    }
                }
            }
            formGestionRef.ActualiserIndexLignesInventaire();
            if (couperLignes)
            {
                couperLignes = false;
                btn_CollerLigne.Enabled = false;
                rowsInventaireCopiee.Clear();
            }
        }

        /// <summary>
        ///  Permet de "couper" donc copier puis vider les rows copiées, on peut coller une fois après la coupe.
        /// </summary>
        public void CouperLignesInventaire()
        {
            CopierLignesInventaire();
            for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
            {
                for (int e = 1; e < 9; e++)
                {
                    dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[e].Value = "";
                }
            }
            couperLignes = true;
        }

        /// <summary>
        ///  Permet de "clear" les rows séléctionnées, de les vider.
        /// </summary>
        public void ViderLignesInventaire()
        {
            for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
            {
                for (int e = 1; e < 9; e++)
                {
                    dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[e].Value = "";
                }
            }
        }

        /// <summary>
        ///  Permet de rechercher dans les factures en supprimant les rows de la dataGridView facture qui ne contiennent pas l'élément recherché, on parcours chaque colonne ou une colonne spécifique selon le filtre choisi.
        /// </summary>
        /// <param name="texteARechercher">Le texte qui sera recherché dans les cellules.</param>
        public void RechercherFacture(string texteARechercher)
        {
            texteARechercher = texteARechercher.ToLower();
            bool trouverTexteDansRow = false;
            dgv_Facture.Rows.Clear();
            foreach (DataGridViewRow row in factureRowsCharge)
            {
                dgv_Facture.Rows.Add(row);
            }
            List<int> indexsRowASupprimer = new List<int>();
            for (int i = 0; i < dgv_Facture.Rows.Count; i++)
            {
                int filtreSelectedIndex = cb_FiltreRecherche.SelectedIndex;
                if (filtreSelectedIndex > 0)
                {
                    DataGridViewCell cell = dgv_Facture.Rows[i].Cells[filtreSelectedIndex];
                    if (!(cell.Value == null))
                    {
                        string cellText = (cell.Value.ToString()) ?? "";
                        cellText = cellText.ToLower();
                        if (cellText.Contains(texteARechercher))
                        {
                            trouverTexteDansRow = true;
                        }
                    }
                }
                else
                {
                    for (int e = 0; e < dgv_Facture.Rows[i].Cells.Count; e++)
                    {
                        DataGridViewCell cell = dgv_Facture.Rows[i].Cells[e];
                        if (!(cell.Value == null))
                        {
                            string cellText = (cell.Value.ToString()) ?? "";
                            cellText = cellText.ToLower();
                            if (cellText.Contains(texteARechercher))
                            {
                                trouverTexteDansRow = true;
                                break;
                            }
                        }
                    }
                }

                if (!trouverTexteDansRow)
                {
                    if (!(indexsRowASupprimer.Contains(i)))
                    {
                        indexsRowASupprimer.Insert(0, i);
                    }
                }
                trouverTexteDansRow = false;
            }
            foreach (int i in indexsRowASupprimer)
            {
                dgv_Facture.Rows.RemoveAt(i);
            }
        }

        /// <summary>
        ///  Supprime des lignes de la dataGriwView facture en mettant à jour certaines choses par la même occasion.
        /// </summary>
        public void SupprimerLignesFacture()
        {
            DataGridViewSelectedRowCollection savedSelectedRows;
            savedSelectedRows = dgv_Facture.SelectedRows;
            txt_Recherche.Text = "";
            foreach (DataGridViewRow ligne in savedSelectedRows)
            {
                dgv_Facture.Rows.Remove(ligne);
            }
            formGestionRef.ActualiserIndexLignesFacture();
            formGestionRef.DefinirFactureRowsCharge();
        }

        /// <summary>
        ///  Permet d'insérer une ligne dans la dataGridView facture, insérer avant ou après la ligne séléctionné selon l'option choisi.
        /// </summary>
        public void InsererLigneFacture(int indexRowSelected)
        {
            txt_Recherche.Text = "";
            if (formGestionRef.insertionAvant)
            {
                dgv_Facture.Rows.Insert(indexRowSelected, "");
            }
            else
            {
                dgv_Facture.Rows.Insert(indexRowSelected + 1, "");
            }
        }

        /// <summary>
        ///  Permet de récupérer et copier les rows séléctionnées dans la liste de DataGridViewRow "rowsFactureCopiee".
        /// </summary>
        public void CopierLignesFacture()
        {
            rowsFactureCopiee.Clear();
            int premierIndex = dgv_Facture.SelectedRows[(dgv_Facture.SelectedRows.Count - 1)].Index;
            int dernierIndex = dgv_Facture.SelectedRows[0].Index;
            DataGridViewRow nouvelleRow = new DataGridViewRow();
            nouvelleRow = (DataGridViewRow)dgv_Facture.Rows[0].Clone();
            foreach (DataGridViewRow row in dgv_Facture.SelectedRows)
            {
                nouvelleRow = new DataGridViewRow();
                nouvelleRow = (DataGridViewRow)dgv_Facture.Rows[0].Clone();
                for (int e = 1; e < 5; e++)
                {
                    nouvelleRow.Cells[e].Value = dgv_Facture.Rows[row.Index].Cells[e].Value;
                }
                rowsFactureCopiee.Insert(0, nouvelleRow);
            }
            txt_Recherche.Text = "";
            btn_CollerLigne.Enabled = true;
        }

        /// <summary>
        ///  Permet de coller les lignes copiées dans les lignes actuellement séléctionnées.
        /// </summary>
        public void CollerLignesFacture()
        {
            int indexPremierRowSelected = dgv_Facture.SelectedRows[(dgv_Facture.SelectedRows.Count - 1)].Index;
            int indexDernierRowSelected = dgv_Facture.SelectedRows[0].Index;
            txt_Recherche.Text = "";
            List<DataGridViewRow> rowsAremplacer = new List<DataGridViewRow>();
            int e = 0;
            if (((indexDernierRowSelected - indexPremierRowSelected) + 1) == rowsFactureCopiee.Count || dgv_Facture.SelectedRows.Count == 1)
            {
                for (int i = (dgv_Facture.SelectedRows.Count - 1); i > -1; i--)
                {
                    for (int k = 1; k < 5; k++)
                    {
                        dgv_Facture.Rows[dgv_Facture.SelectedRows[i].Index].Cells[k].Value = rowsFactureCopiee[e].Cells[k].Value;
                    }
                    e++;
                }
            }
            else
            {
                for (int i = (dgv_Facture.SelectedRows.Count - 1); i > -1; i--)
                {
                    if (dgv_Facture.SelectedRows[i].Index <= (dgv_Facture.Rows.Count - 1) && e < rowsFactureCopiee.Count)
                    {
                        for (int k = 1; k < 5; k++)
                        {
                            dgv_Facture.Rows[dgv_Facture.SelectedRows[i].Index].Cells[k].Value = rowsFactureCopiee[e].Cells[k].Value;
                        }
                        e++;
                    }
                }
            }
            formGestionRef.ActualiserIndexLignesFacture();
            if (couperLignes)
            {
                couperLignes = false;
                btn_CollerLigne.Enabled = false;
                rowsFactureCopiee.Clear();
            }
        }

        /// <summary>
        ///  Permet de "couper" donc copier puis vider les rows copiées, on peut coller une fois après la coupe.
        /// </summary>
        public void CouperLignesFacture()
        {
            CopierLignesFacture();
            for (int i = (dgv_Facture.SelectedRows.Count - 1); i > -1; i--)
            {
                for (int e = 1; e < 5; e++)
                {
                    dgv_Facture.Rows[dgv_Facture.SelectedRows[i].Index].Cells[e].Value = "";
                }
            }
            couperLignes = true;
        }

        /// <summary>
        ///  Permet de "clear" les rows séléctionnées, de les vider.
        /// </summary>
        public void ViderLignesFacture()
        {
            for (int i = (dgv_Facture.SelectedRows.Count - 1); i > -1; i--)
            {
                for (int e = 1; e < 5; e++)
                {
                    dgv_Facture.Rows[dgv_Facture.SelectedRows[i].Index].Cells[e].Value = "";
                }
            }
        }



        public string AssemblerPrestationNomPourcentage(string nomSepare, float pourcentageSepare)
        {
            return nomSepare + " (" + pourcentageSepare * 100 + "%)";
        }

        public (string, float) SeparerPrestationNomPourcentage(string stringOriginal)
        {
            string nomSepare = stringOriginal;
            string strPourcentageSepare;
            float pourcentageSepare;
            nomSepare = nomSepare.Substring(0, nomSepare.IndexOf('(') - 1);
            strPourcentageSepare = stringOriginal.Substring(stringOriginal.IndexOf('(') + 1, (stringOriginal.IndexOf(')') - stringOriginal.IndexOf('(')) - 2);
            pourcentageSepare = float.Parse(strPourcentageSepare) / 100;
            return (nomSepare, pourcentageSepare);
        }


        public void AccederSiteWebSelected(string url)
        {
            try
            {
                ProcessStartInfo psInfo = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psInfo);
            }
            catch (Exception exc)
            {
                MessageBox.Show("L'url de ce site web ne semble pas fonctionner", "Erreur URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool DateComprisDansPeriode(DateTime dateAverif, DateTime debutPeriode, DateTime finPeriode)
        {
            return (dateAverif >= debutPeriode) && (dateAverif <= finPeriode);
        }

        public DateTime ModifierDateDepuisPeriode(DateTime date, EnumPeriodes periode, bool ajouterPeriode = false)
        {
            int multiplicateur = -1;
            if (ajouterPeriode)
            {
                multiplicateur = 1;
            }
            switch (periode)
            {
                case EnumPeriodes.SEMAINE:
                    return date.AddDays(7 * multiplicateur);
                case EnumPeriodes.MOIS:
                    return date.AddMonths(1 * multiplicateur);
                case EnumPeriodes.ANNEE:
                    return date.AddYears(1 * multiplicateur);
                default:
                    return DateTime.MinValue;
            }
        }

        public DateTime PremierJourPeriode(DateTime date, EnumPeriodes periode)
        {
            string dateStr;
            switch (periode)
            {
                case EnumPeriodes.SEMAINE:
                    while(date.DayOfWeek.ToString() != "Monday")
                    {
                        date = date.AddDays(-1);
                    }
                    return date;
                case EnumPeriodes.MOIS:
                    dateStr = date.ToString();
                    dateStr = "01" + dateStr.Substring(2);
                    return DateTime.Parse(dateStr);
                case EnumPeriodes.ANNEE:
                    dateStr = date.ToString();
                    dateStr = "01/01" + dateStr.Substring(5);
                    return DateTime.Parse(dateStr);
                default:
                    return DateTime.MinValue;
            }
        }

        public DateTime DernierJourPeriode(DateTime date, EnumPeriodes periode)
        {
            string dateStr;
            switch (periode)
            {
                case EnumPeriodes.SEMAINE:
                    while (date.DayOfWeek.ToString() != "Friday")
                    {
                        date = date.AddDays(1);
                    }
                    return date;
                case EnumPeriodes.MOIS:
                    dateStr = date.ToString();
                    dateStr = DateTime.DaysInMonth(date.Year, date.Month) + dateStr.Substring(2);
                    return DateTime.Parse(dateStr);
                case EnumPeriodes.ANNEE:
                    dateStr = date.ToString();
                    dateStr = DateTime.DaysInMonth(date.Year, 12) + "/12" + dateStr.Substring(5);
                    return DateTime.Parse(dateStr);
                default:
                    return DateTime.MinValue;
            }
        }

        public DateTimeIntervalType ConversionPeriodeAdateTimeIntervalType(EnumPeriodes periode)
        {
            switch (periode)
            {
                case EnumPeriodes.SEMAINE:
                    return DateTimeIntervalType.Days;
                case EnumPeriodes.MOIS:
                    return DateTimeIntervalType.Days;
                case EnumPeriodes.ANNEE:
                    return DateTimeIntervalType.Months;
                default:
                    return DateTimeIntervalType.Days;
            }
        }
    }
}
