using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDB {
    using SelectingFunc = Func<ObjectsLinkRecord, bool>;

    /// <summary>
    /// Таблица связей объектов. (id_parent & id_child)
    /// </summary>
    public class TableObjectsLinks {
        Database Source = null;
        const string TableName = "ObjectsLinks";
        long[] ChildIDs4Remove = null;
        long[] ParentIDs4Remove = null;

        TableObjectsLinks (Database source) {
            this.Source = source;
        }
        void RemoveByIDsAndFields (long[] IDs, int FieldIndex) {
            if (Source == null)
                return;
            try {
                Source.RemoveRecord( TableName, rec => {
                    if (IDs == null)
                        return false;
                    for (int i = 0; i < IDs.Length; i++) {
                        if (Convert.ToInt64( rec.Fields[FieldIndex] ) == IDs[i]) {
                            return true;
                        }
                    }
                    return false;
                } );
            } catch { }
            IDs = null;
        }

        /// <summary>
        /// Создает таблицу связей объектов.
        /// </summary>
        /// <param name="source">Ссылка на менеджер базы данных.</param>
        /// <returns>Возвращает ссылку на экземпляр класса таблицы.</returns>
        public static TableObjectsLinks Bind (Database source) {
            if (source == null)
                return null;
            return new TableObjectsLinks( source );
        }

        /// <summary>
        /// Загружает все записи из базы.
        /// </summary>
        /// <returns></returns>
        public ObjectsLinkRecord[] GetAllRecords () {
            if (Source == null) return null;
            try {
                Record[] records = Source.GetAllRecords( TableName );
                ObjectsLinkRecord[] result = new ObjectsLinkRecord[records.Length];
                for (int i = 0; i < result.Length; i++) {
                    try {
                        result[i] = new ObjectsLinkRecord( Convert.ToInt64( records[i].Fields[0] ), Convert.ToInt64( records[i].Fields[1] ) );
                    } catch { }
                } //for records
                return result;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// Удаляет все записи всех указанных родительских объектов.
        /// </summary>
        /// <param name="IDs">Массив идентификаторов родительских объектов.</param>
        public void RemoveByParentIDs (long[] IDs) {
            RemoveByIDsAndFields( IDs, 0 );
        }
        /// <summary>
        /// Удаляет все записи всех указанных дочерних объектов.
        /// </summary>
        /// <param name="IDs">Массив идентификаторов дочерних объектов.</param>
        public void RemoveByChildIDs (long[] IDs) {
            RemoveByIDsAndFields( IDs, 1 );
        }
        /// <summary>
        /// Удаляет записи, удовлетворяющие функции пользователя.
        /// </summary>
        /// <param name="func">Пользовательская функция. Возвращает TRUE, если необходимо удалить запись.</param>
        public void RemoveByUserFunc (SelectingFunc func) {
            if (Source == null)
                return;
            try {
                Source.RemoveRecord( TableName, rec => {
                    return func( new ObjectsLinkRecord( Convert.ToInt64( rec.Fields[0] ), Convert.ToInt64( rec.Fields[1] ) ) );
                } );
            } catch { }
        }
        /// <summary>
        /// Удалить все записи в базе данных.
        /// </summary>
        public void Clear () {
            if (Source == null)
                return;
            Source.RewriteTableWithRecords( TableName, null );
        }
        /// <summary>
        /// Добавляет записи в базу даных.
        /// </summary>
        /// <param name="objs">Массив записей.</param>
        public void AddRecords (ObjectsLinkRecord[] objs) {
            if (Source == null || objs == null)
                return;
            if (objs.Length == 0) return;
            Record[] result = new Record[objs.Length];
            for (int i = 0; i < objs.Length; i++) {
                string[] par = new string[2];
                par[0] = objs[i].ParentID.ToString();
                par[1] = objs[i].ChildID.ToString();
                result[i] = new Record( par );
            }
            Source.AppendRecords( TableName, result );
        }
    }

    /// <summary>
    /// Структура, реализующая одного объекта.
    /// </summary>
    public struct ObjectsLinkRecord {
        public long ParentID;
        public long ChildID;
        public ObjectsLinkRecord (long ParentID, long ChildID) {
            this.ParentID = ParentID;
            this.ChildID = ChildID;
        }
        public override string ToString () {
            return "{" + ParentID + "; " + ChildID + "}";
        }
    } //ObjectsLinkRecord
}
