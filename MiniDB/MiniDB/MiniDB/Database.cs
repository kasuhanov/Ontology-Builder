using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniDB {
    using SelectingFunc = Func<Record, bool>;
    using RecordsList = List<Record>;

    /// <summary>
    /// Менеджер локальной файловой базы данных
    /// </summary>
    public class Database {
        #region STATIC
        #region PRIVATE
        static char pathSeparate = '\\';
        const string TableFileExtention = ".tbl";
        static string GetDirPath (string filePath) {
            if (filePath == null)
                return null;
            if (filePath.Length == 0)
                return filePath;
            string res = filePath.Replace( '/', pathSeparate );
            try {
                while (res[res.Length - 1] != pathSeparate) {
                    res.Remove( res.Length-1, 1 );
                    if (res.Length == 0)
                        break;
                }
            } catch {
                res = null;
            }
            return res;
        }
        #endregion
        public static Database Create (string directory = "") {
            string dir;
            if (directory == null) {
                dir = "";
            } else {
                dir = directory;
            }
            bool flag = true;
            if (Directory.Exists( dir ) == false) {
                try {
                    Directory.CreateDirectory( dir );
                    flag = true;
                } catch {
                    flag = false;
                }
            }
            if (flag) {
                return new Database( dir );
            } else {
                return null;
            }
        }
        public static Database OpenLocal () {
            return Open( "" );
        }
        public static Database Open (string directory = "") {
            if (directory == null)
                directory = "";
            try {
                directory = Path.GetFullPath( directory );
            } catch {
                return null;
            }
            if (Directory.Exists( directory )) {
                List<string> list = new List<string>();
                string[] files = Directory.GetFiles( directory );
                foreach (string file in files) {
                    if (Path.GetExtension( file ) == TableFileExtention) {
                        list.Add( Path.GetFileNameWithoutExtension( file ) );
                    }
                }
                Database db = new Database( directory );
                db.workingDirectory = directory;
                db.tableList = list.ToArray();
                return db;
            } else {
                return Database.Create( directory );
            }
        } //Open
        #endregion

        #region PRIVATE
        string workingDirectory = "";
        string[] tableList = null;
        #region functions
        Database (string directory){
            try {
                workingDirectory = directory;
            } catch { }
        }
        Record[] GetRecordsFromString (string src) {
            if (src == null)
                return null;
            if (src.Length == 0)
                return null;
            try {
                string[] records = src.Split( Record.RecordSeparator ); //get records array
                Record[] result = new Record[records.Length - (records[records.Length-1] == "" ? 1 : 0)];
                for (int i = 0; i < result.Length; i++) {
                    string[] fields_plus_GID = records[i].Split( Record.FieldSeparator );
                    string[] fields = new string[fields_plus_GID.Length - 1 - (fields_plus_GID[fields_plus_GID.Length-1] == "" ? 1 : 0)];
                    for (int k = 0; k < fields.Length; k++) {
                        fields[k] = fields_plus_GID[k+1];
                    }
                    long GID = 0;
                    try {
                        GID = Convert.ToInt64( fields_plus_GID[0] );
                    } catch { }
                    Record rec = new Record( GID, fields );
                    result[i] = (rec); //get fields from each record
                }
                return result;
            } catch {
                return null;
            }
        }
        string GetStringFromRecords (Record[] src) {
            if (src == null)
                return "";
            string text = "";
            foreach (Record rec in src) {
                text += rec.GID + (Record.FieldSeparator + "") + rec.ToString();
            }
            return text;
        }
        string GetFileName (string Table) {
            if (workingDirectory[workingDirectory.Length - 1] != '\\')
                workingDirectory += "\\";
            return workingDirectory + Table + TableFileExtention;
        }
        #endregion
        #endregion

        #region PUBLIC
        public Record[] GetAllRecords (string TableName) {
            try {
                string file = File.ReadAllText( GetFileName( TableName ) );
                return GetRecordsFromString( file );
            } catch (Exception ex) {
                throw ex;
            }
        }
        public Record[] GetRecordsWhere (string TableName, SelectingFunc SelectingFunc, bool InvertedLogic = false) {
            try {
                RecordsList list = new RecordsList();
                Record[] temp = GetAllRecords( TableName );
                foreach(Record rec in temp){
                    if (SelectingFunc( rec ) == !InvertedLogic) {
                        list.Add( rec );
                    }
                }
                return list.ToArray();
            } catch (Exception ex) {
                throw ex;
            }
        }
        public void AppendRecords (string TableName, Record[] records) {
            try {
                File.AppendAllText( GetFileName( TableName ), GetStringFromRecords( records ) );
            } catch (Exception ex) {
                throw ex;
            }
        }
        public void RewriteTableWithRecords (string TableName, Record[] records) {
            try {
                File.WriteAllText( GetFileName( TableName ), GetStringFromRecords( records ) );
            } catch (Exception ex) {
                throw ex;
            }
        }
        public void RemoveRecord (string TableName, SelectingFunc SelectingFunc) {
            try {
                RewriteTableWithRecords( TableName, GetRecordsWhere( TableName, SelectingFunc, true ) );
            } catch (Exception ex) {
                throw ex;
            }
        }
        #endregion
    } //Database

    /// <summary>
    /// Запись с полями в строковом формате
    /// </summary>
    public class Record{
        protected long gID = 0;
        protected static long maxGID = 0;
        /// <summary>
        /// Глобальный служебный идентификатор записей.
        /// </summary>
        public long GID { get { return gID; } }
        //Управляющие символы https://ru.wikipedia.org/wiki/%D0%A3%D0%BF%D1%80%D0%B0%D0%B2%D0%BB%D1%8F%D1%8E%D1%89%D0%B8%D0%B5_%D1%81%D0%B8%D0%BC%D0%B2%D0%BE%D0%BB%D1%8B
        public const char RecordSeparator = (char)30;
        public const char FieldSeparator = (char)31;
        public string[] Fields;
        public Record (string[] fields) {
            this.gID = maxGID++;
            this.Fields = fields;
        }
        public Record (long GID, string[] fields) {
            if (GID > maxGID)
                maxGID = GID + 1;
            this.gID = GID;
            this.Fields = fields;
        }
        public override string ToString () {
            string result = "";
            try {
                if (Fields != null) {
                    for (int i = 0; i < Fields.Length; i++) {
                        result += Fields[i] + FieldSeparator;//(i != Fields.Length-1 ? "" + FieldSeparator : "");
                    }
                }
            } catch { }
            result += RecordSeparator;
            return result;
        }
    }
}
