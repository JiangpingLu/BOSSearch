//-------------------------------------------------------------------------------
// <Copyright file="PartyMng.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the function for manager party info in azure
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 11, 2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Dec 2, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 
//          Add description
//	Date Modified		: Dec 2, 2015
//	Changed By		    : AJ
//	Change Description  : Add authid of SmartyStreets for the API of ValidateAddress
//  Issue number        : 
/////////////////////////////////////////////////////////////////////////////////////////

using BOSMDMProdConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSMDMProdConsoleApp
{
    public class PartyMng
    {
        private BOSSearchCompanyContext bosSearchCompanyContext = new BOSSearchCompanyContext();
        private CompanyStatusHistoryContext companyStatusHistoryContext = new CompanyStatusHistoryContext();
        private ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BOSSearchConn"];
        /// <summary>
        /// Search the history data of companies in EF 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CompanyStatusHistory> SearchCompanyHistory()
        {
            return companyStatusHistoryContext.CompanyStatusHistories;
        }

        /// <summary>
        /// Insert data into companyhistory in EF
        /// </summary>
        /// <param name="companyHistory"></param>
        public bool SaveCompany(CompanyStatusHistory companyHistory)
        {
            bool isSaved = false;
            try
            {
                companyStatusHistoryContext.CompanyStatusHistories.Add(companyHistory);
                if(companyStatusHistoryContext.SaveChanges()!=-1)
                {
                    isSaved = true;
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return isSaved;
        }

        /// <summary>
        /// Insert data into companyhistory in SQL
        /// </summary>
        /// <param name="companyHistory"></param>
        /// <returns></returns>
        public bool SaveCompanyInSql(CompanyStatusHistory companyHistory)
        {
            bool res = false;
            if (setting == null)
            {
                return res;
            }
            var conn = setting.ConnectionString;
            List<Company> companyList = new List<Company>();
            var sql = string.Format("Insert into Companies (CompanyId,SourcePartyId,CompanyName,StatusAfter,StatusBefore,UpdateDate) values ({0},{1},{2},{3},{4},{5})", companyHistory.CompanyId, companyHistory.SourcePartyId, companyHistory.CompanyName, companyHistory.StatusAfter, companyHistory.StatusBefore, companyHistory.UpdateDate);
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    int result = cmd.ExecuteNonQuery();
                    if (result != -1)
                    {
                        res = true;
                    }
                }
                con.Close();
            }
            return res;
        }

        /// <summary>
        /// Search the data of companies in EF 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetCompanies()
        {
            return bosSearchCompanyContext.Companies;
        }

        /// <summary>
        /// Search the data of companies in SQL
        /// </summary>
        /// <returns></returns>
        public List<Company> GetCompany()
        {
            if (setting == null)
            {
                return null;
            }
            var conn = setting.ConnectionString;

            List<Company> companyList = new List<Company>();
            var sql = "select t.CompanyId,t.SourcePartyId,t.CompanyName,t.Email,t.Address,t.City,t.State,t.Status,t.Self from Companies t order by t.CompanyId";
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        
                        while (read.Read())
                        {
                            Company company = new Company();
                            company.CompanyId = Convert.ToInt32(read.GetValue(0).ToString());
                            company.SourcePartyId = read.GetValue(1).ToString();
                            company.CompanyName = read.GetValue(2).ToString();
                            company.Email = read.GetValue(3).ToString();
                            company.Address = read.GetValue(4).ToString();
                            company.City = read["City"].ToString();
                            company.State = read["State"].ToString();
                            company.Status = read["Status"].ToString();
                            company.Self = read["Self"].ToString();
                            companyList.Add(company);
                        }
                        read.Close();
                    }
                }
                con.Close();
            }
            return companyList;
        }

    }
}
