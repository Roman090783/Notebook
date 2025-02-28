﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class Notiz
    {
        /// <summary>
        /// Auflistung aller Notizen
        /// </summary>
        public static Dictionary<Guid, Notiz> Notizen = new Dictionary<Guid, Notiz>();

        /// <summary>
        /// Eindeutige ID einer Notiz
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// Kategorie einer Notiz
        /// </summary>
        public Kategorie Kategorie { get; private set; }

        /// <summary>
        /// Inhalt (Text) einer Notiz
        /// </summary>
        public string Inhalt { get; set; }

        /// <summary>
        /// Datum/Zeit der Erstellung einer Notiz
        /// </summary>
        public DateTime ErstelltAm { get; private set; }

        /// <summary>
        /// Legt eine neue Notiz an und fügt sie der Auflistung hinzu
        /// </summary>
        /// <param name="kat">Kategorie der neuen Notiz</param>
        /// <param name="text">Inhalt der neuen Notiz</param>
        public Notiz(Kategorie kat, string text)
        {
            this.Kategorie = kat;
            this.Inhalt = text;
            this.ID = Guid.NewGuid();
            this.ErstelltAm = DateTime.Now;
            Notizen.Add(this.ID, this);
        }

        /// <summary>
        /// Entfernt eine Notiz aus der Auflistung
        /// </summary>
        /// <param name="notiz">Die zu entfernende Notiz</param>
        /// <returns>true, wenn die Notiz erfolgreich entfernt werden konnte, sonst false</returns>
        public static bool Entfernen(Notiz notiz) => Notizen.Remove(notiz.ID);
    }

}
