namespace FranceInformatiqueInventaire
{
    partial class FormGestion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestion));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            menuStrip1 = new MenuStrip();
            testToolStripMenuItem = new ToolStripMenuItem();
            TSMenuItem_Fichier_Nouveau = new ToolStripMenuItem();
            TSMenuItem_Fichier_Ouvrir = new ToolStripMenuItem();
            TSMenuItem_Fichier_Sauvegarder = new ToolStripMenuItem();
            TSMenuItem_Fichier_SauvegarderSous = new ToolStripMenuItem();
            préférencesToolStripMenuItem = new ToolStripMenuItem();
            TSMenuItem_Preferences_InsertionLigne = new ToolStripMenuItem();
            TSMenuItem_Preferences_InsertionLigne_Cb = new ToolStripComboBox();
            TSMenuItem_Preferences_ConfirmationSuppression = new ToolStripMenuItem();
            TSMenuItem_Preferences_ConfirmationVider = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            TSMenuItem_Preferences_Theme = new ToolStripMenuItem();
            TSMenuItem_Preferences_Theme_CouleurP = new ToolStripMenuItem();
            TSMenuItem_Preferences_Theme_CouleurS = new ToolStripMenuItem();
            TSMenuItem_Preferences_Theme_CouleurT = new ToolStripMenuItem();
            lbl_RechercheInventaire = new Label();
            txt_RechercheInventaire = new TextBox();
            tlp_Bas = new TableLayoutPanel();
            tlp_Haut = new TableLayoutPanel();
            cb_FiltreRecherche = new ComboBox();
            lbl_Filtre = new Label();
            ts_Inventaire = new ToolStrip();
            btn_AjouterLigneInventaire = new ToolStripButton();
            btn_SupprimerLigneInventaire = new ToolStripButton();
            btn_ViderLigneInventaire = new ToolStripButton();
            btn_InsererLigneInventaire = new ToolStripButton();
            btn_CopierLigneInventaire = new ToolStripButton();
            btn_CouperLigneInventaire = new ToolStripButton();
            btn_CollerLigneInventaire = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btn_Sauvegarder = new ToolStripButton();
            btn_SauvegarderSous = new ToolStripButton();
            tlp_Main = new TableLayoutPanel();
            tlp_Millieu = new TableLayoutPanel();
            tabControl_Onglets = new TabControl();
            OngletInventaire = new TabPage();
            tabControl_Inventaire = new TabControl();
            tab_Inventaire = new TabPage();
            dgv_Inventaire = new DataGridView();
            Index = new DataGridViewTextBoxColumn();
            Type = new DataGridViewComboBoxColumn();
            Marque = new DataGridViewComboBoxColumn();
            Nom = new DataGridViewTextBoxColumn();
            Annee = new DataGridViewTextBoxColumn();
            Prix = new DataGridViewTextBoxColumn();
            DateEntree = new DataGridViewTextBoxColumn();
            DateSortie = new DataGridViewTextBoxColumn();
            Commentaire = new DataGridViewTextBoxColumn();
            tab_Marque = new TabPage();
            tlp_MarqueMain = new TableLayoutPanel();
            lb_Marque = new ListBox();
            tlp_GererMarque = new TableLayoutPanel();
            txt_AjoutMarque = new TextBox();
            btn_SupprimerMarque = new Button();
            btn_AjouterMarque = new Button();
            tab_Type = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            lb_Type = new ListBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            txt_AjoutType = new TextBox();
            btn_SupprimerType = new Button();
            btn_AjouterType = new Button();
            OngletFactureNette = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            listBox_Factures = new ListBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            txt_FactureTotalHT = new TextBox();
            btn_SupprFacture = new Button();
            btn_ModifFacture = new Button();
            btn_AjoutFacture = new Button();
            txt_FactureAjout = new TextBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            colorDialog1 = new ColorDialog();
            menuStrip1.SuspendLayout();
            tlp_Haut.SuspendLayout();
            ts_Inventaire.SuspendLayout();
            tlp_Main.SuspendLayout();
            tlp_Millieu.SuspendLayout();
            tabControl_Onglets.SuspendLayout();
            OngletInventaire.SuspendLayout();
            tabControl_Inventaire.SuspendLayout();
            tab_Inventaire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Inventaire).BeginInit();
            tab_Marque.SuspendLayout();
            tlp_MarqueMain.SuspendLayout();
            tlp_GererMarque.SuspendLayout();
            tab_Type.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            OngletFactureNette.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(73, 82, 97);
            menuStrip1.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem, préférencesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Size = new Size(1057, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { TSMenuItem_Fichier_Nouveau, TSMenuItem_Fichier_Ouvrir, TSMenuItem_Fichier_Sauvegarder, TSMenuItem_Fichier_SauvegarderSous });
            testToolStripMenuItem.ForeColor = SystemColors.Control;
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(54, 20);
            testToolStripMenuItem.Text = "Fichier";
            // 
            // TSMenuItem_Fichier_Nouveau
            // 
            TSMenuItem_Fichier_Nouveau.Name = "TSMenuItem_Fichier_Nouveau";
            TSMenuItem_Fichier_Nouveau.Size = new Size(166, 22);
            TSMenuItem_Fichier_Nouveau.Text = "Nouveau";
            TSMenuItem_Fichier_Nouveau.Click += TSMenuItem_Fichier_Nouveau_Click;
            // 
            // TSMenuItem_Fichier_Ouvrir
            // 
            TSMenuItem_Fichier_Ouvrir.Name = "TSMenuItem_Fichier_Ouvrir";
            TSMenuItem_Fichier_Ouvrir.Size = new Size(166, 22);
            TSMenuItem_Fichier_Ouvrir.Text = "Ouvrir";
            TSMenuItem_Fichier_Ouvrir.Click += TSMenuItem_Fichier_Ouvrir_Click;
            // 
            // TSMenuItem_Fichier_Sauvegarder
            // 
            TSMenuItem_Fichier_Sauvegarder.Enabled = false;
            TSMenuItem_Fichier_Sauvegarder.Name = "TSMenuItem_Fichier_Sauvegarder";
            TSMenuItem_Fichier_Sauvegarder.Size = new Size(166, 22);
            TSMenuItem_Fichier_Sauvegarder.Text = "Sauvegarder";
            TSMenuItem_Fichier_Sauvegarder.Click += TSMenuItem_Fichier_Sauvegarder_Click;
            // 
            // TSMenuItem_Fichier_SauvegarderSous
            // 
            TSMenuItem_Fichier_SauvegarderSous.Name = "TSMenuItem_Fichier_SauvegarderSous";
            TSMenuItem_Fichier_SauvegarderSous.Size = new Size(166, 22);
            TSMenuItem_Fichier_SauvegarderSous.Text = "Sauvegarder sous";
            TSMenuItem_Fichier_SauvegarderSous.Click += TSMenuItem_Fichier_SauvegarderSous_Click;
            // 
            // préférencesToolStripMenuItem
            // 
            préférencesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { TSMenuItem_Preferences_InsertionLigne, TSMenuItem_Preferences_ConfirmationSuppression, TSMenuItem_Preferences_ConfirmationVider, toolStripSeparator1, TSMenuItem_Preferences_Theme });
            préférencesToolStripMenuItem.ForeColor = SystemColors.Control;
            préférencesToolStripMenuItem.Name = "préférencesToolStripMenuItem";
            préférencesToolStripMenuItem.Size = new Size(80, 20);
            préférencesToolStripMenuItem.Text = "Préférences";
            // 
            // TSMenuItem_Preferences_InsertionLigne
            // 
            TSMenuItem_Preferences_InsertionLigne.DropDownItems.AddRange(new ToolStripItem[] { TSMenuItem_Preferences_InsertionLigne_Cb });
            TSMenuItem_Preferences_InsertionLigne.Name = "TSMenuItem_Preferences_InsertionLigne";
            TSMenuItem_Preferences_InsertionLigne.Size = new Size(288, 22);
            TSMenuItem_Preferences_InsertionLigne.Text = "Insertion de ligne";
            // 
            // TSMenuItem_Preferences_InsertionLigne_Cb
            // 
            TSMenuItem_Preferences_InsertionLigne_Cb.DropDownStyle = ComboBoxStyle.DropDownList;
            TSMenuItem_Preferences_InsertionLigne_Cb.FlatStyle = FlatStyle.Standard;
            TSMenuItem_Preferences_InsertionLigne_Cb.Items.AddRange(new object[] { "Avant la ligne", "Après la ligne" });
            TSMenuItem_Preferences_InsertionLigne_Cb.Name = "TSMenuItem_Preferences_InsertionLigne_Cb";
            TSMenuItem_Preferences_InsertionLigne_Cb.Size = new Size(121, 23);
            TSMenuItem_Preferences_InsertionLigne_Cb.SelectedIndexChanged += TSMenuItem_Preferences_InsertionLigne_Cb_SelectedIndexChanged;
            // 
            // TSMenuItem_Preferences_ConfirmationSuppression
            // 
            TSMenuItem_Preferences_ConfirmationSuppression.Checked = true;
            TSMenuItem_Preferences_ConfirmationSuppression.CheckOnClick = true;
            TSMenuItem_Preferences_ConfirmationSuppression.CheckState = CheckState.Checked;
            TSMenuItem_Preferences_ConfirmationSuppression.Name = "TSMenuItem_Preferences_ConfirmationSuppression";
            TSMenuItem_Preferences_ConfirmationSuppression.Size = new Size(288, 22);
            TSMenuItem_Preferences_ConfirmationSuppression.Text = "Confirmation avant suppression de ligne";
            TSMenuItem_Preferences_ConfirmationSuppression.CheckedChanged += rToolStripMenuItem_CheckedChanged;
            // 
            // TSMenuItem_Preferences_ConfirmationVider
            // 
            TSMenuItem_Preferences_ConfirmationVider.Checked = true;
            TSMenuItem_Preferences_ConfirmationVider.CheckOnClick = true;
            TSMenuItem_Preferences_ConfirmationVider.CheckState = CheckState.Checked;
            TSMenuItem_Preferences_ConfirmationVider.Name = "TSMenuItem_Preferences_ConfirmationVider";
            TSMenuItem_Preferences_ConfirmationVider.Size = new Size(288, 22);
            TSMenuItem_Preferences_ConfirmationVider.Text = "Confirmation avant vider ligne";
            TSMenuItem_Preferences_ConfirmationVider.CheckedChanged += TSMenuItem_Preferences_ConfirmationVider_CheckedChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(285, 6);
            // 
            // TSMenuItem_Preferences_Theme
            // 
            TSMenuItem_Preferences_Theme.AccessibleDescription = "Permet de choisir les couleurs du logiciel";
            TSMenuItem_Preferences_Theme.DropDownItems.AddRange(new ToolStripItem[] { TSMenuItem_Preferences_Theme_CouleurP, TSMenuItem_Preferences_Theme_CouleurS, TSMenuItem_Preferences_Theme_CouleurT });
            TSMenuItem_Preferences_Theme.Name = "TSMenuItem_Preferences_Theme";
            TSMenuItem_Preferences_Theme.Size = new Size(288, 22);
            TSMenuItem_Preferences_Theme.Text = "Thème";
            // 
            // TSMenuItem_Preferences_Theme_CouleurP
            // 
            TSMenuItem_Preferences_Theme_CouleurP.BackColor = SystemColors.Control;
            TSMenuItem_Preferences_Theme_CouleurP.ImageTransparentColor = Color.Transparent;
            TSMenuItem_Preferences_Theme_CouleurP.Name = "TSMenuItem_Preferences_Theme_CouleurP";
            TSMenuItem_Preferences_Theme_CouleurP.Size = new Size(176, 22);
            TSMenuItem_Preferences_Theme_CouleurP.Text = "Couleur primaire";
            TSMenuItem_Preferences_Theme_CouleurP.Click += TSMenuItem_Preferences_Theme_CouleurP_Click;
            // 
            // TSMenuItem_Preferences_Theme_CouleurS
            // 
            TSMenuItem_Preferences_Theme_CouleurS.Name = "TSMenuItem_Preferences_Theme_CouleurS";
            TSMenuItem_Preferences_Theme_CouleurS.Size = new Size(176, 22);
            TSMenuItem_Preferences_Theme_CouleurS.Text = "Couleur secondaire";
            TSMenuItem_Preferences_Theme_CouleurS.Click += TSMenuItem_Preferences_Theme_CouleurS_Click;
            // 
            // TSMenuItem_Preferences_Theme_CouleurT
            // 
            TSMenuItem_Preferences_Theme_CouleurT.Name = "TSMenuItem_Preferences_Theme_CouleurT";
            TSMenuItem_Preferences_Theme_CouleurT.Size = new Size(176, 22);
            TSMenuItem_Preferences_Theme_CouleurT.Text = "Couleur tertiaire";
            TSMenuItem_Preferences_Theme_CouleurT.Click += TSMenuItem_Preferences_Theme_CouleurT_Click;
            // 
            // lbl_RechercheInventaire
            // 
            lbl_RechercheInventaire.AutoSize = true;
            lbl_RechercheInventaire.Dock = DockStyle.Fill;
            lbl_RechercheInventaire.ForeColor = SystemColors.Control;
            lbl_RechercheInventaire.Location = new Point(537, 0);
            lbl_RechercheInventaire.Name = "lbl_RechercheInventaire";
            lbl_RechercheInventaire.Padding = new Padding(3);
            lbl_RechercheInventaire.Size = new Size(76, 29);
            lbl_RechercheInventaire.TabIndex = 7;
            lbl_RechercheInventaire.Text = "Recherche";
            lbl_RechercheInventaire.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_RechercheInventaire
            // 
            txt_RechercheInventaire.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txt_RechercheInventaire.BackColor = SystemColors.Control;
            txt_RechercheInventaire.ForeColor = SystemColors.WindowText;
            txt_RechercheInventaire.Location = new Point(619, 3);
            txt_RechercheInventaire.Name = "txt_RechercheInventaire";
            txt_RechercheInventaire.PlaceholderText = "Rechercher dans l'inventaire";
            txt_RechercheInventaire.Size = new Size(195, 23);
            txt_RechercheInventaire.TabIndex = 6;
            txt_RechercheInventaire.TextChanged += txt_RechercheInventaire_TextChanged;
            // 
            // tlp_Bas
            // 
            tlp_Bas.ColumnCount = 2;
            tlp_Bas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Bas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Bas.Dock = DockStyle.Fill;
            tlp_Bas.Location = new Point(3, 340);
            tlp_Bas.Name = "tlp_Bas";
            tlp_Bas.RowCount = 1;
            tlp_Bas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Bas.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_Bas.Size = new Size(1044, 1);
            tlp_Bas.TabIndex = 9;
            // 
            // tlp_Haut
            // 
            tlp_Haut.ColumnCount = 5;
            tlp_Haut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.22438F));
            tlp_Haut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.93651F));
            tlp_Haut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.26961F));
            tlp_Haut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.535149F));
            tlp_Haut.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.03434F));
            tlp_Haut.Controls.Add(txt_RechercheInventaire, 2, 0);
            tlp_Haut.Controls.Add(lbl_RechercheInventaire, 1, 0);
            tlp_Haut.Controls.Add(cb_FiltreRecherche, 4, 0);
            tlp_Haut.Controls.Add(lbl_Filtre, 3, 0);
            tlp_Haut.Controls.Add(ts_Inventaire, 0, 0);
            tlp_Haut.Dock = DockStyle.Fill;
            tlp_Haut.Location = new Point(3, 3);
            tlp_Haut.Name = "tlp_Haut";
            tlp_Haut.RowCount = 1;
            tlp_Haut.RowStyles.Add(new RowStyle());
            tlp_Haut.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_Haut.Size = new Size(1044, 29);
            tlp_Haut.TabIndex = 9;
            // 
            // cb_FiltreRecherche
            // 
            cb_FiltreRecherche.Dock = DockStyle.Fill;
            cb_FiltreRecherche.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_FiltreRecherche.FormattingEnabled = true;
            cb_FiltreRecherche.Items.AddRange(new object[] { "Pas de filtre" });
            cb_FiltreRecherche.Location = new Point(867, 3);
            cb_FiltreRecherche.Name = "cb_FiltreRecherche";
            cb_FiltreRecherche.Size = new Size(174, 23);
            cb_FiltreRecherche.TabIndex = 8;
            cb_FiltreRecherche.SelectedIndexChanged += cb_FiltreRecherche_SelectedIndexChanged;
            // 
            // lbl_Filtre
            // 
            lbl_Filtre.AutoSize = true;
            lbl_Filtre.Dock = DockStyle.Fill;
            lbl_Filtre.ForeColor = SystemColors.Control;
            lbl_Filtre.Location = new Point(820, 0);
            lbl_Filtre.Name = "lbl_Filtre";
            lbl_Filtre.Padding = new Padding(3);
            lbl_Filtre.Size = new Size(41, 29);
            lbl_Filtre.TabIndex = 9;
            lbl_Filtre.Text = "Filtre";
            lbl_Filtre.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ts_Inventaire
            // 
            ts_Inventaire.BackColor = Color.FromArgb(73, 82, 97);
            ts_Inventaire.Dock = DockStyle.Fill;
            ts_Inventaire.Items.AddRange(new ToolStripItem[] { btn_AjouterLigneInventaire, btn_SupprimerLigneInventaire, btn_ViderLigneInventaire, btn_InsererLigneInventaire, btn_CopierLigneInventaire, btn_CouperLigneInventaire, btn_CollerLigneInventaire, toolStripSeparator2, btn_Sauvegarder, btn_SauvegarderSous });
            ts_Inventaire.Location = new Point(0, 0);
            ts_Inventaire.Name = "ts_Inventaire";
            ts_Inventaire.Size = new Size(534, 29);
            ts_Inventaire.TabIndex = 10;
            ts_Inventaire.Text = "toolStrip1";
            // 
            // btn_AjouterLigneInventaire
            // 
            btn_AjouterLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_AjouterLigneInventaire.Image = (Image)resources.GetObject("btn_AjouterLigneInventaire.Image");
            btn_AjouterLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_AjouterLigneInventaire.Name = "btn_AjouterLigneInventaire";
            btn_AjouterLigneInventaire.Size = new Size(23, 26);
            btn_AjouterLigneInventaire.Text = "Ajouter une ligne";
            btn_AjouterLigneInventaire.Click += btn_AjouterLigneInventaire_Click_1;
            // 
            // btn_SupprimerLigneInventaire
            // 
            btn_SupprimerLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_SupprimerLigneInventaire.Enabled = false;
            btn_SupprimerLigneInventaire.Image = Properties.Resources.Delete;
            btn_SupprimerLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_SupprimerLigneInventaire.Name = "btn_SupprimerLigneInventaire";
            btn_SupprimerLigneInventaire.Size = new Size(23, 26);
            btn_SupprimerLigneInventaire.Text = "Supprimer";
            btn_SupprimerLigneInventaire.Click += btn_SupprimerLigneInventaire_Click_1;
            // 
            // btn_ViderLigneInventaire
            // 
            btn_ViderLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_ViderLigneInventaire.Enabled = false;
            btn_ViderLigneInventaire.Image = Properties.Resources.Clear;
            btn_ViderLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_ViderLigneInventaire.Name = "btn_ViderLigneInventaire";
            btn_ViderLigneInventaire.Size = new Size(23, 26);
            btn_ViderLigneInventaire.Text = "Vider";
            btn_ViderLigneInventaire.Click += btn_ViderLigneInventaire_Click;
            // 
            // btn_InsererLigneInventaire
            // 
            btn_InsererLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_InsererLigneInventaire.Enabled = false;
            btn_InsererLigneInventaire.Image = Properties.Resources.Insert;
            btn_InsererLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_InsererLigneInventaire.Name = "btn_InsererLigneInventaire";
            btn_InsererLigneInventaire.Size = new Size(23, 26);
            btn_InsererLigneInventaire.Text = "Insérer une ligne";
            btn_InsererLigneInventaire.Click += btn_InsererLigneInventaire_Click;
            // 
            // btn_CopierLigneInventaire
            // 
            btn_CopierLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_CopierLigneInventaire.Enabled = false;
            btn_CopierLigneInventaire.Image = Properties.Resources.Copy1;
            btn_CopierLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_CopierLigneInventaire.Name = "btn_CopierLigneInventaire";
            btn_CopierLigneInventaire.Size = new Size(23, 26);
            btn_CopierLigneInventaire.Text = "Copier";
            btn_CopierLigneInventaire.Click += btn_CopierLigneInventaire_Click;
            // 
            // btn_CouperLigneInventaire
            // 
            btn_CouperLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_CouperLigneInventaire.Enabled = false;
            btn_CouperLigneInventaire.Image = Properties.Resources.Cut1;
            btn_CouperLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_CouperLigneInventaire.Name = "btn_CouperLigneInventaire";
            btn_CouperLigneInventaire.Size = new Size(23, 26);
            btn_CouperLigneInventaire.Text = "Couper";
            btn_CouperLigneInventaire.Click += btn_CouperLigneInventaire_Click;
            // 
            // btn_CollerLigneInventaire
            // 
            btn_CollerLigneInventaire.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_CollerLigneInventaire.Enabled = false;
            btn_CollerLigneInventaire.Image = Properties.Resources.Paste;
            btn_CollerLigneInventaire.ImageTransparentColor = Color.Magenta;
            btn_CollerLigneInventaire.Name = "btn_CollerLigneInventaire";
            btn_CollerLigneInventaire.Size = new Size(23, 26);
            btn_CollerLigneInventaire.Text = "Coller";
            btn_CollerLigneInventaire.Click += btn_CollerLigneInventaire_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 29);
            // 
            // btn_Sauvegarder
            // 
            btn_Sauvegarder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_Sauvegarder.Enabled = false;
            btn_Sauvegarder.Image = Properties.Resources.Sauvegarder;
            btn_Sauvegarder.ImageTransparentColor = Color.Magenta;
            btn_Sauvegarder.Name = "btn_Sauvegarder";
            btn_Sauvegarder.Size = new Size(23, 26);
            btn_Sauvegarder.Text = "Sauvegarder";
            btn_Sauvegarder.Click += btn_Sauvegarder_Click;
            // 
            // btn_SauvegarderSous
            // 
            btn_SauvegarderSous.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_SauvegarderSous.Image = Properties.Resources.SauvegarderSous;
            btn_SauvegarderSous.ImageTransparentColor = Color.Magenta;
            btn_SauvegarderSous.Name = "btn_SauvegarderSous";
            btn_SauvegarderSous.Size = new Size(23, 26);
            btn_SauvegarderSous.Text = "Sauvegarder sous";
            btn_SauvegarderSous.Click += btn_SauvegarderSous_Click;
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 1;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlp_Main.Controls.Add(tlp_Bas, 0, 2);
            tlp_Main.Controls.Add(tlp_Millieu, 0, 1);
            tlp_Main.Controls.Add(tlp_Haut, 0, 0);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(4, 3);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 3;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 98.97683F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 1.023173F));
            tlp_Main.Size = new Size(1050, 341);
            tlp_Main.TabIndex = 10;
            // 
            // tlp_Millieu
            // 
            tlp_Millieu.ColumnCount = 1;
            tlp_Millieu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Millieu.Controls.Add(tabControl_Onglets, 0, 0);
            tlp_Millieu.Dock = DockStyle.Fill;
            tlp_Millieu.Location = new Point(3, 38);
            tlp_Millieu.Name = "tlp_Millieu";
            tlp_Millieu.RowCount = 1;
            tlp_Millieu.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Millieu.Size = new Size(1044, 296);
            tlp_Millieu.TabIndex = 10;
            // 
            // tabControl_Onglets
            // 
            tabControl_Onglets.Controls.Add(OngletInventaire);
            tabControl_Onglets.Controls.Add(OngletFactureNette);
            tabControl_Onglets.Dock = DockStyle.Fill;
            tabControl_Onglets.Location = new Point(3, 3);
            tabControl_Onglets.Name = "tabControl_Onglets";
            tabControl_Onglets.SelectedIndex = 0;
            tabControl_Onglets.Size = new Size(1038, 290);
            tabControl_Onglets.TabIndex = 12;
            // 
            // OngletInventaire
            // 
            OngletInventaire.BackColor = Color.DimGray;
            OngletInventaire.Controls.Add(tabControl_Inventaire);
            OngletInventaire.Location = new Point(4, 24);
            OngletInventaire.Name = "OngletInventaire";
            OngletInventaire.Padding = new Padding(3);
            OngletInventaire.Size = new Size(1030, 262);
            OngletInventaire.TabIndex = 0;
            OngletInventaire.Text = "Inventaire";
            // 
            // tabControl_Inventaire
            // 
            tabControl_Inventaire.Controls.Add(tab_Inventaire);
            tabControl_Inventaire.Controls.Add(tab_Marque);
            tabControl_Inventaire.Controls.Add(tab_Type);
            tabControl_Inventaire.Dock = DockStyle.Fill;
            tabControl_Inventaire.ImeMode = ImeMode.NoControl;
            tabControl_Inventaire.ItemSize = new Size(100, 20);
            tabControl_Inventaire.Location = new Point(3, 3);
            tabControl_Inventaire.Name = "tabControl_Inventaire";
            tabControl_Inventaire.SelectedIndex = 0;
            tabControl_Inventaire.Size = new Size(1024, 256);
            tabControl_Inventaire.SizeMode = TabSizeMode.Fixed;
            tabControl_Inventaire.TabIndex = 4;
            tabControl_Inventaire.Selecting += tabControl_Inventaire_Selecting;
            // 
            // tab_Inventaire
            // 
            tab_Inventaire.BackColor = Color.FromArgb(48, 50, 54);
            tab_Inventaire.Controls.Add(dgv_Inventaire);
            tab_Inventaire.ForeColor = Color.FromArgb(48, 50, 54);
            tab_Inventaire.Location = new Point(4, 24);
            tab_Inventaire.Name = "tab_Inventaire";
            tab_Inventaire.Padding = new Padding(3);
            tab_Inventaire.Size = new Size(1016, 228);
            tab_Inventaire.TabIndex = 0;
            tab_Inventaire.Text = "Inventaire";
            // 
            // dgv_Inventaire
            // 
            dgv_Inventaire.AllowUserToAddRows = false;
            dgv_Inventaire.AllowUserToDeleteRows = false;
            dgv_Inventaire.BackgroundColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_Inventaire.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv_Inventaire.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Inventaire.Columns.AddRange(new DataGridViewColumn[] { Index, Type, Marque, Nom, Annee, Prix, DateEntree, DateSortie, Commentaire });
            dgv_Inventaire.Dock = DockStyle.Fill;
            dgv_Inventaire.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv_Inventaire.GridColor = Color.FromArgb(64, 64, 64);
            dgv_Inventaire.Location = new Point(3, 3);
            dgv_Inventaire.Name = "dgv_Inventaire";
            dgv_Inventaire.RowHeadersWidth = 20;
            dgv_Inventaire.RowTemplate.Height = 25;
            dgv_Inventaire.Size = new Size(1010, 222);
            dgv_Inventaire.TabIndex = 0;
            dgv_Inventaire.CellClick += dgv_Inventaire_CellClick;
            dgv_Inventaire.CellDoubleClick += dgv_Inventaire_CellDoubleClick;
            dgv_Inventaire.CellValueChanged += dgv_Inventaire_CellValueChanged;
            dgv_Inventaire.RowsAdded += dgv_Inventaire_RowsAdded;
            dgv_Inventaire.Scroll += dgv_Inventaire_Scroll;
            dgv_Inventaire.SelectionChanged += dgv_Inventaire_SelectionChanged;
            dgv_Inventaire.KeyDown += dgv_Inventaire_KeyDown;
            // 
            // Index
            // 
            Index.HeaderText = " ID";
            Index.Name = "Index";
            Index.ReadOnly = true;
            Index.Width = 40;
            // 
            // Type
            // 
            Type.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Type.HeaderText = "Type";
            Type.Items.AddRange(new object[] { "Périphérique", "Processeur", "Pâte thermique" });
            Type.MaxDropDownItems = 100;
            Type.Name = "Type";
            Type.Resizable = DataGridViewTriState.True;
            Type.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Marque
            // 
            Marque.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Marque.HeaderText = "Marque";
            Marque.Items.AddRange(new object[] { "Asus", "Logitech", "Nividia", "Intel", "Acer", "Lenovo" });
            Marque.MaxDropDownItems = 100;
            Marque.Name = "Marque";
            Marque.Resizable = DataGridViewTriState.True;
            Marque.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Nom
            // 
            Nom.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nom.HeaderText = "Nom";
            Nom.Name = "Nom";
            // 
            // Annee
            // 
            Annee.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Annee.HeaderText = "Année";
            Annee.Name = "Annee";
            // 
            // Prix
            // 
            Prix.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Prix.HeaderText = "Prix";
            Prix.Name = "Prix";
            // 
            // DateEntree
            // 
            DateEntree.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DateEntree.HeaderText = "Date d'entrée";
            DateEntree.Name = "DateEntree";
            // 
            // DateSortie
            // 
            DateSortie.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DateSortie.HeaderText = "Date de sortie";
            DateSortie.Name = "DateSortie";
            // 
            // Commentaire
            // 
            Commentaire.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Commentaire.HeaderText = "Commentaire";
            Commentaire.Name = "Commentaire";
            // 
            // tab_Marque
            // 
            tab_Marque.BackColor = Color.FromArgb(48, 50, 54);
            tab_Marque.Controls.Add(tlp_MarqueMain);
            tab_Marque.ForeColor = Color.FromArgb(48, 50, 54);
            tab_Marque.Location = new Point(4, 24);
            tab_Marque.Name = "tab_Marque";
            tab_Marque.Padding = new Padding(3);
            tab_Marque.Size = new Size(1016, 228);
            tab_Marque.TabIndex = 1;
            tab_Marque.Text = "Marque";
            // 
            // tlp_MarqueMain
            // 
            tlp_MarqueMain.ColumnCount = 2;
            tlp_MarqueMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_MarqueMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_MarqueMain.Controls.Add(lb_Marque, 0, 0);
            tlp_MarqueMain.Controls.Add(tlp_GererMarque, 1, 0);
            tlp_MarqueMain.Dock = DockStyle.Fill;
            tlp_MarqueMain.Location = new Point(3, 3);
            tlp_MarqueMain.Name = "tlp_MarqueMain";
            tlp_MarqueMain.RowCount = 1;
            tlp_MarqueMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_MarqueMain.Size = new Size(1010, 222);
            tlp_MarqueMain.TabIndex = 5;
            // 
            // lb_Marque
            // 
            lb_Marque.Dock = DockStyle.Fill;
            lb_Marque.FormattingEnabled = true;
            lb_Marque.ItemHeight = 15;
            lb_Marque.Items.AddRange(new object[] { "Asus", "Logitech", "Nividia", "MSI", "Acer", "Lenovo" });
            lb_Marque.Location = new Point(3, 3);
            lb_Marque.Name = "lb_Marque";
            lb_Marque.Size = new Size(499, 216);
            lb_Marque.TabIndex = 1;
            lb_Marque.SelectedIndexChanged += lb_Marque_SelectedIndexChanged;
            lb_Marque.KeyDown += listBox1_KeyDown;
            // 
            // tlp_GererMarque
            // 
            tlp_GererMarque.ColumnCount = 1;
            tlp_GererMarque.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlp_GererMarque.Controls.Add(txt_AjoutMarque, 0, 0);
            tlp_GererMarque.Controls.Add(btn_SupprimerMarque, 0, 2);
            tlp_GererMarque.Controls.Add(btn_AjouterMarque, 0, 1);
            tlp_GererMarque.Dock = DockStyle.Fill;
            tlp_GererMarque.Location = new Point(508, 3);
            tlp_GererMarque.Name = "tlp_GererMarque";
            tlp_GererMarque.RowCount = 4;
            tlp_GererMarque.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlp_GererMarque.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlp_GererMarque.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlp_GererMarque.RowStyles.Add(new RowStyle());
            tlp_GererMarque.Size = new Size(499, 216);
            tlp_GererMarque.TabIndex = 2;
            // 
            // txt_AjoutMarque
            // 
            txt_AjoutMarque.Dock = DockStyle.Fill;
            txt_AjoutMarque.Location = new Point(3, 3);
            txt_AjoutMarque.Name = "txt_AjoutMarque";
            txt_AjoutMarque.PlaceholderText = "Entrer la marque à ajouter";
            txt_AjoutMarque.Size = new Size(493, 23);
            txt_AjoutMarque.TabIndex = 3;
            txt_AjoutMarque.TextChanged += txt_AjoutMarque_TextChanged;
            // 
            // btn_SupprimerMarque
            // 
            btn_SupprimerMarque.Dock = DockStyle.Fill;
            btn_SupprimerMarque.Enabled = false;
            btn_SupprimerMarque.Location = new Point(3, 83);
            btn_SupprimerMarque.Name = "btn_SupprimerMarque";
            btn_SupprimerMarque.Size = new Size(493, 44);
            btn_SupprimerMarque.TabIndex = 6;
            btn_SupprimerMarque.Text = "Supprimer";
            btn_SupprimerMarque.UseVisualStyleBackColor = true;
            btn_SupprimerMarque.Click += btn_SupprimerMarque_Click;
            // 
            // btn_AjouterMarque
            // 
            btn_AjouterMarque.Dock = DockStyle.Fill;
            btn_AjouterMarque.Enabled = false;
            btn_AjouterMarque.Location = new Point(3, 33);
            btn_AjouterMarque.Name = "btn_AjouterMarque";
            btn_AjouterMarque.Size = new Size(493, 44);
            btn_AjouterMarque.TabIndex = 2;
            btn_AjouterMarque.Text = "Ajouter";
            btn_AjouterMarque.UseVisualStyleBackColor = true;
            btn_AjouterMarque.Click += btn_AjouterMarque_Click;
            // 
            // tab_Type
            // 
            tab_Type.BackColor = Color.FromArgb(48, 50, 54);
            tab_Type.Controls.Add(tableLayoutPanel1);
            tab_Type.ForeColor = Color.FromArgb(48, 50, 54);
            tab_Type.Location = new Point(4, 24);
            tab_Type.Name = "tab_Type";
            tab_Type.Padding = new Padding(3);
            tab_Type.Size = new Size(1016, 228);
            tab_Type.TabIndex = 2;
            tab_Type.Text = "Type";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lb_Type, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1010, 222);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // lb_Type
            // 
            lb_Type.Dock = DockStyle.Fill;
            lb_Type.FormattingEnabled = true;
            lb_Type.ItemHeight = 15;
            lb_Type.Items.AddRange(new object[] { "Périphérique", "Processeur", "Pâte thermique" });
            lb_Type.Location = new Point(3, 3);
            lb_Type.Name = "lb_Type";
            lb_Type.Size = new Size(499, 216);
            lb_Type.TabIndex = 1;
            lb_Type.SelectedIndexChanged += lb_Type_SelectedIndexChanged;
            lb_Type.KeyDown += lb_Type_KeyDown;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(txt_AjoutType, 0, 0);
            tableLayoutPanel2.Controls.Add(btn_SupprimerType, 0, 2);
            tableLayoutPanel2.Controls.Add(btn_AjouterType, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(508, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(499, 216);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // txt_AjoutType
            // 
            txt_AjoutType.Dock = DockStyle.Fill;
            txt_AjoutType.Location = new Point(3, 3);
            txt_AjoutType.Name = "txt_AjoutType";
            txt_AjoutType.PlaceholderText = "Entrer la type à ajouter";
            txt_AjoutType.Size = new Size(493, 23);
            txt_AjoutType.TabIndex = 3;
            txt_AjoutType.TextChanged += txt_TypeAjouter_TextChanged;
            // 
            // btn_SupprimerType
            // 
            btn_SupprimerType.Dock = DockStyle.Fill;
            btn_SupprimerType.Enabled = false;
            btn_SupprimerType.Location = new Point(3, 83);
            btn_SupprimerType.Name = "btn_SupprimerType";
            btn_SupprimerType.Size = new Size(493, 44);
            btn_SupprimerType.TabIndex = 6;
            btn_SupprimerType.Text = "Supprimer";
            btn_SupprimerType.UseVisualStyleBackColor = true;
            btn_SupprimerType.Click += btn_SupprimerType_Click;
            // 
            // btn_AjouterType
            // 
            btn_AjouterType.Dock = DockStyle.Fill;
            btn_AjouterType.Enabled = false;
            btn_AjouterType.Location = new Point(3, 33);
            btn_AjouterType.Name = "btn_AjouterType";
            btn_AjouterType.Size = new Size(493, 44);
            btn_AjouterType.TabIndex = 2;
            btn_AjouterType.Text = "Ajouter";
            btn_AjouterType.UseVisualStyleBackColor = true;
            btn_AjouterType.Click += btn_AjouterType_Click;
            // 
            // OngletFactureNette
            // 
            OngletFactureNette.BackColor = Color.FromArgb(48, 50, 54);
            OngletFactureNette.Controls.Add(tableLayoutPanel4);
            OngletFactureNette.Location = new Point(4, 24);
            OngletFactureNette.Name = "OngletFactureNette";
            OngletFactureNette.Padding = new Padding(3);
            OngletFactureNette.Size = new Size(1030, 262);
            OngletFactureNette.TabIndex = 1;
            OngletFactureNette.Text = "Facture nette de charges";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(listBox_Factures, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.ForeColor = Color.FromArgb(48, 50, 54);
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(1024, 256);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // listBox_Factures
            // 
            listBox_Factures.Dock = DockStyle.Fill;
            listBox_Factures.FormattingEnabled = true;
            listBox_Factures.ItemHeight = 15;
            listBox_Factures.Items.AddRange(new object[] { "F2400044" });
            listBox_Factures.Location = new Point(3, 3);
            listBox_Factures.Name = "listBox_Factures";
            listBox_Factures.Size = new Size(506, 250);
            listBox_Factures.TabIndex = 1;
            listBox_Factures.SelectedIndexChanged += listBox_Factures_SelectedIndexChanged;
            listBox_Factures.KeyDown += listBox_Factures_KeyDown;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.FromArgb(48, 50, 54);
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(txt_FactureTotalHT, 0, 1);
            tableLayoutPanel5.Controls.Add(btn_SupprFacture, 0, 5);
            tableLayoutPanel5.Controls.Add(btn_ModifFacture, 0, 4);
            tableLayoutPanel5.Controls.Add(btn_AjoutFacture, 0, 3);
            tableLayoutPanel5.Controls.Add(txt_FactureAjout, 0, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 2);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(515, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 7;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.Size = new Size(506, 250);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // txt_FactureTotalHT
            // 
            txt_FactureTotalHT.Dock = DockStyle.Fill;
            txt_FactureTotalHT.Location = new Point(3, 33);
            txt_FactureTotalHT.Name = "txt_FactureTotalHT";
            txt_FactureTotalHT.PlaceholderText = "Entrer le total HT";
            txt_FactureTotalHT.Size = new Size(500, 23);
            txt_FactureTotalHT.TabIndex = 12;
            // 
            // btn_SupprFacture
            // 
            btn_SupprFacture.Dock = DockStyle.Fill;
            btn_SupprFacture.Enabled = false;
            btn_SupprFacture.ForeColor = Color.FromArgb(48, 50, 54);
            btn_SupprFacture.Location = new Point(3, 193);
            btn_SupprFacture.Name = "btn_SupprFacture";
            btn_SupprFacture.Size = new Size(500, 44);
            btn_SupprFacture.TabIndex = 11;
            btn_SupprFacture.Text = "Supprimer";
            btn_SupprFacture.UseVisualStyleBackColor = true;
            btn_SupprFacture.Click += btn_SupprFacture_Click;
            // 
            // btn_ModifFacture
            // 
            btn_ModifFacture.Dock = DockStyle.Fill;
            btn_ModifFacture.Enabled = false;
            btn_ModifFacture.Location = new Point(3, 143);
            btn_ModifFacture.Name = "btn_ModifFacture";
            btn_ModifFacture.Size = new Size(500, 44);
            btn_ModifFacture.TabIndex = 10;
            btn_ModifFacture.Text = "Modifier";
            btn_ModifFacture.UseVisualStyleBackColor = true;
            // 
            // btn_AjoutFacture
            // 
            btn_AjoutFacture.Dock = DockStyle.Fill;
            btn_AjoutFacture.Enabled = false;
            btn_AjoutFacture.Location = new Point(3, 93);
            btn_AjoutFacture.Name = "btn_AjoutFacture";
            btn_AjoutFacture.Size = new Size(500, 44);
            btn_AjoutFacture.TabIndex = 2;
            btn_AjoutFacture.Text = "Ajouter";
            btn_AjoutFacture.UseVisualStyleBackColor = true;
            btn_AjoutFacture.Click += btn_AjoutFacture_Click;
            // 
            // txt_FactureAjout
            // 
            txt_FactureAjout.Dock = DockStyle.Fill;
            txt_FactureAjout.Location = new Point(3, 3);
            txt_FactureAjout.Name = "txt_FactureAjout";
            txt_FactureAjout.PlaceholderText = "Entrer le nom de la facture à ajouter";
            txt_FactureAjout.Size = new Size(500, 23);
            txt_FactureAjout.TabIndex = 3;
            txt_FactureAjout.TextChanged += txt_FactureAjout_TextChanged;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel6.Controls.Add(comboBox1, 1, 0);
            tableLayoutPanel6.Controls.Add(label1, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 63);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(500, 24);
            tableLayoutPanel6.TabIndex = 13;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Vente", "Reparation" });
            comboBox1.Location = new Point(153, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(344, 23);
            comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(3, 4);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 1;
            label1.Text = "Type de prestation";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.1010378F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99.89896F));
            tableLayoutPanel3.Controls.Add(tlp_Main, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 24);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1057, 347);
            tableLayoutPanel3.TabIndex = 11;
            // 
            // FormGestion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(48, 50, 54);
            ClientSize = new Size(1057, 371);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.AppWorkspace;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FormGestion";
            Text = "Logiciel de gestion FRANCEINFORMATIQUE.FR";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tlp_Haut.ResumeLayout(false);
            tlp_Haut.PerformLayout();
            ts_Inventaire.ResumeLayout(false);
            ts_Inventaire.PerformLayout();
            tlp_Main.ResumeLayout(false);
            tlp_Millieu.ResumeLayout(false);
            tabControl_Onglets.ResumeLayout(false);
            OngletInventaire.ResumeLayout(false);
            tabControl_Inventaire.ResumeLayout(false);
            tab_Inventaire.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Inventaire).EndInit();
            tab_Marque.ResumeLayout(false);
            tlp_MarqueMain.ResumeLayout(false);
            tlp_GererMarque.ResumeLayout(false);
            tlp_GererMarque.PerformLayout();
            tab_Type.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            OngletFactureNette.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem TSMenuItem_Fichier_Ouvrir;
        private ToolStripMenuItem TSMenuItem_Fichier_Sauvegarder;
        private ToolStripMenuItem préférencesToolStripMenuItem;
        private ToolStripMenuItem TSMenuItem_Preferences_ConfirmationVider;
        private ToolStripMenuItem TSMenuItem_Preferences_ConfirmationSuppression;
        private TableLayoutPanel tlp_Bas;
        private TextBox txt_RechercheInventaire;
        private Label lbl_RechercheInventaire;
        private TableLayoutPanel tlp_Haut;
        private TableLayoutPanel tlp_Main;
        private TableLayoutPanel tlp_Millieu;
        private ToolStripMenuItem TSMenuItem_Fichier_SauvegarderSous;
        private ToolStripMenuItem TSMenuItem_Fichier_Nouveau;
        private ComboBox cb_FiltreRecherche;
        private Label lbl_Filtre;
        private ToolStrip ts_Inventaire;
        private ToolStripButton btn_AjouterLigneInventaire;
        private ToolStripButton btn_SupprimerLigneInventaire;
        private ToolStripButton btn_InsererLigneInventaire;
        private ToolStripButton btn_CopierLigneInventaire;
        private ToolStripButton btn_CollerLigneInventaire;
        private TableLayoutPanel tableLayoutPanel3;
        private TabControl tabControl_Inventaire;
        private TabPage tab_Inventaire;
        private DataGridView dgv_Inventaire;
        private DataGridViewTextBoxColumn Index;
        private DataGridViewComboBoxColumn Type;
        private DataGridViewComboBoxColumn Marque;
        private DataGridViewTextBoxColumn Nom;
        private DataGridViewTextBoxColumn Annee;
        private DataGridViewTextBoxColumn Prix;
        private DataGridViewTextBoxColumn DateEntree;
        private DataGridViewTextBoxColumn DateSortie;
        private DataGridViewTextBoxColumn Commentaire;
        private TabPage tab_Marque;
        private TableLayoutPanel tlp_MarqueMain;
        private ListBox lb_Marque;
        private TableLayoutPanel tlp_GererMarque;
        private TextBox txt_AjoutMarque;
        private Button btn_SupprimerMarque;
        private Button btn_AjouterMarque;
        private TabPage tab_Type;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox lb_Type;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txt_AjoutType;
        private Button btn_SupprimerType;
        private Button btn_AjouterType;
        private ToolStripMenuItem TSMenuItem_Preferences_InsertionLigne;
        private ToolStripComboBox TSMenuItem_Preferences_InsertionLigne_Cb;
        private ToolStripMenuItem TSMenuItem_Preferences_Theme;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem TSMenuItem_Preferences_Theme_CouleurP;
        private ToolStripMenuItem TSMenuItem_Preferences_Theme_CouleurS;
        private ToolStripMenuItem TSMenuItem_Preferences_Theme_CouleurT;
        private ColorDialog colorDialog1;
        private ToolStripButton btn_CouperLigneInventaire;
        private ToolStripButton btn_ViderLigneInventaire;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btn_Sauvegarder;
        private ToolStripButton btn_SauvegarderSous;
        private TabControl tabControl_Onglets;
        private TabPage OngletInventaire;
        private TabPage OngletFactureNette;
        private TableLayoutPanel tableLayoutPanel4;
        private ListBox listBox_Factures;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txt_FactureAjout;
        private Button btn_AjoutFacture;
        private Button btn_ModifFacture;
        private Button btn_SupprFacture;
        private TextBox txt_FactureTotalHT;
        private TableLayoutPanel tableLayoutPanel6;
        private ComboBox comboBox1;
        private Label label1;
    }
}