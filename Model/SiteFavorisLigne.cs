using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceInformatiqueInventaire.Model
{
    /// <summary>
    /// Classe du modèle, représentant un site favoris sous forme de nom et d'URL.
    /// </summary>
    public class SiteFavorisLigne
    {
        /// <summary>
        /// Le nom du site web, par exemple "Google".
        /// </summary>
        public string nom;
        /// <summary>
        /// L'url du site web.
        /// </summary>
        public string url;

        /// <summary>
        /// Constructeur de l'entité siteFavorisLigne.
        /// </summary>
        public SiteFavorisLigne(string nom, string url)
        {
            this.nom = nom;
            this.url = url;
        }

        /// <summary>
        /// Override du ToString() pour retourner le nom du site web.
        /// </summary>
        public override string ToString()
        {
            return nom;
        }
    }
}
