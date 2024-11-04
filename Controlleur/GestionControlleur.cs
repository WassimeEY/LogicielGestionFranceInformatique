﻿using FranceInformatiqueInventaire.bddmanager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Controlleur
{
    /// <summary>
    ///  Classe utilisée comme controlleur de l'application.
    /// </summary>
    public partial class GestionControlleur
    {
        private FormGestion formGestionRef;
        private DataGridView dgv_Inventaire;
        private TextBox txt_RechercheInventaire;
        private ToolStripButton btn_CollerLigneInventaire;
        BddManager bddManagerRef;
        ToolStripMenuItem TSMenuItem_Fichier_Sauvegarder;
        private List<DataGridViewRow> inventaireRowsCharge;
        private List<DataGridViewRow> rowsCopiee = new List<DataGridViewRow>();
        private ComboBox cb_FiltreRecherche;
        private ListBox lb_Marque;
        private ListBox lb_Type;
        private List<string> marquesCharge = new List<string>();
        private List<string> typesCharge = new List<string>();
        private bool couperLignes;

        public GestionControlleur(FormGestion formGestionRef, DataGridView dgv_Inventaire, TextBox txt_RechercheInventaire, ToolStripButton btn_CollerLigneInventaire, BddManager bddManagerRef, ToolStripMenuItem TSMenuItem_Fichier_Sauvegarder, List<DataGridViewRow> inventaireRowsCharge, List<DataGridViewRow> rowsCopiee, ComboBox cb_FiltreRecherche, ListBox lb_Marque, ListBox lb_Type, List<string> marquesCharge, List<string> typesCharge, bool couperLignes)
        {
            this.formGestionRef = formGestionRef;
            this.dgv_Inventaire = dgv_Inventaire;
            this.txt_RechercheInventaire = txt_RechercheInventaire;
            this.btn_CollerLigneInventaire = btn_CollerLigneInventaire;
            this.bddManagerRef = bddManagerRef;
            this.TSMenuItem_Fichier_Sauvegarder = TSMenuItem_Fichier_Sauvegarder;
            this.inventaireRowsCharge = inventaireRowsCharge;
            this.rowsCopiee = rowsCopiee;
            this.cb_FiltreRecherche = cb_FiltreRecherche;
            this.lb_Marque = lb_Marque;
            this.lb_Type = lb_Type;
            this.marquesCharge = marquesCharge;
            this.typesCharge = typesCharge;
            this.couperLignes = couperLignes;
        }

        /// <summary>
        ///  Supprime des lignes de la dataGriwView inventaire en mettant à jour certaines choses par la même occasion.
        /// </summary>
        public void SupprimerLignesInventaire()
        {
            DataGridViewSelectedRowCollection savedSelectedRows;
            savedSelectedRows = dgv_Inventaire.SelectedRows;
            txt_RechercheInventaire.Text = "";
            foreach (DataGridViewRow ligne in savedSelectedRows)
            {
                dgv_Inventaire.Rows.Remove(ligne);
            }
            formGestionRef.ActualiserIndexLignes();
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
        ///  Permet d'insérer une ligne dans la dataGridView inventaire, insérer avant ou après la ligne séléctionné selon l'option choisi.
        /// </summary>
        public void InsererLigneInventaire(int indexRowSelected)
        {
            txt_RechercheInventaire.Text = "";
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
        ///  Permet de récupérer et copier les rows séléctionnées dans la liste de DataGridViewRow "rowsCopiee".
        /// </summary>
        public void CopierLignesInventaire()
        {
            rowsCopiee.Clear();
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
                rowsCopiee.Insert(0, nouvelleRow);
            }
            txt_RechercheInventaire.Text = "";
            btn_CollerLigneInventaire.Enabled = true;
        }

        /// <summary>
        ///  Permet de coller les lignes copiées dans les lignes actuellement séléctionnées.
        /// </summary>
        public void CollerLignesInventaire()
        {
            int indexPremierRowSelected = dgv_Inventaire.SelectedRows[(dgv_Inventaire.SelectedRows.Count - 1)].Index;
            int indexDernierRowSelected = dgv_Inventaire.SelectedRows[0].Index;
            txt_RechercheInventaire.Text = "";
            List<DataGridViewRow> rowsAremplacer = new List<DataGridViewRow>();
            int e = 0;
            if (((indexDernierRowSelected - indexPremierRowSelected) + 1) == rowsCopiee.Count || dgv_Inventaire.SelectedRows.Count == 1)
            {
                for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
                {
                    for (int k = 1; k < 9; k++)
                    {
                        dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[k].Value = rowsCopiee[e].Cells[k].Value;
                    }
                    e++;
                }
            }
            else
            {
                for (int i = (dgv_Inventaire.SelectedRows.Count - 1); i > -1; i--)
                {
                    if (dgv_Inventaire.SelectedRows[i].Index <= (dgv_Inventaire.Rows.Count - 1) && e < rowsCopiee.Count)
                    {
                        for (int k = 1; k < 9; k++)
                        {
                            dgv_Inventaire.Rows[dgv_Inventaire.SelectedRows[i].Index].Cells[k].Value = rowsCopiee[e].Cells[k].Value;
                        }
                        e++;
                    }
                }
            }
            formGestionRef.ActualiserIndexLignes();
            if (couperLignes)
            {
                couperLignes = false;
                btn_CollerLigneInventaire.Enabled = false;
                rowsCopiee.Clear();
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
    }
}