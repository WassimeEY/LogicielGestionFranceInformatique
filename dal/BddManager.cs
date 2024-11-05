using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using FranceInformatiqueInventaire.Model;

namespace FranceInformatiqueInventaire.dal
{
    /// <summary>
    ///  Classe utilisée comme pont pour communiquer avec les bases de données (fichier .db) en utilisant l'extension SQLite.
    /// </summary>
    public class BddManager
    {
        private static BddManager? instance;
        private string connectionTexte = "";
        private FormGestion mainFormRef;

        /// <summary>
        ///  Constructeur de la classe, on récupére et définit ici la référence avec la FormGestion.
        /// </summary>
        private BddManager(FormGestion mainForm)
        {
            mainFormRef = mainForm;
        }

        /// <summary>
        ///  Permet de récupérer l'unique instance du singleton BddManager.
        /// </summary>
        /// <param name="mainForm">Référence donnée nécessaire pour le constructeur de la classe.</param>
        /// <returns>L'unique instance du singleton.</returns>

        public static BddManager GetInstance(FormGestion mainForm)
        {
            if (instance == null)
            {
                instance = new BddManager(mainForm);
            }
            return instance;
        }

        /// <summary>
        ///  Permet de lire le fichier .db directement, puis de retourner une liste des instances de l'entité InventaireLigne.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>Une liste des instances de l'entité InventaireLigne récupérées grace au curseur SQL.</returns>
        public List<InventaireLigne> RecupererInventaireTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<InventaireLigne> inventaireLignes = new List<InventaireLigne>();
            int id;
            string type;
            string marque;
            string nom;
            string annee;
            float prix;
            string dateEntree;
            string dateSortie;
            string commentaire;
            using (SQLiteConnection connection = new SQLiteConnection(connectionTexte))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Inventaire", connection))
                {
                    object r = command.ExecuteScalar();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id"));
                            type = reader.GetString(reader.GetOrdinal("Type"));
                            marque = reader.GetString(reader.GetOrdinal("Marque"));
                            nom = reader.GetString(reader.GetOrdinal("Nom"));
                            annee = reader.GetString(reader.GetOrdinal("Annee"));
                            prix = reader.IsDBNull(reader.GetOrdinal("Prix")) ? -1f : reader.GetFloat(reader.GetOrdinal("Prix"));
                            dateEntree = reader.GetString(reader.GetOrdinal("DateEntree"));
                            dateSortie = reader.GetString(reader.GetOrdinal("DateSortie"));
                            commentaire = reader.GetString(reader.GetOrdinal("Commentaire"));
                            InventaireLigne nouvelleLigne = new InventaireLigne(id, type, marque, nom, annee, prix, dateEntree, dateSortie, commentaire);
                            inventaireLignes.Add(nouvelleLigne);
                        }
                    }
                }
                return inventaireLignes;
            }
        }

        /// <summary>
        ///  Permet de lire le fichier .db directement, puis de retourner une liste des instances de l'entité InventaireMarque.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>La liste des instances de type InventaireMarque récupéré à partir du curseur SQL.</returns>
        public List<InventaireMarque> RecupererMarqueTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<InventaireMarque> marqueLignes = new List<InventaireMarque>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionTexte))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Marque", connection))
                {
                    object r = command.ExecuteScalar();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string marqueNom = reader.GetString(reader.GetOrdinal("Nom"));
                            marqueLignes.Add(new InventaireMarque(marqueNom));
                        }
                    }
                }
            }
            return marqueLignes;
        }

        /// <summary>
        ///  Permet de lire le fichier .db directement, puis de retourner une liste des instances de l'entité InventaireType.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>La liste des instances de type InventaireType récupéré à partir du curseur SQL.</returns>
        public List<InventaireType> RecupererTypeTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<InventaireType> typeLignes = new List<InventaireType>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionTexte))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Type", connection))
                {
                    object r = command.ExecuteScalar();
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string typeNom = reader.GetString(reader.GetOrdinal("Nom"));
                            typeLignes.Add(new InventaireType(typeNom));
                        }
                    }
                }
            }
            return typeLignes;
        }

        /// <summary>
        ///  Permet de créer et d'écrire un nouveau fichier .db qui aura toutes les données de l'inventaire, des marques et des types et autre.
        /// </summary>
        /// <param name="chemin">Chemin où la création et écriture du fichier sera faite.</param>
        public void EcrireBdd(string chemin)
        {
            List<InventaireLigne> inventaireActuelle = mainFormRef.RecupererInventaireActuelle();
            List<InventaireMarque> lesMarquesActuelle = mainFormRef.RecupererMarquesActuelle();
            List<InventaireType> lesTypesActuelle = mainFormRef.RecupererTypesActuelle();
            string connectionTexte;
            SQLiteConnection.CreateFile(chemin);
            connectionTexte = @"Data Source=" + chemin + ";Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionTexte))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE [Inventaire] ([Id] bigint NOT NULL, [Type] text,[Marque] text, [Nom] text, [Annee] text, [Prix] real, [DateEntree] text, [DateSortie] text, [Commentaire] text, CONSTRAINT [sqlite_master_PK_Inventaire] PRIMARY KEY ([Id]));", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                        using (SQLiteCommand command2 = new SQLiteCommand("INSERT INTO Inventaire (Id, Type, Marque, Nom, Annee, Prix, DateEntree, DateSortie, Commentaire) VALUES (@Id, @Type, @Marque, @Nom, @Annee, @Prix, @DateEntree, @DateSortie, @Commentaire)", connection, transaction))
                        {
                            for (int i = 0; i < inventaireActuelle.Count; i++)
                            {
                                InventaireLigne inventaireLigne = inventaireActuelle[i];
                                command2.Parameters.AddWithValue("@Id", inventaireLigne.id);
                                command2.Parameters.AddWithValue("@Type", inventaireLigne.type);
                                command2.Parameters.AddWithValue("@Marque", inventaireLigne.marque);
                                command2.Parameters.AddWithValue("@Nom", inventaireLigne.nom);
                                command2.Parameters.AddWithValue("@Annee", inventaireLigne.annee);
                                if (inventaireLigne.prix != -1)
                                {
                                    command2.Parameters.AddWithValue("@Prix", inventaireLigne.prix);
                                }
                                else
                                {
                                    command2.Parameters.AddWithValue("@Prix", DBNull.Value);
                                }
                                command2.Parameters.AddWithValue("@DateEntree", inventaireLigne.dateEntree);
                                command2.Parameters.AddWithValue("@DateSortie", inventaireLigne.dateSortie);
                                command2.Parameters.AddWithValue("@Commentaire", inventaireLigne.commentaire);
                                command2.ExecuteNonQuery();
                            }
                        }
                    }

                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE [Marque] ([Id] bigint NOT NULL, [Nom] text,CONSTRAINT [sqlite_master_PK_Inventaire] PRIMARY KEY ([Id]));", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                        using (SQLiteCommand command2 = new SQLiteCommand("INSERT INTO Marque (Id, Nom) VALUES (@Id, @Nom)", connection, transaction))
                        {
                            for (int i = 0; i < lesMarquesActuelle.Count; i++)
                            {
                                command2.Parameters.AddWithValue("@Id", i);
                                command2.Parameters.AddWithValue("@Nom", lesMarquesActuelle[i].nom);
                                command2.ExecuteNonQuery();
                            }
                            
                        }
                    }

                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE [Type] ([Id] bigint NOT NULL, [Nom] text,CONSTRAINT [sqlite_master_PK_Inventaire] PRIMARY KEY ([Id]));", connection, transaction))
                    {
                        command.ExecuteNonQuery();
                        using (SQLiteCommand command2 = new SQLiteCommand("INSERT INTO Type (Id, Nom) VALUES (@Id, @Nom)", connection, transaction))
                        {
                            for (int i = 0; i < lesTypesActuelle.Count; i++)
                            {
                                command2.Parameters.AddWithValue("@Id", i);
                                command2.Parameters.AddWithValue("@Nom", lesTypesActuelle[i].nom);
                                command2.ExecuteNonQuery();
                            }

                        }
                    }
                    transaction.Commit();

                }
            }
        }
    }
}
