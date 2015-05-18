using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDB {
    using SelectingFunc = Func<ObjectRecord, bool>;

    /// <summary>
    /// Таблица объектов. (id & name)
    /// </summary>
    public class TableObjects {
        protected Database Source = null;
        protected const string TableName = "Objects";
        long[] IDs4Remove = null;
        string[] Names4Remove = null;

        TableObjects (Database source) {
            this.Source = source;
        }

        /// <summary>
        /// Создает таблицу объектов? прикрепленную к менеджеру базы данных.
        /// </summary>
        /// <param name="source">Ссылка на менеджер базы данных.</param>
        /// <returns>Возвращает ссылку на экземпляр класса таблицы.</returns>
        public static TableObjects Bind (Database source)
        {
            if (source == null)
                return null;
            return new TableObjects(source);
        }

        /// <summary>
        /// Загружает все записи из базы.
        /// </summary>
        /// <returns></returns>
        public ObjectRecord[] GetAllRecords () {
            if (Source == null) return null;
            try {
                Record[] records = Source.GetAllRecords( TableName );
                ObjectRecord[] result = new ObjectRecord[records.Length];
                for (int i = 0; i < result.Length; i++) {
                    try {
                        result[i] = new ObjectRecord( Convert.ToInt64( records[i].Fields[0] ), records[i].Fields[1] );
                    } catch { }
                } //for records
                return result;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// Удаляет все записи с указанными идентификаторами.
        /// </summary>
        /// <param name="IDs">Массив идентификаторов.</param>
        public void RemoveByIDs (long[] IDs) {
            if (Source == null)
                return;
            try {
                IDs4Remove = IDs;
                Source.RemoveRecord( TableName, rec => {
                    if (IDs4Remove == null)
                        return false;
                    for (int i = 0; i < IDs4Remove.Length; i++) {
                        if (Convert.ToInt64( rec.Fields[0] ) == IDs4Remove[i]) {
                            return true;
                        }
                    }
                    return false;
                } );
            } catch { }
            IDs4Remove = null;
        }
        /// <summary>
        /// Удаляет все записи, содержащие указанные имена.
        /// </summary>
        /// <param name="Names">Массив имен.</param>
        public void RemoveByNames (string[] Names) {
            if (Source == null)
                return;
            try {
                Names4Remove = Names;
                Source.RemoveRecord( TableName, rec => {
                    if (Names4Remove == null)
                        return false;
                    for (int i = 0; i < Names4Remove.Length; i++) {
                        if (rec.Fields[1] == Names4Remove[i]) {
                            return true;
                        }
                    }
                    return false;
                } );
            } catch { }
            Names4Remove = null;
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
                    return func( new ObjectRecord( Convert.ToInt64( rec.Fields[0] ), rec.Fields[1] ) );
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
        public void AddRecords (ObjectRecord[] objs) {
            if (Source == null || objs == null)
                return;
            if (objs.Length == 0) return;
            Record[] result = new Record[objs.Length];
            for (int i = 0; i < objs.Length; i++) {
                string[] par = new string[2];
                par[0] = objs[i].ID.ToString();
                par[1] = objs[i].Name;
                result[i] = new Record( par );
            }
            Source.AppendRecords( TableName, result );
        }
    }

    /// <summary>
    /// Структура, реализующая одного объекта.
    /// </summary>
    public struct ObjectRecord {
        static long maxID = 0;
        public long ID;
        public string Name;
        /// <summary>
        /// Конструктор с автоматическим выставлением идентификатора.
        /// Чтобы идентификаторы не конфликтовали, рекомендуется предварительно считать все существующие записи.
        /// </summary>
        /// <param name="name">Имя (описание) объекта.</param>
        public ObjectRecord (string name) {
            this.ID = maxID++;
            this.Name = name;
        }
        /// <summary>
        /// Конструктор с ручным выставлением идентификатора.
        /// Чтобы идентификаторы не конфликтовали, рекомендуется предварительно считать все существующие записи.
        /// </summary>
        /// <param name="name">Имя (описание) объекта.</param>
        public ObjectRecord (long id, string name) {
            if (id > maxID)
                maxID = id + 1;
            this.ID = id;
            this.Name = name;
        }
        public override string ToString () {
            return "{" + ID + "; " + Name + "}";
        }
    } //ObjectRecord
}
