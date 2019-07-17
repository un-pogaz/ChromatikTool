﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Chromatik.Compression
{
    /// <summary>
    /// 
    /// </summary>
    static public class ChromatikZIP
    {
        /// <summary>
        /// Obtient un <see cref="ZipArchive"/>
        /// </summary>
        /// <param name="ZipPath"></param>
        /// <returns></returns>
        static public ZipArchive GetArchive(string ZipPath)
        {
            return GetArchive(ZipPath, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
        /// <summary>
        /// Obtient un <see cref="ZipArchive"/>
        /// </summary>
        /// <param name="ZipPath"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        static public ZipArchive GetArchive(string ZipPath, FileAccess access)
        {
            return GetArchive(ZipPath, access, FileShare.ReadWrite);
        }
        /// <summary>
        /// Obtient un <see cref="ZipArchive"/>
        /// </summary>
        /// <param name="ZipPath"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        static public ZipArchive GetArchive(string ZipPath, FileAccess access, FileShare share)
        {
            ZipArchiveMode ZipMode = ZipArchiveMode.Update;
            if (access == FileAccess.Read)
                ZipMode = ZipArchiveMode.Read;

            return new ZipArchive(new FileStream(ZipPath, FileMode.OpenOrCreate, access, share), ZipMode, false);
        }

        /// <summary>
        /// Supprime le dossier "__MACOSX" de l'archive ZIP
        /// </summary>
        /// <param name="ZipPath">ZIP cible</param>
        /// <returns>Retour true si le ZIP a était modifier; sinon false</returns>
        static public bool Delete__MACOSX(string ZipPath)
        {
            bool edited = false;
            using (ZipArchive archive = GetArchive(ZipPath, FileAccess.Read, FileShare.Read))
                foreach (var entry in archive.Entries)
                    if (entry.FullName.Contains("__MACOSX"))
                    {
                        edited = true;
                        break;
                    }

            if (edited)
                using (ZipArchive archive = GetArchive(ZipPath, FileAccess.ReadWrite, FileShare.Read))
                    edited = Delete__MACOSX(archive);

            return edited;
        }

        /// <summary>
        /// Supprime le dossier "__MACOSX" de l'archive ZIP
        /// </summary>
        /// <param name="zip">ZIP cible</param>
        /// <returns>Retour true si le ZIP a était modifier; sinon false</returns>
        static public bool Delete__MACOSX(ZipArchive zip)
        {
            bool edited = false;
            if (zip.Mode == ZipArchiveMode.Update)
            {
                List<ZipArchiveEntry> __MACOSX = new List<ZipArchiveEntry>();
                foreach (ZipArchiveEntry entry in zip.Entries)
                    if (entry.FullName.Contains("__MACOSX"))
                        __MACOSX.Add(entry);

                edited = (__MACOSX.Count > 0);
                foreach (ZipArchiveEntry entry in __MACOSX)
                    entry.Delete();
            }
            return edited;
        }
    }
}
