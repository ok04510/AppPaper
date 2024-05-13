using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;

namespace ASP.NET_MVC研修5.Models
{
    public class ApplicationInfoModel
    {
        public List<ApplicationRowModel> 検索結果一覧;
        public List<ApplicationRowModel> 表示一覧;
        public int 表示件数;
        public int 現ページ;
        public string ソート列;
        public string ソート順;


        public void Find(ApplicationIConditionModel condition)
        {
            現ページ = 1;

            string sql = "select * from application_info ";

            MySqlConnection con = new MySqlConnection(
                "server=localhost;port=3306;userid=root;password=root;" +
                "database = csharp; convert zero datetime=True");

            if (condition.表示状態 != null && condition.表示状態 != "")
            {
                sql = sql + "where application_info.APPLY_STATUS =" + condition.表示状態 + "";
            }

            con.Open();
            // 接続オブジェクト「con」を利用し、データベースサーバーとの
            // やり取り処理を制御するオブジェクトを作成する
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

            // クエリの実行結果を保存するテーブルオブジェクトを作成する
            DataTable tb = new DataTable();

            // クエリを実行し、結果をテーブルに入れる
            da.Fill(tb);
            // オブジェクトを解放する
            da.Dispose();
            // データベースサーバーとの接続を閉じる
            con.Close();

            検索結果一覧 = new List<ApplicationRowModel>();
            foreach (DataRow row in tb.Rows)
            {
                ApplicationRowModel result = new ApplicationRowModel();
                result.状態 = row["APPLY_STATUS"].ToString();
                result.申請ID = (int)row["APPLY_ID"];
                result.申請書類 = row["APPLY_FILE"].ToString();
                result.タイトル = row["TITLE"].ToString();
                result.申請日 = row["APPLY_TIME"].ToString();
                result.承認日 = row["APPROVE_TIME"].ToString();
                result.連絡事項 = row["NOTICE_MATTER"].ToString();

                検索結果一覧.Add(result);
            }

            SortAll(ソート列, ソート順);
            GetPage(表示件数, 現ページ);

        }

        public void GetPage(int rowCount, int pageNum)
        {
            if (rowCount == 0 || 検索結果一覧.Count <= rowCount)
            {
                表示一覧 = 検索結果一覧;
            }
            else
            {
                if (rowCount == 表示件数)
                {
                    表示一覧 = 検索結果一覧   // ↓
                        .Where((item, index) => index >= (pageNum - 1) *
                            rowCount && index < pageNum * rowCount)
                            .ToList();
                }
                else
                {
                    表示一覧 = 検索結果一覧.Where((item, index) =>
                        index < rowCount).ToList();
                }
            }

            表示件数 = rowCount;
            現ページ = pageNum;
        }

        // 「colName」はソート対象列名、「sortOrder」はソート順（昇順または降順）
        public void SortAll(string colName, string sortOrder)
        {
            // 昇順でソートする
            if (sortOrder == "▲" || sortOrder == "")
            {
                if (ソート列 == "状態")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.状態).ToList();
                }
                if (ソート列 == "申請ID")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.申請ID).ToList();
                }
                if (ソート列 == "申請書類")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.申請書類).ToList();
                }
                if (ソート列 == "タイトル")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.タイトル).ToList();
                }
                if (ソート列 == "申請日")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.申請日).ToList();
                }
                if (ソート列 == "承認日")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.承認日).ToList();
                }
                if (ソート列 == "連絡事項")
                {
                    検索結果一覧 = 検索結果一覧.OrderBy(x => x.連絡事項).ToList();
                }
            }
            else // 降順でソートする
            {
                if (ソート列 == "状態")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.状態).ToList();
                }
                if (ソート列 == "申請ID")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.申請ID).ToList();
                }
                if (ソート列 == "申請書類")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.申請書類).ToList();
                }
                if (ソート列 == "タイトル")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.タイトル).ToList();
                }
                if (ソート列 == "申請日")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.申請日).ToList();
                }
                if (ソート列 == "承認日")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.承認日).ToList();
                }
                if (ソート列 == "連絡事項")
                {
                    検索結果一覧 = 検索結果一覧.OrderByDescending(x => x.連絡事項).ToList();
                }
            }
        }

        public void Sort(string colName, string sortOrder)
        {
            // 下記3つ＞ソート情報と表示したいページ番号を再設定する
            ソート列 = colName;
            ソート順 = sortOrder;
            現ページ = 1;
            // 「検索結果一覧」をソートする
            SortAll(colName, sortOrder);
            // ソートされた「検索結果一覧」からデータを抜き出して
            // 「表示一覧」に設定する
            GetPage(表示件数, 現ページ);
        }

    }
}