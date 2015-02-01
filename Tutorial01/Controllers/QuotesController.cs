using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Tariffic.Quotes;
using Tutorial01.Code;

namespace Tariffic.Quotes
{
    public class QuotesController : ApiController
    {
        private QuotesContext db = new QuotesContext();

        // GET api/Quotes
        public IEnumerable<Quote> GetQuotes(string q = null, string sort = null, bool desc = false, int? limit = null, int offset = 0)
        {
            var list = ((IObjectContextAdapter)db).ObjectContext.CreateObjectSet<Quote>();

            IQueryable<Quote> items = string.IsNullOrEmpty(sort) ? list.OrderBy(o=>o.Quote1)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined") items = items.Where(t => t.Quote1.Contains(q));

            if (offset > 0) items = items.Skip(offset);
            if (limit.HasValue) items = items.Take(limit.Value);
            return items;
        }
        [HttpGet]
        public IEnumerable<QuoteAggregate> QuotesCount()
        {
            var qts = from q in db.Quotes
                      group q by q.Mood into iMoodes
                      orderby iMoodes.Key
                      //select new IQueryable<QuoteAggregate>() { MoodDesc = iMoodes.Key, iMoodes};
                      select new QuoteAggregate() { Emotion = iMoodes.Key, Qty = iMoodes.Count() };
            return qts;
        }

        // GET api/Quotes/5
        public Quote GetQuotes(int id)
        {
            Quote Quotes = db.Quotes.Find(id);
            if (Quotes == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return Quotes;
        }        

        // PUT api/Quotes/5
        public HttpResponseMessage PutQuotes(int id, Quote quotes)
        {
            if (ModelState.IsValid && id == quotes.Id)
            {
                db.Entry(quotes).State = EntityState.Modified;
                quotes.Quote1 = quotes.Quote1.TarifficCase();
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Quotes
        public HttpResponseMessage PostQuotes(Quote quotes)
        {
            if (ModelState.IsValid)
            {
                quotes.Quote1 = quotes.Quote1.TarifficCase();
                db.Quotes.Add(quotes);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, quotes);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = quotes.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Quotes/5
        public HttpResponseMessage DeleteQuotes(int id)
        {
            Quote Quotes = db.Quotes.Find(id);
            if (Quotes == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Quotes.Remove(Quotes);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, Quotes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}