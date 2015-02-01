using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Tariffic.Quotes;
using Tutorial01.Code;


namespace Tutorial01.Controllers
{
    public class QuoteCountController:ODataController
    {
        private QuotesContext db = new QuotesContext();
        public IEnumerable<Quote> Get()
        {            
            /*var qts = from q in db.Quotes
                      group q by q.Mood into iMoodes
                      orderby iMoodes.Key
                      //select new IQueryable<QuoteAggregate>() { MoodDesc = iMoodes.Key, iMoodes};
                      select new QuoteAggregate() { Emotion = iMoodes.Key, Qty = iMoodes.Count()} ;            
            return qts;*/
            return db.Quotes;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}