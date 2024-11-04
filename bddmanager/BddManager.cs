using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace FranceInformatiqueInventaire.bddmanager
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
        ///  Permet de lire le fichier .db directement, puis de retourner un ValueTuple qui contient elle même des listes pour chaque colonne.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>Le ValueTuple qui contient chaque colonne.</returns>
        public (List<int>, List<string>, List<string>, List<string>, List<string>, List<float>, List<string>, List<string>, List<string>) RecupererInventaireTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<int> listeId = new List<int>();
            List<string> listeType = new List<string>();
            List<string> listeMarque = new List<string>();
            List<string> listeNom = new List<string>();
            List<string> listeAnnee = new List<string>();
            List<float> listePrix = new List<float>();
            List<string> listeDateEntree = new List<string>();
            List<string> listeDateSortie = new List<string>();
            List<string> listeCommentaire = new List<string>();
            
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
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string type = reader.GetString(reader.GetOrdinal("Type"));
                            string marque = reader.GetString(reader.GetOrdinal("Marque"));
                            string nom = reader.GetString(reader.GetOrdinal("Nom"));
                            string annee = reader.GetString(reader.GetOrdinal("Annee"));
                            float prix = reader.IsDBNull(reader.GetOrdinal("Prix")) ? -1f : reader.GetFloat(reader.GetOrdinal("Prix"));
                            string dateEntree = reader.GetString(reader.GetOrdinal("DateEntree"));
                            string dateSortie = reader.GetString(reader.GetOrdinal("DateSortie"));
                            string commentaire = reader.GetString(reader.GetOrdinal("Commentaire"));

                            listeId.Add(id);
                            listeType.Add(type);
                            listeMarque.Add(marque);
                            listeNom.Add(nom);
                            listeAnnee.Add(annee);
                            listePrix.Add(prix);
                            listeDateEntree.Add(dateEntree);
                            listeDateSortie.Add(dateSortie);
                            listeCommentaire.Add(commentaire);
                        }
                    }


                }
                return (listeId, listeType, listeMarque, listeNom, listeAnnee, listePrix, listeDateEntree, listeDateSortie, listeCommentaire);
            }

            
        }

        /// <summary>
        ///  Permet de lire le fichier .db directement, puis de retourner une liste correspondant aux marques.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>La liste correspondant aux marques.</returns>
        public List<string> RecupererMarqueTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<string> lesMarques = new List<string>();
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
                            string uneMarque = reader.GetString(reader.GetOrdinal("Nom"));
                            lesMarques.Add(uneMarque);
                        }
                    }
                }
            }
            return (lesMarques);
        }

        /// <summary>
        ///  Permet de lire le fichier .db directement, puis de retourner une liste correspondant aux types.
        /// </summary>
        /// <param name="connectionTexteTemp">Le texte qui permet la connexion temporaire avec le fichier .db .</param>
        /// <returns>La liste correspondant aux types.</returns>
        public List<string> RecupererTypeTable(string connectionTexteTemp)
        {
            connectionTexte = connectionTexteTemp;
            List<string> lesTypes = new List<string>();
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
                            string unType = reader.GetString(reader.GetOrdinal("Nom"));
                            lesTypes.Add(unType);
                        }
                    }
                }
            }
            return (lesTypes);
        }

        /// <summary>
        ///  Permet de créer et d'écrire un nouveau fichier .db qui aura toutes les données de l'inventaire, des marques et des types.
        /// </summary>
        /// <param name="chemin">Chemin où la création et écriture du fichier sera faite.</param>
        public void EcrireBdd(string chemin)
        {
            var listes = mainFormRef.RecupererListesActuelleDgvInventaire();
            List<string> lesMarquesActuelle = mainFormRef.RecupererMarquesActuelle();
            List<string> lesTypesActuelle = mainFormRef.RecupererTypesActuelle();
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
                            for (int i = 0; i < listes.Item1.Count; i++)
                            {
                                command2.Parameters.AddWithValue("@Id", listes.Item1[i]);
                                command2.Parameters.AddWithValue("@Type", listes.Item2[i]);
                                command2.Parameters.AddWithValue("@Marque", listes.Item3[i]);
                                command2.Parameters.AddWithValue("@Nom", listes.Item4[i]);
                                command2.Parameters.AddWithValue("@Annee", listes.Item5[i]);
                                if (listes.Item6[i] != -1)
                                {
                                    command2.Parameters.AddWithValue("@Prix", listes.Item6[i]);
                                }
                                else
                                {
                                    command2.Parameters.AddWithValue("@Prix", DBNull.Value);
                                }

                                command2.Parameters.AddWithValue("@DateEntree", listes.Item7[i]);
                                command2.Parameters.AddWithValue("@DateSortie", listes.Item8[i]);
                                command2.Parameters.AddWithValue("@Commentaire", listes.Item9[i]);
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
                                command2.Parameters.AddWithValue("@Nom", lesMarquesActuelle[i]);
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
                                command2.Parameters.AddWithValue("@Nom", lesTypesActuelle[i]);
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
