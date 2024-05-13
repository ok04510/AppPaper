using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC研修5.Models;

namespace ASP.NET_MVC研修5.Controllers
{
    public class ApplicationForUserController : Controller
    {
        //
        // GET: /ApplicationForUser/
        /*public ActionResult Index(string colName, string sortOrder, ApplicationIConditionModel condition)
        {
            ApplicationInfoModel Appinfomodel = new ApplicationInfoModel();

            //Appinfomodel.表示区分 = 0;
            Appinfomodel.表示件数 = 10;

            Appinfomodel.Find(condition);

            //Appinfomodel.Sort(colName, sortOrder);
            //Appinfomodel.Sort(Appinfomodel.ソート列, Appinfomodel.ソート順);

            Session["ユーザー検索"] = Appinfomodel;

            return View("ApplicationManage", Appinfomodel);
        }*/
        public ActionResult Index()
        {
            return View("ApplicationManage");
        }


        [HttpPost]
        public ActionResult Find(ApplicationIConditionModel condition)
        {
            ApplicationInfoModel Appinfomodel = new ApplicationInfoModel();

            if (Session["ユーザー検索"] != null)
            {
                // 「検索」ボタンが一回以上クリックされた場合、前回の検索情報を取得する
                Appinfomodel = (ApplicationInfoModel)Session["ユーザー検索"];
            }
            else
            {
                Appinfomodel.ソート順 = "▲";
                Appinfomodel.ソート列 = "状態";
            }

            Appinfomodel.Find(condition);

            // "ユーザー検索"のキーでSessionにAppinfomodelを保存するSessionから
            // 保存したデータを抽出したい場合、"ユーザー検索"のキーで抽出できる
            Session["ユーザー検索"] = Appinfomodel;

            return PartialView("_ApplicationList", Appinfomodel);
        }

        [HttpPost]
        public ActionResult GetPage(int rowCount, int pageNum)
        {
            // 「Find」メソッドで保存したデータをSessionから抽出する
            ApplicationInfoModel Appinfomodel = (ApplicationInfoModel)Session["ユーザー検索"];
            Appinfomodel.GetPage(rowCount, pageNum);
            return PartialView("_ApplicationList", Appinfomodel);
        }
        
        [HttpPost]
        public ActionResult Sort(string colName, string sortOrder)
        {
            // 「検索」ボタンの処理でSessionに保存した"ユーザー検索"変数をSessionから抽出する
            ApplicationInfoModel Appinfomodel = (ApplicationInfoModel)Session["ユーザー検索"];
            // findModel内にある「検索結果一覧」をソートして、
            // 該当するデータを「表示一覧」に入れる
            Appinfomodel.Sort(colName, sortOrder);
            // パーシャルビューにソートした結果を反映し、反映した結果を
            // クライアントに返す
            // 存在する場合、次の画面を表示する
            return PartialView("_ApplicationList", Appinfomodel);

        }

	}
}