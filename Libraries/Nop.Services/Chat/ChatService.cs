using Nop.Core.Data;
using Nop.Core.Domain.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Chat
{
    public class ChatService : IChatService
    {
       
        #region Ctor
        private readonly IRepository<Agents> _agentsRepository;
        private readonly IRepository<Convertation> _convertationRepository;
        private readonly IRepository<Messenger> _messengerRepository;

        public ChatService(IRepository<Agents> agentsRepository,
            IRepository<Convertation>  convertationRepository,
            IRepository<Messenger>  messengerRepository
            )
        {
            this._agentsRepository = agentsRepository;
            this._convertationRepository = convertationRepository;
            this._messengerRepository = messengerRepository; 
        }
        #endregion

        #region agent
       public virtual  List<Agents> GetallAgents(string AgentsName="")
        {
            var query = _agentsRepository.Table;
           if(!string.IsNullOrWhiteSpace(AgentsName))
           {
               query = query.Where(c => c.NickName == AgentsName);
           }
           return query.ToList();
        }
        public virtual Agents GetAgentsById(int AgentsId)
        {
            if (AgentsId == 0)
                return null;

            return _agentsRepository.GetById(AgentsId);
        }
        public virtual Agents GetAgentsByCustomerId(int CustomerId)
        {

            var query = _agentsRepository.Table;
            query = query.Where(m => !m.IsDelete && m.CustomerId == CustomerId);
            if (query.Count() > 0)
                return query.FirstOrDefault();
            else
                  return null ;
        }


        public virtual void InsertAgents(Agents _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Agents");
            _item.NgayTao = DateTime.Now;
            _agentsRepository.Insert(_item);
           
        }
        public virtual void UpdateAgents(Agents _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Agents");
            _agentsRepository.Update(_item);

        }
        public virtual void DeleteAgents(Agents _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Agents");

            _item.IsDelete = true;
            UpdateAgents(_item);
        }
        #endregion
        #region Convertation
        public virtual List<Convertation> GetallConvertation(int agentsid)
        {
            var query = _convertationRepository.Table;
           
                query = query.Where(c => c.AgentsId == agentsid || c.AgentsId == null);
            
            query = query.OrderByDescending(c=>c.Id);
            return query.ToList();
        }
        public virtual List<Convertation> GetallConversation_History(int agentsid)
        {
            var query = _convertationRepository.Table;
            if(query.Count()>0)
            {
                var item_last = query.OrderByDescending(c => c.Id).Take(10).ToList().Last();
                int id_last = item_last.Id;
                query = query.Where(c => c.AgentsId == agentsid && c.Id < id_last);
            }
           
            
            query = query.OrderByDescending(c=>c.Id);
            return query.ToList();
        }
        
        public virtual Convertation GetConvertationById(int ConvertationId)
        {
            if (ConvertationId == 0)
                return null;

            return _convertationRepository.GetById(ConvertationId);
        }
        public virtual Convertation GetConvertationBySessionCustomer(string session)
        {
            var query = _convertationRepository.Table;
            query = query.Where(m=>m.SessionConvertation==session);
            if (query.Count() > 0)
                return query.FirstOrDefault();
            else
                return null;
        }
        public virtual void InsertConvertation(Convertation _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Convertation");

            _convertationRepository.Insert(_item);

        }
        public virtual void UpdateConvertation(Convertation _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Convertation");
            _convertationRepository.Update(_item);

        }
       
        #endregion
        #region Messenger
        public virtual List<Messenger> GetallMessengerByConvertationId(int ConvertationId=0)
        {
            var query = _messengerRepository.Table;
            if(ConvertationId>0)
            {
                query = query.Where(c => c.ConvertationId == ConvertationId);
                query = query.OrderByDescending(m => m.Id);
            }
            return query.ToList();

        }
        public virtual List<Messenger> GetMessengerNew(int ConvertationId, int MessengerLastId)
        {
            var query = _messengerRepository.Table;           
                query = query.Where(c => c.ConvertationId == ConvertationId && c.Id>MessengerLastId);
               
            return query.ToList();

        }
        public virtual Messenger GetMessengerById(int MessengerId)
        {
            if (MessengerId == 0)
                return null;

            return _messengerRepository.GetById(MessengerId);
        }


        public virtual void InsertMessenger(Messenger _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Messenger");

            _messengerRepository.Insert(_item);

        }
        public virtual void UpdateMessenger(Messenger _item)
        {
            if (_item == null)
                throw new ArgumentNullException("Messenger");
            _messengerRepository.Update(_item);

        }


        #endregion
    }
}
